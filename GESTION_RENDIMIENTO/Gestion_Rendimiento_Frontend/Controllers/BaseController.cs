using Gestion_Rendimiento_Common;
using Gestion_Rendimiento_Entity;
using Gestion_Rendimiento_Entity.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_Frontend.Controllers
{
    public class BaseController : Controller
    {
        protected BaseResponse baseResponse;
        protected ConfiguracionSistemaModel _configuracionSistemaBase;
        protected readonly CultureInfo _provider;
        protected ILogger _logger;
        protected IHttpContextAccessor _contextAccessor;

        public BaseController()
        {
            _provider = new CultureInfo("es-ES");
        }
        //string userDesignation = ((ClaimsIdentity)User.Identity).FindFirst("OperationType").Value;
        // string userImage = ((ClaimsIdentity)User.Identity).FindFirst("ImageLink").Value;


        public UsuarioSesion UsuarioActual => AdministraSession();

        UsuarioSesion AdministraSession()
        {

            var Id_Personal = this.User.Claims.FirstOrDefault(c => c.Type == "Id_Personal")?.Value;
            var usuario_nombre = this.User.Claims.FirstOrDefault(c => c.Type == "Nombre")?.Value;
            var menu = this.User.Claims.FirstOrDefault(c => c.Type == "Menu")?.Value;
            var autenticacion = this.User.Claims.FirstOrDefault(c => c.Type == "Autenticacion")?.Value;
            var login = this.User.Claims.FirstOrDefault(c => c.Type == "Login")?.Value;
            var id_oficina = this.User.Claims.FirstOrDefault(c => c.Type == "ID_OFICINA")?.Value;
            UsuarioSesion session = new UsuarioSesion
            {
                UsuarioLogin = login,
                ID_PERSONAL = Id_Personal,
                Nombre = usuario_nombre,
                Autenticacion = autenticacion,
                ID_OFICINA = string.IsNullOrEmpty(id_oficina) ? "0" : id_oficina
            };

            return session;
        }

        protected DateTime PrimerDiaMes(DateTime date)
        {

            DateTime primerDiaMes = new DateTime(date.Year, date.Month, 1);
            return primerDiaMes;

        }

        protected DateTime UltimoDiaMes(DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
        }

        public static int ObtenerLongitudArchivo(string longitud = "10")
        {
            int longitudArchivo = 0;
            try
            {
                if (string.IsNullOrEmpty(longitud))
                {
                    string longitud_Archivo = longitud;
                    if (!string.IsNullOrEmpty(longitud_Archivo))
                        longitudArchivo = Convert.ToInt32(longitud);
                }
            }
            catch (Exception)
            {
                longitudArchivo = 0;
            }

            return longitudArchivo;
        }

        public static double ConvertirMegasABytes(int longitud)
        {
            var longitudBytes = 0;
            try
            {
                if (longitud > 0)
                {
                    longitudBytes = longitud * 1024 * 1024;
                }

            }
            catch (Exception)
            {
                longitudBytes = 0;
            }
            return longitudBytes;
        }


        protected PaginationModel GetPagination()
        {

            var pagination = new PaginationModel();
            pagination.order = new List<Order>();
            pagination.search = new Search();
            // https://www.c-sharpcorner.com/article/using-datatables-grid-with-asp-net-mvc/

            var draw = Request.Form["draw"] == DBNull.Value ? "0" : Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"] == DBNull.Value ? "0" : Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"] == DBNull.Value ? "0" : Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"] == DBNull.Value ? "" : Request.Form["search[value]"].FirstOrDefault();
            //Paging Size (10,20,50,100)    
            //  int pageSize = length != null ? Convert.ToInt32(length) : 0;
            //  int skip = start != null ? Convert.ToInt32(start) : 0;
            //   int recordsTotal = 0;
            var sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDir = Request.Form["order[0][dir]"].FirstOrDefault();
            pagination.draw = Convert.ToInt32(draw);
            pagination.start = Convert.ToInt32(start);
            pagination.length = Convert.ToInt32(length);
            pagination.search.value = searchValue;

            var order = new Order
            {
                sortColum = true,
                name = string.Empty
            };

            if (!string.IsNullOrEmpty(sortColumnName))
            {
                order.name = sortColumnName;
            }
            if (!string.IsNullOrEmpty(sortColumnDir))
            {
                if (sortColumnDir.ToUpper() != "ASC")
                {
                    order.sortColum = false;
                }
            }
            pagination.order.Add(order);
            return pagination;
        }




   







        public virtual DateTime IsDate(string inputDate)
        {
            DateTime dateValue;
            try
            {

                dateValue = DateTime.ParseExact(inputDate, "dd/MM/yyyy HH:mm", null);

            }
            catch
            {
                dateValue = DateTime.ParseExact(inputDate, "dd/MM/yyyy", null);
            }
            return dateValue;
        }

        public virtual bool IsDate(string inputDate, string tipo = "0")
        {
            bool isDate = true;
            DateTime dateValue;
            try
            {
                dateValue = DateTime.ParseExact(inputDate, "dd/MM/yyyy HH:mm", null);

            }
            catch
            {

                try
                {
                    dateValue = DateTime.ParseExact(inputDate, "dd/MM/yyyy", null);

                }
                catch
                {
                    isDate = false;
                }
            }
            return isDate;
        }


        #region Utilities
        public string IP => this.HttpContext.Connection.RemoteIpAddress?.ToString();

        public string Servidor => this.Request.Host.Value;
        #endregion

        #region [Virtual Base]
        public virtual FileContentResult FileDownload(byte[] fileContents, string contentType, string fileDownloadName)
        => new FileContentResult(fileContents, contentType) { FileDownloadName = fileDownloadName };
        public virtual FileStreamResult FilePreview(byte[] fileContents, string contentType, string fileDownloadName)
        {
            //MemoryStream ms = new MemoryStream(fileContents);
            //return new FileStreamResult(ms, contentType);

            try
            {
                return new FileStreamResult(new MemoryStream(fileContents), contentType);
            }
            catch (Exception ex)
            {
                Log.CreateLogger(ex.Message);
                return null;
            }
        }

        #endregion


        #region "Configuracion Celda"




        //public SessionEntity SetDataSession()
        //{
        //    SessionEntity data = null;
        //    try
        //    {
        //        //  string ejecucion = _configuracionSistemaBase.Seguridad.EjecucionLocal;
        //        var user = _contextAccessor.HttpContext.User.Identity.IsAuthenticated;

        //        if (user)
        //        {
        //            SessionEntity session = _contextAccessor == null ? null : _contextAccessor.HttpContext.Session.GetObjectFromJson<SessionEntity>(Constantes.UsuarioSesion) ?? new SessionEntity();
        //            // UsuarioActual = session;
        //            if (session == null)
        //            {
        //                string token = "";
        //                if (HttpContext != null)
        //                {
        //                    if (HttpContext.Request != null)
        //                    {
        //                        if (HttpContext.Request.Cookies != null)
        //                        {
        //                            token = HttpContext.Request.Cookies["user"];
        //                        }
        //                    }
        //                }
        //                if (!string.IsNullOrEmpty(token))
        //                {
        //                    session = Reconexion(token);
        //                    //   UsuarioActual = session;
        //                }
        //            }

        //            data = session;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        _logger.LogInformation(ex.Message);
        //    }

        //    return data;
        //}



        /*

        protected void celdaFormatoEtiqueta_Numero(ExcelWorksheet ws1, object valor, int fila1, int columna1, int fila2 = 0, int columna2 = 0, string alineacion = "C", string formatoDecimal = "D")
        {
            try
            {
                string formato = "#,##0.00;[red]-#,##0.00;";
                if (formatoDecimal == "E") // entero
                {
                    // sin decimal
                    formato = "#,##0.00;[red]-#,##0;";

                }
                //   formato += "-";
                valor = valor == null ? 0 : valor;
                valor = valor == "" ? 0 : valor;


                fila2 = (fila2 == 0 ? fila1 : fila2);
                columna2 = (columna2 == 0 ? columna1 : columna2);
                using (var rng = ws1.Cells[fila1, columna1, fila2, columna2])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.WrapText = true;
                    rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    if (alineacion == "L")
                    {
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    }
                    if (alineacion == "D")
                    {
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    }
                    if (alineacion == "J")
                    {
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Justify;
                    }
                    if (alineacion == "C")
                    {
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    rng.Style.Font.Size = 11;
                    rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Bottom.Color.SetColor(Color.FromArgb(0, 0, 0));
                    rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Left.Color.SetColor(Color.FromArgb(0, 0, 0));
                    rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Right.Color.SetColor(Color.FromArgb(0, 0, 0));
                    rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Top.Color.SetColor(Color.FromArgb(0, 0, 0));
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 255, 255));
                    rng.Style.Font.Color.SetColor(Color.FromArgb(0, 0, 0));
                    if (fila2 - fila1 > 0 || columna2 - columna1 > 0)
                    {
                        rng.Merge = true;
                    }
                    if (formatoDecimal == "D")
                    {
                        rng.Value = decimal.Parse(valor.ToString());
                        rng.Style.Numberformat.Format = formato;
                    }
                    else
                    {
                        rng.Value = Double.Parse(valor.ToString());
                        // rng.Style.Numberformat.Format = formato;
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        protected void celdaFormatoEtiqueta(ExcelWorksheet ws1, object valor, int fila1, int columna1, int fila2 = 0, int columna2 = 0, string alineacion = "C", bool negrita = false)
        {
            try
            {

                fila2 = (fila2 == 0 ? fila1 : fila2);
                columna2 = (columna2 == 0 ? columna1 : columna2);
                using (var rng = ws1.Cells[fila1, columna1, fila2, columna2])
                {
                    if (negrita)
                    {
                        rng.Style.Font.Bold = true;
                    }
                    rng.Style.WrapText = true;
                    rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    if (alineacion == "L")
                    {
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    }
                    if (alineacion == "D")
                    {
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    }
                    if (alineacion == "J")
                    {
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Justify;
                    }
                    if (alineacion == "C")
                    {
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    rng.Style.Font.Size = 10;
                    rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Bottom.Color.SetColor(Color.FromArgb(0, 0, 0));
                    rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Left.Color.SetColor(Color.FromArgb(0, 0, 0));
                    rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Right.Color.SetColor(Color.FromArgb(0, 0, 0));
                    rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Top.Color.SetColor(Color.FromArgb(0, 0, 0));
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 255, 255));
                    rng.Style.Font.Color.SetColor(Color.FromArgb(0, 0, 0));
                    if (fila2 - fila1 > 0 || columna2 - columna1 > 0)
                    {
                        rng.Merge = true;
                    }
                    rng.Value = valor;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }




        protected void celdaFormatoEtiqueta_Cabecera(ExcelWorksheet ws1, object valor, int fila1, int columna1, int fila2 = 0, int columna2 = 0, string alineacion = "C", bool negrita = false)
        {
            try
            {

                fila2 = (fila2 == 0 ? fila1 : fila2);
                columna2 = (columna2 == 0 ? columna1 : columna2);
                using (var rng = ws1.Cells[fila1, columna1, fila2, columna2])
                {
                    if (negrita)
                    {
                        rng.Style.Font.Bold = true;
                    }
                    rng.Style.WrapText = true;
                    rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    if (alineacion == "L")
                    {
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    }
                    if (alineacion == "D")
                    {
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    }
                    if (alineacion == "J")
                    {
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Justify;
                    }
                    if (alineacion == "C")
                    {
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }


                    // rng.Style.Font.Bold = true;
                    rng.Style.WrapText = true;

                    rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(141, 180, 226));
                    rng.Style.Font.Size = 11;

                    // rng.Style.Font.Color.SetColor(Color.FromArgb(255, 255, 255));

                    rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Top.Color.SetColor(Color.FromArgb(0, 0, 0));
                    rng.Style.Border.Left.Color.SetColor(Color.FromArgb(0, 0, 0));
                    rng.Style.Border.Right.Color.SetColor(Color.FromArgb(0, 0, 0));
                    rng.Style.Border.Bottom.Color.SetColor(Color.FromArgb(0, 0, 0));


                    if ((fila2 - fila1) > 0 || (columna2 - columna1) > 0)
                    {
                        rng.Merge = true;
                    }
                    rng.Value = valor;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }


        protected void celdaFormatoFondoVerdeColorBlanco(ExcelWorksheet ws1, object valor, int fila1, int columna1, int fila2 = 0, int columna2 = 0)
        {
            Color fondo = Color.FromArgb(84, 130, 53);
            Color texto = Color.FromArgb(255, 255, 255);

            celdaFormato(ws1, valor, fila1, columna1, fila2, columna2, fondo, texto);
        }

        protected void celdaFormatoFondoAzulColorBlanco(ExcelWorksheet ws1, object valor, int fila1, int columna1, int fila2 = 0, int columna2 = 0, string alineacion = "C")
        {
            Color fondo = Color.FromArgb(68, 114, 196);
            Color texto = Color.FromArgb(255, 255, 255);

            celdaFormato(ws1, valor, fila1, columna1, fila2, columna2, fondo, texto, alineacion);
        }

        protected void celdaFormatoFondoGrisColorNegro(ExcelWorksheet ws1, object valor, int fila1, int columna1, int fila2 = 0, int columna2 = 0)
        {
            Color fondo = Color.FromArgb(201, 201, 201);
            Color texto = Color.FromArgb(0, 0, 0);

            celdaFormato(ws1, valor, fila1, columna1, fila2, columna2, fondo, texto);
        }

        protected void celdaFormatoFondoAzulColorNegro(ExcelWorksheet ws1, object valor, int fila1, int columna1, int fila2 = 0, int columna2 = 0)
        {
            Color fondo = Color.FromArgb(47, 117, 181);
            Color texto = Color.FromArgb(0, 0, 0);

            celdaFormato(ws1, valor, fila1, columna1, fila2, columna2, fondo, texto);
        }

        protected void celdaFormatoFondoGrisColorRojo(ExcelWorksheet ws1, object valor, int fila1, int columna1, int fila2 = 0, int columna2 = 0)
        {
            Color fondo = Color.FromArgb(201, 201, 201);
            Color texto = Color.FromArgb(255, 0, 0);

            celdaFormato(ws1, valor, fila1, columna1, fila2, columna2, fondo, texto);
        }

        protected void celdaFormatoFondoCelesteColorNegro(ExcelWorksheet ws1, object valor, int fila1, int columna1, int fila2 = 0, int columna2 = 0, string alineacion = "C")
        {
            Color fondo = Color.FromArgb(163, 226, 232);
            Color texto = Color.FromArgb(0, 0, 0);

            celdaFormato(ws1, valor, fila1, columna1, fila2, columna2, fondo, texto, alineacion);
        }

        protected void celdaFormato(ExcelWorksheet ws1, object valor, int fila1, int columna1, int fila2, int columna2, Color colorFondo, Color colorTexto, string alineacion = "C")
        {
            try
            {
                fila2 = (fila2 == 0 ? fila1 : fila2);
                columna2 = (columna2 == 0 ? columna1 : columna2);
                using (var rng = ws1.Cells[fila1, columna1, fila2, columna2])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.WrapText = true;
                    rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    if (alineacion == "L")
                    {
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    }
                    else
                    {
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }


                    rng.Style.Font.Size = 11;
                    rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Bottom.Color.SetColor(Color.FromArgb(0, 0, 0));
                    rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Left.Color.SetColor(Color.FromArgb(0, 0, 0));
                    rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Right.Color.SetColor(Color.FromArgb(0, 0, 0));
                    rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Top.Color.SetColor(Color.FromArgb(0, 0, 0));

                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    rng.Style.Fill.BackgroundColor.SetColor(colorFondo);
                    rng.Style.Font.Color.SetColor(colorTexto);
                    if (fila2 - fila1 > 0 || columna2 - columna1 > 0)
                    {
                        rng.Merge = true;
                    }
                    rng.Value = valor;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        */
        #endregion "Configuracion Celda"


        public string GetBaseUrl()
        {
            var request = HttpContext.Request;

            var host = request.Host.ToUriComponent();

            var pathBase = request.PathBase.ToUriComponent();

            return $"{request.Scheme}://{host}{pathBase}";
        }

        protected string GuardarArchivo(string rutaBase, string archivo, byte[] buffer, bool isRandom = true)
        {

            archivo = this.RutaArchivo(archivo, isRandom);
            string ruta = Path.Combine(rutaBase, archivo);
            if (System.IO.File.Exists(ruta) == true)
            {
                System.IO.File.Delete(ruta);
            }
            System.IO.File.WriteAllBytes(ruta, buffer);
            return ruta;
        }

        protected void EliminarArchivo(string archivo)
        {
            if (System.IO.File.Exists(archivo) == true)
            {
                System.IO.File.Delete(archivo);
            }
        }

        protected string RutaArchivo(string archivo, bool isRandom = true)
        {
            string nombre = Guid.NewGuid().ToString();
            string extension = Path.GetExtension(archivo);
            return nombre + extension;
        }

        protected DateTime GetFechaActual()
        {
            try
            {
                CultureInfo cultura = CultureInfo.CreateSpecificCulture("es-PE");
                string fecha = DateTime.Now.ToString(cultura);
                DateTime fechaHora = DateTime.Parse(fecha, cultura, DateTimeStyles.AssumeLocal);
                return fechaHora;
            }
            catch (Exception ex)
            {
                Log.CreateLogger(ex.Message);
                return DateTime.Now;
            }
        }

        protected DateTime GetFechaActual(DateTime fechaInput)
        {
            try
            {
                CultureInfo cultura = CultureInfo.CreateSpecificCulture("es-PE");
                string fecha = fechaInput.ToString(cultura);
                DateTime fechaHora = DateTime.Parse(fecha, cultura, DateTimeStyles.AssumeLocal);
                return fechaHora;
            }
            catch (Exception ex)
            {
                Log.CreateLogger(ex.Message);
                return DateTime.Now;
            }
        }

    }
}
