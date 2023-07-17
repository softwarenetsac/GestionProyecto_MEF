using Gestion_Rendimiento_Entity;
using Gestion_Rendimiento_Entity.Model;
using Gestion_Rendimiento_Frontend.Extensiones;
using Gestion_Rendimiento_IService;
using Gestion_Rendimiento_Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gestion_Rendimiento_Common;
using System.Net;
using Gestion_Rendimiento_Frontend.Models;
using Gestion_Rendimiento_Entity.Request;
using System.Globalization;
using DocumentFormat.OpenXml.Office2016.Excel;
using System.Security.Policy;
using Microsoft.AspNetCore.Mvc.Rendering;
using DocumentFormat.OpenXml.Office.CustomUI;

namespace Gestion_Rendimiento_Frontend.Controllers
{
    public class RendimientoController : BaseController
    {


        private readonly IConfiguracionRendimientoService _configuracionRendimientoService;
        private readonly IRendimientoService _RendimientoService;
        private readonly IOficinaService _oficinaService;
        private readonly IEvaluadoService _evaluadoService;
        private readonly IProyectoService _proyectoService;
        private readonly IProyectoDetalleService _proyectodetalleService;
        private readonly IPersonaService _personaService;
        public RendimientoController(
          IPersonaService personaService,
          IOficinaService oficinaService,
          IRendimientoService RendimientoService,
          IEvaluadoService evaluadoService,
          IProyectoDetalleService proyectodetalleService,
          IProyectoService proyectoService,
          IConfiguracionRendimientoService configuracionRendimientoService,
          IOptions<ConfiguracionSistemaModel> configuracionSistema,
           IHttpContextAccessor contextAccessor
            )
        {
            _proyectodetalleService = proyectodetalleService;
            _proyectoService = proyectoService;
            _oficinaService = oficinaService;
            _RendimientoService = RendimientoService;
            _configuracionRendimientoService = configuracionRendimientoService;
            _configuracionSistemaBase = configuracionSistema.Value;
            _contextAccessor = contextAccessor;
            _evaluadoService = evaluadoService;
            _personaService = personaService;
        }
        #region PROGRAMACION
        public IActionResult Programacion()
        {
            var modelo = new RendimientoViewModel();
            var usuario_conectado = _personaService.Detalle(UsuarioActual.ID_PERSONAL);
            modelo.Personas = _personaService.GetTodosXUnidad(usuario_conectado.ID_AREA);
            string Anio = _proyectoService.Proyecto_Min_Ano();
            int Anio_a=DateTime.Now.Year;
            modelo.List_Anio = new List<SelectListItem>();
            if (Anio!=null || Anio != "")
            {
                if (Anio_a == int.Parse(Anio))
                {
                    modelo.List_Anio.Add(new SelectListItem { Value = Anio_a.ToString(), Text = Anio_a.ToString() });
                }
                else
                {
                    for (int I = int.Parse(Anio); I <= Anio_a; I += 1)
                    {
                        modelo.List_Anio.Add(new SelectListItem { Value = I.ToString(), Text = I.ToString()});
                    }
                }
            }
            return View(modelo);
        }
        public IActionResult GenerarHTML([FromBody] RendimientoModel model)
        {
            var respuesta = new BaseResponse();
            respuesta.Extra = FilaTabla(model);

            return Ok(respuesta);

        }
        private string FilaTabla(RendimientoModel modelo)
        {
            string html = "";
            try
            {
                var listaPrioridad = new List<ProyectoModel>();
                if (modelo.ID_PROYECTO > 0)
                {
                    var lista = _RendimientoService.GetOne(modelo.ID_PROYECTO);
                    var entity = new ProyectoModel
                    {
                        DESCRIPCION = lista.DESCRIPCION,
                        ID_PROYECTO = lista.ID_PROYECTO,
                    };
                    listaPrioridad.Add(entity);
                }
                else
                {
                    listaPrioridad = null;
                    listaPrioridad = modelo.ItemsProyecto == null ? new List<ProyectoModel>() : modelo.ItemsProyecto;
                    if (modelo.TIPO == "E")
                    {
                        var entity = new ProyectoModel
                        {
                            DESCRIPCION = "",
                            ID_PROYECTO = 0
                        };
                        listaPrioridad.Add(entity);
                    }
                }

                var det = _proyectodetalleService.GetProyectoXId(modelo.ID_PROYECTO);
                List<RendimientoDetalleModel> det_datos = null;
                if (det != null)
                {
                    det_datos = det.Select(t => new RendimientoDetalleModel
                    {
                        ID_DETALLE = t.ID_DETALLE_PROYECTO,
                        ID_PROYECTO = t.ID_PROYECTO,
                        INDICADOR = t.INDICADOR_PRODUCTO,
                        VALOR = t.VALOR,
                        EVIDENCIA = t.EVIDENCIA,
                        PLAZOS = t.PLAZO,
                    }).ToList();
                }
                var listaPrioridadD = modelo.ItemsPrioridad.Count == 0 ? (det == null ? new List<RendimientoDetalleModel>(): det_datos) : modelo.ItemsPrioridad;
                if (modelo.TIPO == "D")
                {
                    var entity_ = new RendimientoDetalleModel
                    {
                        EVIDENCIA = "",
                        PLAZOS = "",
                        INDICADOR = "",
                        VALOR = 0,
                        ID_PROYECTO = modelo.ID_DETALLE_TEMP,
                        ID_DETALLE = 0
                    };
                    listaPrioridadD.Add(entity_);
                }


                if (listaPrioridad.Count() > 0)
                {
                    foreach (var item_PROYECTO in listaPrioridad)
                    {
                        var detalle = listaPrioridadD.Where(x => x.ID_PROYECTO == item_PROYECTO.ID_PROYECTO).ToList();
                        html += GrupoCabeceraRegistroHTML(item_PROYECTO.ID_PROYECTO, item_PROYECTO.DESCRIPCION);
                        if (detalle.Count > 0)
                        {
                            foreach (var item in detalle)
                            {
                                html += GrupoDetalleRegistroHTML(item.EVIDENCIA, item.PLAZOS, item.INDICADOR, item.VALOR, item.ID_DETALLE, item.ID_PROYECTO);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                html = "";
                Log.CreateLogger(ex.Message);

            }
            return html;

        }
        private string GrupoCabeceraRegistroHTML(int ID, string DES)
        {
            string html = "";
            html += "<tr style='background-color:#FCE4D6;' id='" + ID + "'>";
            html += "<td colspan='5'>";
            html += "<div class='form-group'>";
            html += "<textarea rows=\"3\" cols=\"50\" id='" + ID + "' name='" + ID + "' maxlength='4000' class='form-control tdDESCRIPCION' required>" + DES + "</textarea>";
            html += "</div>";
            html += "</td>";
            if (ID > 0)
            {
                html += "<td style='text-align:center'>";
                html += "<a value=" + ID + " href ='javascript:void(0);' idpk_e=" + ID + " class='AddRegDet' ><i class='icon icon-plus-circle icon-2x'></i></a>";
                html += "</td>";
            }

            html += "<td class='tdID_DETALLE' style='display:none;'>" + ID + "</td>";
            html += "<td class='tdP_G' style='display:none;'>1</td>";
            html += "</tr>";
            return html;
        }
        private string GrupoDetalleRegistroHTML(string EVIDENCIA, string PLAZOS, string INDICADOR, int VALOR, int FILA, int ID_PROYECTO)
        {
            string html = "";
            string remove = deleteFila(FILA, "1");
            //var td_id = GetId();
            var E = "S_EV_" + FILA;
            var P = "S_P_" + FILA;
            var I = "S_I_" + FILA;
            var V = "S_V_" + FILA;
            html += "<tr>";
            html += "<td class='tdID_PROYECTO_S' style='display:none;'>" + ID_PROYECTO + "</td>";
            html += "<td>" + remove + "</td>";
            html += "<td>";
            html += "<div class='form-group'>";
            html += "<label>Indicador / Producto<span class=\"text-primary m-l-sm\">(*)</span></label>";
            html += "<textarea id='" + I + "' name='" + I + "' maxlength='4000' rows=\"4\" cols=\"50\" class='form-control tdINDICADOR' required>" + INDICADOR + "</textarea>";
            html += "</div>";
            html += "</td>";

            html += "<td>";
            html += "<div class='form-group'>";
            html += "<label>Valor Meta<span class=\"text-primary m-l-sm\">(*)</span></label>";
            html += "<input id='" + V + "' name='" + V + "' type ='text' maxlength='2'  value='" + VALOR + "' class='form-control tdVALOR' required/>";
            html += "</div>";
            html += "</td>";

            html += "<td>";
            html += "<div class='form-group'>";
            html += "<label>Evidencia<span class=\"text-primary m-l-sm\">(*)</span></label>";
            html += "<textarea id='" + E + "' name='" + E + "'  rows=\"4\" cols=\"50\" maxlength='4000' class='form-control tdEVIDENCIA' required>" + EVIDENCIA + "</textarea>";
            html += "</div>";
            html += "</td>";

            html += "<td>";
            html += "<div class='form-group'>";
            html += "<label>Plazos<span class=\"text-primary m-l-sm\">(*)</span></label>";
            if (PLAZOS == "")
            {
                html += "<input id='" + P + "' name='" + P + "' type ='text' maxlength='10'  value='" + PLAZOS + "' class='form-control tdPLAZOS' data-provide=\"datepicker\" data-date-today-highlight=\"true\" data-date-format=\"dd/mm/yyyy\" data-date-language=\"es\" data-date-autoclose=\"true\" required/>";

            }
            else
            {
                html += "<input id='" + P + "' name='" + P + "' type ='text' maxlength='10'  value='" + Convert.ToDateTime(PLAZOS).ToString("dd/MM/yyyy") + "' class='form-control tdPLAZOS' data-provide=\"datepicker\" data-date-today-highlight=\"true\" data-date-format=\"dd/mm/yyyy\" data-date-language=\"es\" data-date-autoclose=\"true\" required/>";
            }
            html += "</div>";
            html += "</td>";
            html += "<td class='tdID_SUB_DETALLE' style='display:none;'>1</td>";
            html += "<td class='tdDetPrioridad' style='display:none;'>";
            html += FILA;
            html += "</td>";

            html += "</tr>";
            return html;
        }
        private string deleteFila(int FILA, string editable)
        {
            string html = "";
            if (editable == "1")
            {
                html = "<a value=" + FILA + " href ='javascript:void(0);' ><i class='icon icon-remove icon-2x delete_mantenimiento'></i></a>";
            }
            return html;

        }
        public BaseResponse GuardarProyecto([FromBody] RendimientoModel items)
        {
            BaseResponse respuesta = new BaseResponse();
            var usuario_conectado = _personaService.Detalle(UsuarioActual.ID_PERSONAL);
            string usuario_login = UsuarioActual == null ? "" : UsuarioActual.UsuarioLogin;
            // string correos = "";
            int id = 0;
            foreach (var item in items.ItemsProyecto)
            {

                var entidad = new Proyecto
                {
                    DESCRIPCION = item.DESCRIPCION,
                    ID_PERSONAL = usuario_conectado.ID_PERSONAL,
                    ID_OFICINA = usuario_conectado.ID_OFICINA,
                    ID_ESTADO = 1,
                    ID_EVALUADOR = item.ID_EVALUADOR,
                    USUARIO_CREACION = usuario_login,
                    FECHA_CREACION = DateTime.Now,
                    ANIO = DateTime.Now.Year.ToString(),
                    IP_CREACION = IP,
                    FLG_ESTADO = "1",
                    ID_PROYECTO = item.ID_PROYECTO,
                };
                if (item.ID_PROYECTO == 0)
                {
                    var valor = _proyectoService.Insertar(entidad);
                    id = valor.ID_PROYECTO;
                }
                else
                {
                    var valor = _proyectoService.Actualizar(entidad);
                    id = valor.ID_PROYECTO;
                }

                if (id > 0)
                {
                    respuesta.Success = true;
                    respuesta.Message = "Se ha procesado correctamente";
                }
            }
            if (id > 0)
            {
                foreach (var item in items.ItemsPrioridad)
                {

                    var entidad = new Proyecto_Detalle
                    {
                        ID_PROYECTO = item.ID_PROYECTO,
                        ID_DETALLE_PROYECTO = item.ID_DETALLE,
                        INDICADOR_PRODUCTO = item.INDICADOR,
                        VALOR = item.VALOR,
                        PLAZO = item.PLAZOS,
                        EVIDENCIA = item.EVIDENCIA,
                        USUARIO_CREACION = usuario_login,
                        FECHA_CREACION = DateTime.Now,
                        IP_CREACION = IP,
                        FLG_ESTADO = "1",

                    };
                    if (item.ID_DETALLE == 0)
                    {
                        var valor = _proyectodetalleService.Insertar(entidad);
                        id = valor.ID_DETALLE_PROYECTO;
                    }
                    else
                    {
                        var valor = _proyectodetalleService.Actualizar(entidad);
                        id = valor.ID_DETALLE_PROYECTO;
                    }

                    if (id > 0)
                    {
                        respuesta.Success = true;
                        respuesta.Message = "Se ha procesado correctamente";
                    }
                }
            }
            return respuesta;
        }
        public IActionResult GetAll_Proyecto([FromBody] RendimientoConsultaModel request)
        {
            var result = new MethodResponseModel<IEnumerable<RendimientoConsultaModel>> { Result = null };
            request.TIPO = "U";
            string id_personal = "";
            if (UsuarioActual != null)
            {
                id_personal = UsuarioActual.ID_PERSONAL;
            }
            request.ID_PERSONAL = id_personal;
            var lista = new List<RendimientoConsultaModel>();
            if (request.ANIO == "0")
            {
                lista = GetLista_Seguimiento(request).ToList();
            }
            else
            {
                lista = GetLista_Seguimiento(request).Where(x => x.ANIO == request.ANIO).ToList();
            }
            result.Result = lista;
            return Ok(result);

        }
        #endregion
        public IActionResult GetAll_Programacion([FromBody] RendimientoConsultaModel request)
        {
            var result = new MethodResponseModel<IEnumerable<RendimientoConsultaModel>> { Result = null };
            request.TIPO = "U";
            string id_personal = "";
            if (UsuarioActual != null)
            {
                id_personal = UsuarioActual.ID_PERSONAL;
            }
            request.ID_PERSONAL = id_personal;
            var lista = GetLista_Seguimiento(request).ToList();
            result.Result = lista;
            return Ok(result);

        }
        #region SEGUIMIENTO
        public IActionResult Seguimiento()
        {
            return View();
        }

        public IActionResult  GetAll_Seguimiento([FromBody] RendimientoConsultaModel request)
        {
            var result = new MethodResponseModel<IEnumerable<RendimientoConsultaModel>> { Result = null };
            var lista = GetLista_Seguimiento(request).ToList();
            result.Result = lista;
            return Ok(result);

        }
        private List<RendimientoConsultaModel> GetLista_Seguimiento(RendimientoConsultaModel request)
        {
            var lista = _RendimientoService.GetAll(request).ToList();
            return lista;
        }
        private MethodResponseModel<IEnumerable<RendimientoConsultaModel>> ListarReporteSeguimiento([FromBody] RendimientoConsultaModel request)
        {
            var result = new MethodResponseModel<IEnumerable<RendimientoConsultaModel>> { Result = null };
            try
            {
                var lista = GetLista_Seguimiento(request);
                result.Result = lista;
                result.Success = lista.Count > 0;
            }
            catch (Exception ex)
            {
                Log.CreateLogger(ex.Message);
                result.Message = ex.Message;
                result.Code = (int)HttpStatusCode.InternalServerError;
                result.Result = new List<RendimientoConsultaModel>();
            }
            return result;
        }
        #endregion
        public IActionResult ExportarExcelSeguimiento(RendimientoConsultaModel request)
        {
            try
            {
                //generamos el reporte
                ExcelPackage.LicenseContext = LicenseContext.Commercial;
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                int row = 3;
                using (ExcelPackage package = new ExcelPackage())
                {
                    var ws1 = package.Workbook.Worksheets.Add("Informacion");
                    //creamos el titulo del reporte
                    using (var rng = ws1.Cells[2, 1, 2, 5])
                    {
                        rng.Style.Font.Bold = true;
                        rng.Style.WrapText = true;
                        rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rng.Style.Font.Size = 15;
                        rng.Merge = true;
                        rng.Value = "Seguimiento de Proyectos";
                    }

                    row += 1;
                    int fila = 1;
                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "Proyecto", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 70; fila++;

                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "Año", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 70; fila++;

                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "Evaluado", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 30; fila++;

                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "Puesto", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 10; fila++;

                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "Evaluador", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 15; fila++;

                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "Plazo", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 10; fila++;

                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "Estado", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 15; fila++;

                    ws1.Row(row).Height = 30;

                    var data = this.ListarReporteSeguimiento(request);
                    if (data.Success)
                    {
                        if (data.Result.Count() > 0)
                        {

                            foreach (var item in data.Result)
                            {
                                row++;
                                int columna = 1;
                                ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.DESCRIPCION, row, columna, 0, 0, "L"); columna++;
                                ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.ANIO, row, columna, 0, 0, "L"); columna++;
                                ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.NOMBRE_EVALUADO, row, columna, 0, 0, "L"); columna++;
                                ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.NOMBRE_CARGO, row, columna, 0, 0, "L"); columna++;
                                ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.NOMBRE_EVALUADOR, row, columna, 0, 0, "L"); columna++;
                                ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.PLAZO, row, columna, 0, 0, "L"); columna++;
                                ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.NOMBRE_ESTADO, row, columna, 0, 0, "L"); columna++;
                            }
                        }
                    }

                    string strFileName = "Seguimiento" + DateTime.Now.ToString() + ".xlsx";
                    byte[] dataByte = package.GetAsByteArray();
                    return FileDownload(dataByte, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", strFileName);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #region     EVALUACION
        public IActionResult Evaluacion()
        {
            return View();
        }
        #endregion
        #region RENDIMIENTO ORH
        public IActionResult RendimientoORH()
        {
            var modelo = new EvaluadoViewModel();
            modelo.Oficinas = _oficinaService.GetOrganosTodos().ToList();
            var usuario_conectado = _personaService.Detalle(UsuarioActual.ID_PERSONAL);
            modelo.Personas = _personaService.GetTodosXUnidad(usuario_conectado.ID_AREA);
            string Anio = _proyectoService.Proyecto_Min_Ano();
            int Anio_a = DateTime.Now.Year;
            modelo.List_Anio = new List<SelectListItem>();
            if (Anio != null || Anio != "")
            {
                if (Anio_a == int.Parse(Anio))
                {
                    modelo.List_Anio.Add(new SelectListItem { Value = Anio_a.ToString(), Text = Anio_a.ToString() });
                }
                else
                {
                    for (int I = int.Parse(Anio); I <= Anio_a; I += 1)
                    {
                        modelo.List_Anio.Add(new SelectListItem { Value = I.ToString(), Text = I.ToString() });
                    }
                }
            }
            return View(modelo);
        }
        public IActionResult GenerarHTML_Cons([FromBody] RendimientoModel model)
        {
            var respuesta = new BaseResponse();
            respuesta.Extra = FilaTabla_Cons(model);

            return Ok(respuesta);

        }
        private string FilaTabla_Cons(RendimientoModel modelo)
        {
            string html = "";
            try
            {
                var listaPrioridad = new List<ProyectoModel>();
                    var lista = _RendimientoService.GetOne(modelo.ID_PROYECTO);
                    var entity = new ProyectoModel
                    {
                        DESCRIPCION = lista.DESCRIPCION,
                        ID_PROYECTO = lista.ID_PROYECTO,
                    };
                    listaPrioridad.Add(entity);
                


                var det = _proyectodetalleService.GetProyectoXId(modelo.ID_PROYECTO);
                List<RendimientoDetalleModel> det_datos = null;
                if (det != null)
                {
                    det_datos = det.Select(t => new RendimientoDetalleModel
                    {
                        ID_DETALLE = t.ID_DETALLE_PROYECTO,
                        ID_PROYECTO = t.ID_PROYECTO,
                        INDICADOR = t.INDICADOR_PRODUCTO,
                        VALOR = t.VALOR,
                        EVIDENCIA = t.EVIDENCIA,
                        PLAZOS = t.PLAZO,
                    }).ToList();
                }
                var listaPrioridadD = det == null ? new List<RendimientoDetalleModel>() : det_datos;
                if (listaPrioridad.Count() > 0)
                {
                    foreach (var item_PROYECTO in listaPrioridad)
                    {
                        var detalle = listaPrioridadD.Where(x => x.ID_PROYECTO == item_PROYECTO.ID_PROYECTO).ToList();
                        html += GrupoCabeceraRegistroHTML(item_PROYECTO.ID_PROYECTO, item_PROYECTO.DESCRIPCION);
                        if (detalle.Count > 0)
                        {
                            foreach (var item in detalle)
                            {
                                html += GrupoDetalleRegistroHTML(item.EVIDENCIA, item.PLAZOS, item.INDICADOR, item.VALOR, item.ID_DETALLE, item.ID_PROYECTO);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                html = "";
                Log.CreateLogger(ex.Message);

            }
            return html;

        }
        private List<RendimientoConsultaModel> GetLista_Rendimiento(RendimientoConsultaModel request)
        {
            var lista = _RendimientoService.GetAll(request).ToList();
            return lista;
        }
        public IActionResult GetAll_Rendimiento([FromBody] RendimientoConsultaModel request)
        {
            var result = new MethodResponseModel<IEnumerable<RendimientoConsultaModel>> { Result = null };
            request.TIPO = "ORH";
            var lista = GetLista_Rendimiento(request).ToList();
            result.Result = lista;
            return Ok(result);

        }
        private MethodResponseModel<IEnumerable<RendimientoConsultaModel>> ListarReporte_Rendimiento([FromBody] RendimientoConsultaModel request)
        {
            var result = new MethodResponseModel<IEnumerable<RendimientoConsultaModel>> { Result = null };
            try
            {
                request.TIPO = "ORH";
                var lista = GetLista_Rendimiento(request);
                result.Result = lista;
                result.Success = lista.Count > 0;
            }
            catch (Exception ex)
            {
                Log.CreateLogger(ex.Message);
                result.Message = ex.Message;
                result.Code = (int)HttpStatusCode.InternalServerError;
                result.Result = new List<RendimientoConsultaModel>();
            }
            return result;
        }
        public IActionResult ExportarExcel_Rendimiento(RendimientoConsultaModel request)
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.Commercial;
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                int row = 3;
                using (ExcelPackage package = new ExcelPackage())
                {
                    var ws1 = package.Workbook.Worksheets.Add("Informacion");
                    using (var rng = ws1.Cells[2, 1, 2, 5])
                    {
                        rng.Style.Font.Bold = true;
                        rng.Style.WrapText = true;
                        rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rng.Style.Font.Size = 15;
                        rng.Merge = true;
                        rng.Value = "Lista de Evaluados";
                    }

                    row += 1;
                    int fila = 1;
                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "Año", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 10; fila++;
                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "Prioridades Anuales", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 50; fila++;
                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "Evaluado", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 10; fila++;
                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "Cargo", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 15; fila++;
                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "Evaluador", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 50; fila++;

                    ws1.Row(row).Height = 30;

                    var data = this.ListarReporte_Rendimiento(request);
                    if (data.Success)
                    {
                        if (data.Result.Count() > 0)
                        {

                            foreach (var item in data.Result)
                            {
                                row++;
                                int columna = 1;
                                ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.ANIO, row, columna, 0, 0, "L"); columna++;
                                ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.DESCRIPCION, row, columna, 0, 0, "L"); columna++;
                                ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.NOMBRE_EVALUADO, row, columna, 0, 0, "L"); columna++;
                                ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.NOMBRE_CARGO, row, columna, 0, 0, "L"); columna++;
                                ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.NOMBRE_EVALUADOR, row, columna, 0, 0, "L"); columna++;
                            }
                        }
                    }

                    string strFileName = "Evaluado" + DateTime.Now.ToString() + ".xlsx";
                    byte[] dataByte = package.GetAsByteArray();
                    return FileDownload(dataByte, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", strFileName);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
   