using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Gestion_Rendimiento_Common;
using Gestion_Rendimiento_Entity;
using Gestion_Rendimiento_Entity.Model;
using Gestion_Rendimiento_Frontend.Extensiones;
using Gestion_Rendimiento_Frontend.Extensiones.Email;
using Gestion_Rendimiento_Frontend.Models;
using Gestion_Rendimiento_IService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_Frontend.Controllers
{
    public class EvaluadorController : BaseController
    {
        private readonly IOficinaService _oficinaService;
        private readonly IPersonaService _personaService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUsuarioService _usuarioService;
        private readonly IVariableService _variableService;
        private readonly IEvaluadorService _evaluadorService;
        public EvaluadorController(
                   IOficinaService oficinaService,
        IPersonaService personaService,
        IOptions<ConfiguracionSistemaModel> configuracionSistema,
                 IHttpContextAccessor contextAccessor,
                 IWebHostEnvironment webHostEnvironment,
                 IUsuarioService usuarioService,
                 IVariableService variableService,
                 IEvaluadorService evaluadorService
            ) {
            _oficinaService = oficinaService;
            _personaService = personaService;
            _configuracionSistemaBase = configuracionSistema.Value;
            _contextAccessor = contextAccessor;
            _webHostEnvironment = webHostEnvironment;
            _usuarioService = usuarioService;
            _variableService = variableService;
            _evaluadorService = evaluadorService;
        }

        public IActionResult Index()
        {
            var tipo = Constantes.TipoOrgano;
            var modelo = new EvaluadorViewModel();
            modelo.Oficinas = _oficinaService.GetOrganos(tipo).ToList();
            modelo.Personas = _personaService.GetTodos("1", "0").ToList();
            return View(modelo);
        }



   


        public IActionResult ProcearAutorizador([FromBody] EvaluadorModel modelo)
        {
            var result = new MethodResponseModel<string> { };
            try
            {
                var flg_estado = (int)StatusEnums.Active;


                if (string.IsNullOrEmpty(modelo.ID_AREA_JEFE))
                {
                    throw new Exception("Dede seleccionar una Unidad Orgánica");
                }

                if (string.IsNullOrEmpty(modelo.ID_OFICINA_JEFE))
                {
                    throw new Exception("Dede seleccionar un Órgano");
                }

                if (!string.IsNullOrEmpty(modelo.FECHA_INICIO_JEFE_CADENA))
                {
                    modelo.FECHA_INICIO_JEFE = DateTime.ParseExact(modelo.FECHA_INICIO_JEFE_CADENA, "dd/MM/yyyy", null);
                }

                if (!string.IsNullOrEmpty(modelo.FECHA_FINAL_JEFE_CADENA))
                {
                    modelo.FECHA_FINAL_JEFE = DateTime.ParseExact(modelo.FECHA_FINAL_JEFE_CADENA, "dd/MM/yyyy", null);
                }

                if (!string.IsNullOrEmpty(modelo.FECHA_INICIO_ALTERNO_CADENA))
                {

                    if (string.IsNullOrEmpty(modelo.FECHA_FINAL_JEFE_CADENA))
                    {
                        throw new Exception("Si estable fecha de incio del alterno debe ingresar fecha final del responsable principal");
                    }
                    else
                    {
                        modelo.FECHA_INICIO_ALTERNO = DateTime.ParseExact(modelo.FECHA_INICIO_ALTERNO_CADENA, "dd/MM/yyyy", null);
                    }
                }

                if (!string.IsNullOrEmpty(modelo.FECHA_FINAL_ALTERNO_CADENA))
                {
                    modelo.FECHA_FINAL_ALTERNO = DateTime.ParseExact(modelo.FECHA_FINAL_ALTERNO_CADENA, "dd/MM/yyyy", null);
                }

                if (!string.IsNullOrEmpty(modelo.FECHA_DOCUMENTO_JEFE_CADENA))
                {
                    modelo.FECHA_DOCUMENTO_JEFE = DateTime.ParseExact(modelo.FECHA_DOCUMENTO_JEFE_CADENA, "dd/MM/yyyy", null);
                }

                if (!string.IsNullOrEmpty(modelo.FECHA_DOCUMENTO_ALTERNO_CADENA))
                {
                    modelo.FECHA_DOCUMENTO_ALTERNO = DateTime.ParseExact(modelo.FECHA_DOCUMENTO_ALTERNO_CADENA, "dd/MM/yyyy", null);
                }

                if (
               (!string.IsNullOrEmpty(modelo.FECHA_INICIO_JEFE_CADENA)) &&
              (!string.IsNullOrEmpty(modelo.FECHA_FINAL_JEFE_CADENA))
              )
                {

                    var inicio = (DateTime.ParseExact(modelo.FECHA_INICIO_JEFE_CADENA, "dd/MM/yyyy", null)).ToString("yyyyMMdd");
                    var final = (DateTime.ParseExact(modelo.FECHA_FINAL_JEFE_CADENA, "dd/MM/yyyy", null)).ToString("yyyyMMdd");
                    if (Convert.ToInt32(final) < Convert.ToInt32(inicio))
                    {
                        throw new Exception("Fecha final del responsable principal debe ser mayor a la fecha de incio");
                    }
                }


                if (
          (!string.IsNullOrEmpty(modelo.FECHA_INICIO_JEFE_CADENA)) &&
         (!string.IsNullOrEmpty(modelo.FECHA_INICIO_ALTERNO_CADENA))
         )
                {

                    var inicio = (DateTime.ParseExact(modelo.FECHA_INICIO_JEFE_CADENA, "dd/MM/yyyy", null)).ToString("yyyyMMdd");
                    var final = (DateTime.ParseExact(modelo.FECHA_INICIO_ALTERNO_CADENA, "dd/MM/yyyy", null)).ToString("yyyyMMdd");
                    if (Convert.ToInt32(final) < Convert.ToInt32(inicio))
                    {
                        throw new Exception("Fecha de inicio del responsable alterno debe ser mayor a la fecha de incio del responsable principal");
                    }
                }



                if (
                  (!string.IsNullOrEmpty(modelo.FECHA_INICIO_ALTERNO_CADENA)) &&
                 (!string.IsNullOrEmpty(modelo.FECHA_FINAL_ALTERNO_CADENA))
                 )
                {

                    var inicio = (DateTime.ParseExact(modelo.FECHA_INICIO_ALTERNO_CADENA, "dd/MM/yyyy", null)).ToString("yyyyMMdd");
                    var final = (DateTime.ParseExact(modelo.FECHA_FINAL_ALTERNO_CADENA, "dd/MM/yyyy", null)).ToString("yyyyMMdd");
                    if (Convert.ToInt32(final) < Convert.ToInt32(inicio))
                    {
                        throw new Exception("Fecha final del responsable alterno debe ser mayor a la fecha de incio del alterno");
                    }
                }
                if (
                  (!string.IsNullOrEmpty(modelo.FECHA_FINAL_JEFE_CADENA)) &&
                 (!string.IsNullOrEmpty(modelo.FECHA_INICIO_ALTERNO_CADENA))
                 )
                {
                    var incio = (DateTime.ParseExact(modelo.FECHA_FINAL_JEFE_CADENA, "dd/MM/yyyy", null)).ToString("yyyyMMdd");
                    var final = (DateTime.ParseExact(modelo.FECHA_INICIO_ALTERNO_CADENA, "dd/MM/yyyy", null)).ToString("yyyyMMdd");
                    if (Convert.ToInt32(incio) > Convert.ToInt32(final))
                    {
                        throw new Exception("Fecha final del responsable principal debe ser menor a la fecha de incio del alterno");
                    }
                }



                if (string.IsNullOrEmpty(modelo.ID_OFICINA_JEFE))
                {
                    modelo.ID_OFICINA_JEFE = "0";
                }
                var id = modelo.ID_EVALUADOR;
                var usuario_login = "";
                string usuario_remitente = "Oficina de ORH";
                string id_personal_remitente = "";
                string correo_remitente = "";
                if (UsuarioActual != null)
                {
                    usuario_login = UsuarioActual.UsuarioLogin;
                    usuario_remitente = UsuarioActual.Nombre;
                    id_personal_remitente = UsuarioActual.ID_PERSONAL;
                    var usuario_conectado = _personaService.Detalle(id_personal_remitente);
                    if (usuario_conectado != null)
                    {
                        correo_remitente = usuario_conectado.CORREO_INSTITUCIONAL;
                    }
                }
                var entidad = new Evaluador
                {
                    ID_EVALUADOR = modelo.ID_EVALUADOR,
                    ID_OFICINA_JEFE = modelo.ID_OFICINA_JEFE == "0" ? modelo.ID_AREA_JEFE : modelo.ID_OFICINA_JEFE,
                    ID_AREA_JEFE = modelo.ID_AREA_JEFE,
                    ID_PERSONA_JEFE = modelo.ID_PERSONA_JEFE,
                    ID_PERSONA_ALTERNO = modelo.ID_PERSONA_ALTERNO,
                    FECHA_INICIO_JEFE = modelo.FECHA_INICIO_JEFE,
                    FECHA_FINAL_JEFE = modelo.FECHA_FINAL_JEFE,
                    FECHA_INICIO_ALTERNO = modelo.FECHA_INICIO_ALTERNO,
                    FECHA_FINAL_ALTERNO = modelo.FECHA_FINAL_ALTERNO,
                    FLG_AUTORIZADOR = modelo.FLG_AUTORIZADOR,
                    FLG_INDEFINADO_ALTERNO = modelo.FLG_INDEFINADO_ALTERNO,
                    FLG_INDEFINADO_JEFE = modelo.FLG_INDEFINADO_ALTERNO,
                    FLG_ESTADO = flg_estado.ToString(),
                    NUMERO_DOCUMENTO_JEFE = modelo.NUMERO_DOCUMENTO_JEFE,
                    FECHA_DOCUMENTO_JEFE = modelo.FECHA_DOCUMENTO_JEFE,
                    OBSERVACION_JEFE = modelo.OBSERVACION_JEFE,
                    NUMERO_DOCUMENTO_ALTERNO = modelo.NUMERO_DOCUMENTO_ALTERNO,
                    FECHA_DOCUMENTO_ALTERNO = modelo.FECHA_DOCUMENTO_ALTERNO,
                    OBSERVACION_ALTERNO = modelo.OBSERVACION_ALTERNO,
                    FLG_TIPO = modelo.FLG_TIPO,
                    ID_GRUPO = modelo.ID_GRUPO == 0 ? null : modelo.ID_GRUPO,
                    USUARIO_CREACION = usuario_login,
                    USUARIO_MODIFICACION = usuario_login,
                    IP_CREACION = IP,
                    IP_MODIFICACION = IP,
                    FECHA_CREACION = GetFechaActual(DateTime.Now),
                    FECHA_MODIFICACION = GetFechaActual(DateTime.Now)
                };
                var id_personal_jefe = "";
                if (entidad.FLG_AUTORIZADOR == "1")
                {
                    id_personal_jefe = entidad.ID_PERSONA_JEFE;
                }
                else
                {
                    id_personal_jefe = entidad.ID_PERSONA_ALTERNO;
                }
                var existente = _evaluadorService.GetResponsable(Convert.ToInt32(entidad.ID_AREA_JEFE), Convert.ToInt32(entidad.ID_OFICINA_JEFE), id_personal_jefe).ToList();
                if (existente.Count() > 0)
                {
                    var itemExistente = existente.FirstOrDefault();
                    id = itemExistente.ID_EVALUADOR;
                    entidad.ID_EVALUADOR = id;
                }
                if (id <= 0)
                {
                    var respuesta = _evaluadorService.Insertar(entidad);
                    id = respuesta.ID_EVALUADOR;
                }
                else
                {
                    var respuesta = _evaluadorService.Actualizar(entidad);
                }
                if (id > 0)
                {
                    result.Success = true;
                    result.Message = "Se ha procesado correctamente";
                    result.Result = id.ToString();
                }

                if (result.Success)
                {
                    string correo_destino = "";
                    string nombre_responsable = "";
                    var entidad_responsable = _personaService.Detalle(id_personal_jefe);
                    if (entidad_responsable != null)
                    {
                        correo_destino = entidad_responsable.CORREO_INSTITUCIONAL;
                        nombre_responsable = entidad_responsable.NOMBRE_COMPLETO;
                    }
                    if (!string.IsNullOrEmpty(correo_destino))
                    {

                        // crear su rol del responsable
                        if (_configuracionSistemaBase.Seguridad.FlgCrearRol == "1")
                        {
                            string rol_responsable = getRolResponsable();

                            if (!string.IsNullOrEmpty(correo_destino))
                            {
                                var parametro = new UsuarioSeguridad
                                {
                                    ID_OFICINA = Convert.ToInt32(entidad.ID_OFICINA_JEFE),
                                    ID_PERSONA = id_personal_jefe,
                                    ID_SISTEMA = _configuracionSistemaBase.Seguridad.CodigoSistema,
                                    NOMBRE_ROL = rol_responsable,
                                    USU_CREACION = usuario_login
                                };
                                var crearRol = _usuarioService.GenerarUsuario(parametro);
                            }
                        }


                        // enviar correo electronico  
                        string mensaje = "Estimado(a): ";
                        mensaje += nombre_responsable;
                        mensaje += "<br><br>";
                        mensaje += TextoMensaje();
                        var url = GetBaseUrl() + "/Login/Index";
                        var html = CorreoElectronico.GenerarHtml_General(mensaje, url);
                        _configuracionSistemaBase.Correo.Copia = correo_remitente;
                        string asunto = "Responsable del Teletrabajo";
                        baseResponse = EmailSMTP.EnviarCorreoElectronico(_configuracionSistemaBase.Correo, correo_destino, "Oficina de Recursos Humanos", asunto, html, null);
                    }

                }

            }
            catch (Exception ex)
            {
                Log.CreateLogger(ex.Message);
                result.Message = ex.Message;
                result.Code = (int)HttpStatusCode.InternalServerError;
                result.Success = false;
                result.Result = "";
            }

            return Ok(result);



        }

        private string TextoMensaje()
        {
            string mensaje = "";

            mensaje += "Se le ha asignado el permiso como evaluador al sistema de ORH para la aprobación y/o rechazo del registro Teletrabajo.<br>";
            mensaje += "para ingresar al sistema debe ingresar su usuario y su clave de la red del ministerio.<br>";
            mensaje += "clic en el siguiente link.<br><br><br><br>";

            return mensaje;
        }

        public IActionResult ListarAutorizador([FromBody] EvaluadorConsultaModel request)
        {
            var result = new MethodResponseModel<IEnumerable<EvaluadorConsultaModel>> { Result = null };
            var lista = GetLista(request).ToList();
            result.Result = lista;
            return Ok(result);

        }


        private List<EvaluadorConsultaModel> GetLista(EvaluadorConsultaModel request)
        {
            var lista = _evaluadorService.GetAll(request.ID_AREA, request.ID_OFICINA,"").ToList();
            return lista;
        }
        private MethodResponseModel<IEnumerable<EvaluadorConsultaModel>> ListarReporte([FromBody] EvaluadorConsultaModel request)
        {

            var result = new MethodResponseModel<IEnumerable<EvaluadorConsultaModel>> { Result = null };
            try
            {
                var lista = GetLista(request);
                result.Result = lista;
                result.Success = lista.Count > 0;
            }
            catch (Exception ex)
            {
                Log.CreateLogger(ex.Message);
                result.Message = ex.Message;
                result.Code = (int)HttpStatusCode.InternalServerError;
                result.Result = new List<EvaluadorConsultaModel>();
            }
            return result;
        }
     

        #region  "Reporte"
        public IActionResult ExportarExcel(EvaluadorConsultaModel request)
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
                        rng.Value = "Listado de Evaluadores";
                    }

                    row += 1;
                    int fila = 1;
                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "Órgano", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 70; fila++;

                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "Unidad Orgánica", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 70; fila++;

                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "Evaluador", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 30; fila++;

                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "DNI", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 10; fila++;

                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "Correo", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 15; fila++;

                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "Sexo", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 10; fila++;

                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "F.Ingreso", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 15; fila++;

                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "Puesto", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 20; fila++;

                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "Regimen Laboral", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 20; fila++;


                    ws1.Row(row).Height = 30;

                    var data = this.ListarReporte(request);
                    if (data.Success)
                    {
                        if (data.Result.Count() > 0)
                        {

                            foreach (var item in data.Result)
                            {
                                row++;
                                int columna = 1;
                                ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.NOMBRE_AREA, row, columna, 0, 0, "L"); columna++;
                                ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.NOMBRE_OFICINA, row, columna, 0, 0, "L"); columna++;
                                ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.NOMBRE_COMPLETO, row, columna, 0, 0, "L"); columna++;
                                ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.NUMERO_DNI, row, columna, 0, 0, "L"); columna++;
                                ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.CORREO_INSTITUCIONAL, row, columna, 0, 0, "L"); columna++;
                                ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.ID_SEXO, row, columna, 0, 0, "L"); columna++;
                                ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.FECHA_INGRESO, row, columna, 0, 0, "L"); columna++;
                                ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.NOMBRE_CARGO, row, columna, 0, 0, "L"); columna++;
                                ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.NOMBRE_CATEGORIA, row, columna, 0, 0, "L"); columna++;
                            }
                        }
                    }

                    string strFileName = "Evaluadores" + DateTime.Now.ToString() + ".xlsx";
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
        public IActionResult ProcesarMarcaMasivo()
        {
            var result = new MethodResponseModel<BaseResponse> { };
            try
            {
                if (Request.Form.Files.Count() > 0)
                {
                    var file = Request.Form.Files["FileArchivo"];
                    var nombreArchivo = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var extensionArchivo = Path.GetExtension(nombreArchivo);
                    byte[] fileBytes = null;
                    using (var fileStream = file.OpenReadStream())
                    using (var ms = new MemoryStream())
                    {
                        fileStream.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }
                    string rutaBse = _webHostEnvironment.ContentRootPath;
                    var item = new EvaluadorModel();

                    // RedData(fileBytes);
                    BaseResponse respuesta = ProcesarHojaCalculo(item, rutaBse, nombreArchivo, fileBytes);

                    result.Result = respuesta;
                    result.Success = respuesta.Success;
                    result.Message = respuesta.Message;

                }

            }
            catch (Exception ex)
            {
                Log.CreateLogger(ex.Message);
                result.Message = ex.Message;
                result.Code = (int)HttpStatusCode.InternalServerError;
                result.Result = new BaseResponse();
                result.Success = false;
            }
            return Ok(result);
        }
        public BaseResponse ProcesarHojaCalculo(EvaluadorModel entidad, string rutaBase, string nombreArchivo, byte[] bytes)
        {
            BaseResponse respuesta = new BaseResponse();
            try
            {
                if (string.IsNullOrWhiteSpace(nombreArchivo) == true)
                {
                    throw new Exception("No existe el archivo");
                }
                string rutaBse = _webHostEnvironment.ContentRootPath;

                var folder = rutaBse + "\\Helper";

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                folder += "\\Temporal";
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                nombreArchivo = this.GuardarArchivo(rutaBse, nombreArchivo, bytes);

                List<EvaluadorModel> items = new List<EvaluadorModel>();
                using (SpreadsheetDocument document = SpreadsheetDocument.Open(nombreArchivo, true))
                {
                    Workbook libro = document.WorkbookPart.Workbook;
                    IEnumerable<Sheet> hojas = libro.Descendants<Sheet>();
                    string hojaId = hojas.First(s => s.LocalName == @"sheet").Id;
                    WorksheetPart hoja = (WorksheetPart)document.WorkbookPart.GetPartById(hojaId);
                    SharedStringTable tabla = document.WorkbookPart.SharedStringTablePart.SharedStringTable;
                    items = ProcesarHojaCalculo(hoja.Worksheet, tabla);
                    respuesta = this.GuardarMasivo(items);

                    if (respuesta.Success)
                    {
                        // enviar correo 
                        string correo_destino = respuesta.Extra4;
                        if (string.IsNullOrEmpty(correo_destino))
                        {
                            string mensaje = "Estimados:";
                            //mensaje += correo_destino;
                            mensaje += "<br><br>";
                            mensaje += TextoMensaje();
                            var url = GetBaseUrl() + "/Login/Index";
                            var html = CorreoElectronico.GenerarHtml_General(mensaje, url);
                            string id_personal = "";
                            string usuario_remitente = "Oficina de ORH";
                            if (UsuarioActual != null)
                            {
                                usuario_remitente = UsuarioActual.Nombre;
                                id_personal = UsuarioActual.ID_PERSONAL;
                            }
                            var usuario_conectado = _personaService.Detalle(id_personal);
                            if (usuario_conectado != null)
                            {
                                _configuracionSistemaBase.Correo.Copia = usuario_conectado.CORREO_INSTITUCIONAL;
                            }
                            string asunto = "Responsable del Teletrabajo";
                            baseResponse = EmailSMTP.EnviarCorreoElectronico(_configuracionSistemaBase.Correo, correo_destino, "Oficina de Recursos Humanos", asunto, html, null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;
                respuesta.Success = false;
                Log.CreateLogger(ex.Message);
            }
            finally
            {
                this.EliminarArchivo(nombreArchivo);
            }
            return respuesta;
        }
        private List<EvaluadorModel> ProcesarHojaCalculo(Worksheet hoja, SharedStringTable tabla)
        {
            try
            {
                IEnumerable<Row> registros = this.GetRowsGreaterEqualThan(hoja, 2);
                List<EvaluadorModel> items = new List<EvaluadorModel>();
                int reg = 0;
                var listaPersona = _personaService.GetTodos();
                var listaOficinas = _oficinaService.GetOrganosTodos();
                var listaRegistrados = _evaluadorService.GetAll();
                foreach (Row registro in registros)
                {
                    reg++;
                    String[] valores = GetRowValue(tabla, registro);
                    if (valores.Length == 0) continue;

                    if (valores.Length != 2)
                    {
                        throw new Exception("El formato contiene 2 columnas DNI y SIGLA OFICINA</br>NO acepta campos vacios.");
                    }
                    string DNI = Convert.ToString(valores[0]);
                    string SIGLA_OFICINA = Convert.ToString(valores[1]);


                    if (!string.IsNullOrEmpty(DNI))
                    {
                        DNI = DNI.Trim();
                    }
                    var objetoPersona = listaPersona.Where(p => p.NUMERO_DNI == DNI).ToList().FirstOrDefault();

                    if (objetoPersona == null)
                    {
                        throw new Exception("N° DNI " + DNI + " no se encuentró en el sistema de BITACORAS");
                    }

                    if (!string.IsNullOrEmpty(SIGLA_OFICINA))
                    {
                        SIGLA_OFICINA = SIGLA_OFICINA.Trim();
                    }
                    var objetoOficina = listaOficinas.Where(p => p.SIGLA == SIGLA_OFICINA).FirstOrDefault();
                    if (objetoOficina == null)
                    {
                        throw new Exception("La SIGLA " + SIGLA_OFICINA + " del DNI " + DNI + " no se encuentró en el sistema de BITACORAS");
                    }
                    var objetoExisteResponsable = listaRegistrados.Where(p => (p.ID_PERSONA_JEFE == objetoPersona.ID_PERSONAL || p.ID_PERSONA_ALTERNO == objetoPersona.ID_PERSONAL) &&
                                                                              p.ID_AREA_JEFE == objetoOficina.ID_AREA.ToString() &&
                                                                              p.ID_OFICINA_JEFE == objetoOficina.ID_OFICINA.ToString()).FirstOrDefault();
                    if (objetoExisteResponsable != null)
                    {
                        throw new Exception("El N° DNI " + DNI + " ya se encuentra asignado a la " + SIGLA_OFICINA + "<br>" + " El cambio del autorizador debe realizar individualmente.");
                    }

                    if (!string.IsNullOrEmpty(DNI))
                    {
                        if (!string.IsNullOrEmpty(SIGLA_OFICINA))
                        {


                            var item = new EvaluadorModel();
                            item.ID_OFICINA_JEFE = objetoOficina.ID_OFICINA.ToString();// modelo.ID_OFICINA_JEFE == "0" ? modelo.ID_AREA_JEFE : modelo.ID_OFICINA_JEFE,
                            item.ID_AREA_JEFE = objetoOficina.ID_AREA.ToString();//modelo.ID_AREA_JEFE,
                            item.ID_PERSONA_JEFE = objetoPersona.ID_PERSONAL;//modelo.ID_PERSONA_JEFE,
                            item.ID_PERSONA_ALTERNO = null;//modelo.ID_PERSONA_ALTERNO,
                            item.FECHA_INICIO_JEFE = DateTime.Now;//modelo.FECHA_INICIO_JEFE,
                            item.FECHA_FINAL_JEFE = null;///modelo.FECHA_FINAL_JEFE,
                            item.FECHA_INICIO_ALTERNO = null;//modelo.FECHA_INICIO_ALTERNO,
                            item.FECHA_FINAL_ALTERNO = null;//modelo.FECHA_FINAL_ALTERNO,
                            item.FLG_AUTORIZADOR = "1";// modelo.FLG_AUTORIZADOR,
                            item.FLG_INDEFINADO_ALTERNO = null; //modelo.FLG_INDEFINADO_ALTERNO,
                            item.FLG_INDEFINADO_JEFE = "2";//modelo.FLG_INDEFINADO_ALTERNO,
                            item.FLG_ESTADO = "1";
                            item.NUMERO_DOCUMENTO_JEFE = null;//modelo.NUMERO_DOCUMENTO_JEFE,
                            item.FECHA_DOCUMENTO_JEFE = null;// modelo.FECHA_DOCUMENTO_JEFE,
                            item.OBSERVACION_JEFE = null;// modelo.OBSERVACION_JEFE,
                            item.NUMERO_DOCUMENTO_ALTERNO = null;// modelo.NUMERO_DOCUMENTO_ALTERNO,
                            item.FECHA_DOCUMENTO_ALTERNO = null;//modelo.FECHA_DOCUMENTO_ALTERNO,
                            item.OBSERVACION_ALTERNO = null;// modelo.OBSERVACION_ALTERNO,
                            item.FLG_TIPO = "2";//  Unidad Organica | objetoOficina.ID_AREA == objetoOficina.ID_OFICINA ? "1" : "2";
                            item.ID_GRUPO = 0;//modelo.ID_GRUPO == 0 ? null : modelo.ID_GRUPO,
                            item.CORREO_INSTITUCIONAL = objetoPersona.CORREO_INSTITUCIONAL;
                            item.NOMBRE_COMPLETO = objetoPersona.NOMBRE_COMPLETO;
                            items.Add(item);

                        }
                    }
                }

                if (items.Count <= 0)
                {
                    throw new Exception("No hay responsable válido para el registro");
                }
                return items;
            }
            catch (Exception ex)
            {
                Log.CreateLogger(ex.Message);
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        protected IEnumerable<Row> GetRowsGreaterEqualThan(Worksheet sheet, int index)
        {
            IEnumerable<Row> rows = from row in sheet.Descendants<Row>()
                                    where row.RowIndex >= index //The table begins on line 5
                                    select row;
            return rows;
        }
        protected string[] GetRowValue(SharedStringTable table, Row row)
        {
            List<String> values = new List<string>();
            foreach (Cell cell in from cell in row.Descendants<Cell>() where cell.CellValue != null select cell)
            {
                if (cell.DataType != null && cell.DataType.HasValue && cell.DataType == CellValues.SharedString)
                { values.Add(table.ChildElements[int.Parse(cell.CellValue.InnerText)].InnerText); }
                else
                { values.Add(cell.CellValue.InnerText); }
            }

            return values.ToArray();
        }


        private string getRolResponsable()
        {

            string rol = "";
            try
            {
                var entity_rol = _variableService.ListarVariable("TAREO", "ROL_RESPONSABLE").Where(x => x.FLG_ESTADO == "1").FirstOrDefault();

                if (entity_rol != null)
                {
                    rol = entity_rol.VALOR.Trim().ToUpper();
                }
            }
            catch (Exception)
            {
                rol = "";
            }
            return rol;
        }
        public BaseResponse GuardarMasivo(List<EvaluadorModel> items)
        {
            BaseResponse respuesta = new BaseResponse();
            string usuario_login = UsuarioActual == null ? "" : UsuarioActual.UsuarioLogin;
            string rol_responsable = getRolResponsable();
            string correos = "";
            foreach (var item in items)
            {

                var entidad = new Evaluador
                {
                    ID_EVALUADOR = item.ID_EVALUADOR,
                    ID_OFICINA_JEFE = item.ID_OFICINA_JEFE == "0" ? item.ID_AREA_JEFE : item.ID_OFICINA_JEFE,
                    ID_AREA_JEFE = item.ID_AREA_JEFE,
                    ID_PERSONA_JEFE = item.ID_PERSONA_JEFE,
                    ID_PERSONA_ALTERNO = item.ID_PERSONA_ALTERNO,
                    FECHA_INICIO_JEFE = item.FECHA_INICIO_JEFE,
                    FECHA_FINAL_JEFE = item.FECHA_FINAL_JEFE,
                    FECHA_INICIO_ALTERNO = item.FECHA_INICIO_ALTERNO,
                    FECHA_FINAL_ALTERNO = item.FECHA_FINAL_ALTERNO,
                    FLG_AUTORIZADOR = item.FLG_AUTORIZADOR,
                    FLG_INDEFINADO_ALTERNO = item.FLG_INDEFINADO_ALTERNO,
                    FLG_INDEFINADO_JEFE = item.FLG_INDEFINADO_ALTERNO,
                    FLG_ESTADO = item.FLG_ESTADO.ToString(),
                    NUMERO_DOCUMENTO_JEFE = item.NUMERO_DOCUMENTO_JEFE,
                    FECHA_DOCUMENTO_JEFE = item.FECHA_DOCUMENTO_JEFE,
                    OBSERVACION_JEFE = item.OBSERVACION_JEFE,
                    NUMERO_DOCUMENTO_ALTERNO = item.NUMERO_DOCUMENTO_ALTERNO,
                    FECHA_DOCUMENTO_ALTERNO = item.FECHA_DOCUMENTO_ALTERNO,
                    OBSERVACION_ALTERNO = item.OBSERVACION_ALTERNO,
                    FLG_TIPO = item.FLG_TIPO,
                    ID_GRUPO = item.ID_GRUPO == 0 ? null : item.ID_GRUPO,
                    USUARIO_CREACION = usuario_login,
                    USUARIO_MODIFICACION = usuario_login,
                    IP_CREACION = IP,
                    IP_MODIFICACION = IP,
                    FECHA_CREACION = DateTime.Now,
                    FECHA_MODIFICACION = DateTime.Now
                };
                var valor = _evaluadorService.Insertar(entidad);

                var id = valor.ID_EVALUADOR;

                if (id > 0)
                {

                    if (_configuracionSistemaBase.Seguridad.FlgCrearRol == "1")
                    {
                        if (!string.IsNullOrEmpty(item.CORREO_INSTITUCIONAL))
                        {
                            var parametro = new UsuarioSeguridad
                            {
                                ID_OFICINA = Convert.ToInt32(entidad.ID_OFICINA_JEFE),
                                ID_PERSONA = entidad.ID_PERSONA_JEFE,
                                ID_SISTEMA = _configuracionSistemaBase.Seguridad.CodigoSistema,
                                NOMBRE_ROL = rol_responsable,
                                USU_CREACION = usuario_login
                            };
                            var crearRol = _usuarioService.GenerarUsuario(parametro);
                        }
                    }
                    respuesta.Success = true;
                    respuesta.Message = "Se ha procesado correctamente";

                    if (string.IsNullOrEmpty(correos))
                    {
                        correos = item.CORREO_INSTITUCIONAL;
                    }
                    else
                    {
                        correos = correos + "," + item.CORREO_INSTITUCIONAL;
                    }
                }
            }
            respuesta.Extra4 = correos;

            return respuesta;


        }





        [HttpPost]
        public IActionResult Anular([FromBody] EvaluadorModel modelo)
        {
            var flg_estado = modelo.FLG_ESTADO;
            var result = new MethodResponseModel<string> { };
            try
            {

                if (flg_estado == "0")
                {
                    result.Message = "Se ha anulado correctamente";
                }
                else
                {
                    result.Message = "Se ha activado correctamente";
                }
                var id = modelo.ID_EVALUADOR;
                if (id > 0)
                {
                    var respuesta = _evaluadorService.Anular(new Evaluador()
                    {
                        ID_EVALUADOR = modelo.ID_EVALUADOR,
                        FLG_ESTADO = flg_estado.ToString(),
                        FECHA_CREACION = DateTime.Now,
                        FECHA_MODIFICACION = DateTime.Now,
                        IP_CREACION = this.IP,
                        IP_MODIFICACION = this.IP
                    });
                    result.Success = true;
                    result.Result = id.ToString();
                    if (_configuracionSistemaBase.Seguridad.FlgCrearRol == "1")
                    {
                        string rol_responsable = getRolResponsable();
                        if (!string.IsNullOrEmpty(respuesta.ID_PERSONA_JEFE))
                        {
                            var parametro = new UsuarioSeguridad
                            {
                                ID_PERSONA = respuesta.ID_PERSONA_JEFE,
                                ID_SISTEMA = _configuracionSistemaBase.Seguridad.CodigoSistema,
                                NOMBRE_ROL = rol_responsable
                            };
                            var anularRol = _usuarioService.AnularUsuario(parametro);
                        }
                        if (!string.IsNullOrEmpty(respuesta.ID_PERSONA_ALTERNO))
                        {
                            var parametro = new UsuarioSeguridad
                            {
                                ID_PERSONA = respuesta.ID_PERSONA_ALTERNO,
                                ID_SISTEMA = _configuracionSistemaBase.Seguridad.CodigoSistema,
                                NOMBRE_ROL = rol_responsable
                            };
                            var anularRol = _usuarioService.AnularUsuario(parametro);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Log.CreateLogger(ex.Message);
                result.Message = ex.Message;
                result.Code = (int)HttpStatusCode.InternalServerError;
                result.Result = "";
            }
            return Ok(result);



        }


     

        public IActionResult ListarDetalleAutorizador([FromBody] EvaluadorModel parametro)
        {
            var modelo = new EvaluadorModel();
            try
            {
                var detalle = _evaluadorService.Detalle(parametro.ID_EVALUADOR);
                if (detalle != null)
                {
                    modelo.ID_EVALUADOR = detalle.ID_EVALUADOR;
                    modelo.ID_AREA_JEFE = detalle.ID_AREA_JEFE.ToString();
                    modelo.ID_OFICINA_JEFE = detalle.ID_OFICINA_JEFE.ToString();
                    modelo.ID_PERSONA_JEFE = detalle.ID_PERSONA_JEFE;
                    modelo.FLG_AUTORIZADOR = detalle.FLG_AUTORIZADOR;
             
                }
            }
            catch (Exception ex)
            {
                Log.CreateLogger(ex.Message);
            }
            return Ok(modelo);
        }




    }
}
