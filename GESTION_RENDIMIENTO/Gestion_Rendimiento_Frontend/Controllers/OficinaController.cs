using Gestion_Rendimiento_Common;
using Gestion_Rendimiento_Entity;
using Gestion_Rendimiento_Entity.Model;
using Gestion_Rendimiento_Entity.Models;
using Gestion_Rendimiento_Entity.Request;
using Gestion_Rendimiento_Frontend.Extensiones;
using Gestion_Rendimiento_Frontend.Seguridad;
using Gestion_Rendimiento_IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Gestion_Rendimiento_Frontend.Controllers
{
    [CustomAuthorization]
    public class OficinaController : BaseController
    {
        private readonly IOficinaService _oficinaService;

        public OficinaController(
        IOficinaService oficinaService,
          IOptions<ConfiguracionSistemaModel> configuracionSistema,
           IHttpContextAccessor contextAccessor
            )
        {
            _oficinaService = oficinaService;
            _configuracionSistemaBase = configuracionSistema.Value;
            _contextAccessor = contextAccessor;
        }



        public IActionResult Index()
        {

            var tipo = Constantes.TipoOrgano;
            var modelo = new ParametroGeneralViewModel
            {
                Oficinas = _oficinaService.GetOrganos(tipo).ToList(),
            };
            return View(modelo);
        }


        public IActionResult GetOrgano()
        {

            var result = new MethodResponseModel<IEnumerable<Oficina>> { Result = null };
            try
            {
                var data = _oficinaService.GetOrganos(Constantes.TipoOrgano);
                result.Result = data;

            }
            catch (Exception ex)
            {
                // _logger.LogError(ex.Message);
                result.Message = ex.Message;
                result.Code = (int)HttpStatusCode.InternalServerError;
                result.Result = null;
            }
            return Ok(result);

        }



        public IActionResult GetOrganoTodos()
        {
            var result = new MethodResponseModel<IEnumerable<Oficina>> { Result = null };
            try
            {
                var data = _oficinaService.GetOrganosTodos();
                result.Result = data;
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex.Message);
                result.Message = ex.Message;
                result.Code = (int)HttpStatusCode.InternalServerError;
                result.Result = null;
            }
            return Ok(result);

        }


        public IActionResult ListarTodos([FromBody] OficinaModel request)
        {
            var result = new MethodResponseModel<IEnumerable<Oficina>> { Result = null };
            try
            {
                var data = _oficinaService.GetTodos(request);
                result.Result = data;
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex.Message);
                result.Message = ex.Message;
                result.Code = (int)HttpStatusCode.InternalServerError;
                result.Result = new List<Oficina>();
            }
            return Ok(result);
        }





        public IActionResult ListarUnidadOrganica([FromBody] ParametroRequest request)
        {
            int id = 0;

            if (request != null)
            {
                if (string.IsNullOrEmpty(request.ID))
                {
                    id = 0;
                }
                id = Convert.ToInt32(request.ID);
            }


            var result = new MethodResponseModel<IEnumerable<Oficina>> { Result = null };
            try
            {
                var data = _oficinaService.GetUnidadOrganicas(id, Constantes.TipoUnidadOrganica);
                result.Result = data;
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex.Message);
                result.Message = ex.Message;
                result.Code = (int)HttpStatusCode.InternalServerError;
                result.Result = new List<Oficina>();
            }
            return Ok(result);
        }

        #region  EXCEL


        public IActionResult ExportarExcel(OficinaModel modelo)
        {

            string fecha_imprimir = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            string extension = ".xlsx";
            try
            {

                var data = new List<Oficina>();
                data = _oficinaService.GetTodos(modelo).ToList();
                string nombre_file = "OFICINAS " + fecha_imprimir + extension;
                var array = GetData(data);

                return FileDownload(array, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombre_file);

            }
            catch (Exception ex)
            {
                Log.CreateLogger(ex.Message);
                throw new Exception(ex.Message);
            }
        }






        private byte[] GetData(List<Oficina> data)
        {

            byte[] array = null;

            //generamos el reporte
            //  ExcelPackage.LicenseContext = LicenseContext.Commercial;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            int row = 3;
            using (ExcelPackage package = new ExcelPackage())
            {
                var ws1 = package.Workbook.Worksheets.Add("Informacion");
                //creamos el titulo del reporte
                using (var rng = ws1.Cells[2, 1, 2, 10])
                {
                    // rng.Style.Font.Bold = true;
                    // rng.Style.WrapText = true;
                    rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    rng.Style.Font.Size = 15;
                    rng.Merge = true;
                    rng.Value = "LISTADO DE ORGANO / UNIDAD ORGANICA";
                }



                row += 1;
                int fila = 1;
                ws1.Row(row).Height = 34;
                ExcelUtil.CeldaFormatoEtiqueta_CabeceraColor(ws1, "Nª", row, fila, 0, 0, "C", true);
                ws1.Column(fila).Width = 5; fila++;
                ExcelUtil.CeldaFormatoEtiqueta_CabeceraColor(ws1, "Id Oficina", row, fila, 0, 0, "C", true);
                ws1.Column(fila).Width = 15; fila++;
                ExcelUtil.CeldaFormatoEtiqueta_CabeceraColor(ws1, "Codigo Padre", row, fila, 0, 0, "C", true);
                ws1.Column(fila).Width = 18; fila++;
                ExcelUtil.CeldaFormatoEtiqueta_CabeceraColor(ws1, "Código Oficina", row, fila, 0, 0, "C", true);
                ws1.Column(fila).Width = 18; fila++;
                ExcelUtil.CeldaFormatoEtiqueta_CabeceraColor(ws1, "Órgano", row, fila, 0, 0, "L", true);
                ws1.Column(fila).Width = 90; fila++;
                ExcelUtil.CeldaFormatoEtiqueta_CabeceraColor(ws1, "Unidad Orgánica", row, fila, 0, 0, "L", true);
                ws1.Column(fila).Width = 90; fila++;
                ExcelUtil.CeldaFormatoEtiqueta_CabeceraColor(ws1, "SIGLA", row, fila, 0, 0, "C", true);
                ws1.Column(fila).Width = 15; fila++;
                ExcelUtil.CeldaFormatoEtiqueta_CabeceraColor(ws1, "Tipo", row, fila, 0, 0, "C", true);
                ws1.Column(fila).Width = 20; fila++;

                if (data.Count() > 0)
                {
                    int Nro = 1;
                    foreach (var item in data)
                    {
                        row++;
                        int columna = 1;
                        ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, Nro, row, columna, 0, 0, "C"); columna++;
                        ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.ID_OFICINA, row, columna, 0, 0, "C"); columna++;
                        ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.CODIGO_PADRE, row, columna, 0, 0, "C"); columna++;
                        ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.CODIGO_OFICINA, row, columna, 0, 0, "C"); columna++;
                        ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, (item.NOMBRE_AREA == null ? "" : item.NOMBRE_AREA), row, columna, 0, 0, "L"); columna++;
                        ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, (item.NOMBRE_OFICINA == null ? "" : item.NOMBRE_OFICINA), row, columna, 0, 0, "L"); columna++;
                        ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, (item.SIGLA == null ? "" : item.SIGLA.Trim()), row, columna, 0, 0, "L"); columna++;
                        ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, (item.TIPO_NOMBRE == null ? "" : item.TIPO_NOMBRE), row, columna, 0, 0, "L"); columna++;
                        Nro++;
                    }
                }

                array = package.GetAsByteArray();



            }
            return array;
        }

        #endregion

    }
}
