using Laserfiche.RepositoryAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using Gestion_Rendimiento_Laserfiche.Repositorio;
namespace Gestion_Rendimiento_Laserfiche.Controllers
{
    public class ExportarArchivoController : ApiController
    {
        // GET: ExportarArchivo
        public RespuestaAPI DescargarArchivoLF (Archivo entidad)
        {
            int id_laser = 1;
            var respuesta = new RespuestaAPI();
            try
            {
                var key = WebConfigurationManager.AppSettings["KeyWS"].ToString();
                if (string.IsNullOrEmpty(entidad.KEY))
                {
                    throw new Exception("la credencia no puede ser null");
                }
                if (entidad.KEY != key)
                {
                    throw new Exception("la credencia es incorrecta");
                }
                respuesta.archivo_lf = UtilLaserfiche.ExportarDocumentoPDF(entidad.ID_LASERFICHE,"");
                if (respuesta.archivo_lf.Length > 0)
                {
                    respuesta.Success = true;
                    respuesta.Message = "Archivo se descargo correctamente";
                }
                else
                {
                    respuesta.Success = true;
                    respuesta.Message = "Error al descargar archivo";
                }
            }
            catch (Exception ex)
            {
                AplicacionLog.Mensaje(ex.Message);
                respuesta.Message = ex.Message;
                respuesta.Id_Laser_Fiche = 0;
                respuesta.Success = false;
            }
            return respuesta;
            //return File(respuesta.archivo_lf, "application/pdf", Guid.NewGuid()+".pdf");
        }
        public class Archivo
        {
            public byte[] ARCHIVO { get; set; }
            public string NOMBRE_ARCHIVO { get; set; }
            public string RUTA_PRINCIPAL { get; set; }
            public string KEY { get; set; }
            public string RUTA_GUARDAR { get; set; }
            public int ID_LASERFICHE { get; set; }
        }
    }
}