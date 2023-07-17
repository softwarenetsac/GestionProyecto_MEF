using Gestion_Rendimiento_Entity;
using Gestion_Rendimiento_Entity.Model;
using Gestion_Rendimiento_Frontend.Extensiones;
using Gestion_Rendimiento_IService;
using Gestion_Rendimiento_Service;
using Microsoft.AspNetCore.Hosting;
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

namespace Gestion_Rendimiento_Frontend.Controllers
{
    public class EvaluadoController : BaseController
    {
        private readonly IEvaluadoService _evaluadoService;
        private readonly IOficinaService _oficinaService;
        public EvaluadoController(
          IOficinaService oficinaService,
          IEvaluadoService evaluadoService,
          IOptions<ConfiguracionSistemaModel> configuracionSistema,
           IHttpContextAccessor contextAccessor)
        {
            _oficinaService = oficinaService;
            _evaluadoService = evaluadoService;
            _configuracionSistemaBase = configuracionSistema.Value;
            _contextAccessor = contextAccessor;
        }
        public IActionResult Index()
        {
            var tipo = Constantes.TipoOrgano;
            var modelo = new EvaluadoViewModel();
            modelo.Oficinas = _oficinaService.GetOrganos(tipo).ToList();
            return View(modelo);
        }
        private List<EvaluadoModel> GetLista(EvaluadoModel request)
        {
            EvaluadoModel modelo = new EvaluadoModel();
            modelo.ID_AREA = request.ID_AREA;
            modelo.ID_OFICINA = request.ID_OFICINA;
            var lista = _evaluadoService.GetAll(modelo).ToList();
            return lista;
        }
        public IActionResult GetAll([FromBody] EvaluadoModel request)
        {
            var result = new MethodResponseModel<IEnumerable<EvaluadoModel>> { Result = null };
            var lista = GetLista(request).ToList();
            result.Result = lista;
            return Ok(result);

        }
        private MethodResponseModel<IEnumerable<EvaluadoModel>> ListarReporte([FromBody] EvaluadoModel request)
        {
            var result = new MethodResponseModel<IEnumerable<EvaluadoModel>> { Result = null };
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
                result.Result = new List<EvaluadoModel>();
            }
            return result;
        }
        public IActionResult ExportarExcel(EvaluadoModel request)
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
                        rng.Value = "Lista de Evaluados";
                    }

                    row += 1;
                    int fila = 1;
                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "Apellidos y Nombres", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 50; fila++;

                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "N° Documento", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 10; fila++;

                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "F. Ingreso", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 15; fila++;

                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "Cargo", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 50; fila++;

                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "Categoría", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 15; fila++;

                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "Correo", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 30; fila++;

                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "Unidad Orgánica", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 50; fila++;

                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "Órgano", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 50; fila++;

                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "Año", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 10; fila++;

                    //ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "Proyecto", row, fila, 0, 0, "L", true);
                    //ws1.Column(fila).Width = 50; fila++;

                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "Evaluador", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 50; fila++;

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
                                ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.NOMBRE_COMPLETO, row, columna, 0, 0, "L"); columna++;
                                ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.NUMERO_DNI, row, columna, 0, 0, "L"); columna++;
                                ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.FECHA_INGRESO, row, columna, 0, 0, "L"); columna++;
                                ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.NOMBRE_CARGO, row, columna, 0, 0, "L"); columna++;
                                ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.NOMBRE_CATEGORIA, row, columna, 0, 0, "L"); columna++;
                                ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.CORREO_INSTITUCIONAL, row, columna, 0, 0, "L"); columna++;
                                ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.NOMBRE_OFICINA, row, columna, 0, 0, "L"); columna++;
                                ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.NOMBRE_AREA, row, columna, 0, 0, "L"); columna++;
                                ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.ANIO, row, columna, 0, 0, "L"); columna++;
                                //ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.PROYECTO, row, columna, 0, 0, "L"); columna++;
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
    }
}
