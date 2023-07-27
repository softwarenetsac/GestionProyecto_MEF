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

    public class ArchivoController : ApiController
    {
        // POST api/values
        public RespuestaAPI UploadFile(Archivo entidad)
        {
            int id_laser = 1;
            var respuesta = new RespuestaAPI();
            try
            {
                var key = WebConfigurationManager.AppSettings["KeyWS"].ToString();
                if (entidad.ARCHIVO == null)
                {
                    throw new Exception("el Archivo a adjuntar no puede ser null");
                }
                if (string.IsNullOrEmpty(entidad.KEY))
                {
                    throw new Exception("la credencia no puede ser null");
                }
                if (entidad.KEY != key)
                {
                    throw new Exception("la credencia es incorrecta");
                }
                string carpeta = HttpContext.Current.Server.MapPath("~/Archivos_Temporales");
                string rutaPdf = Path.Combine(carpeta, entidad.NOMBRE_ARCHIVO);
                using (FileStream fs = new FileStream(rutaPdf, FileMode.Create))
                {
                    fs.Write(entidad.ARCHIVO, 0, entidad.ARCHIVO.Length);
                    fs.Close();
                }
                id_laser = UtilLaserfiche.SubirArchivoSubSubCarpeta(rutaPdf,entidad.RUTA_PRINCIPAL.ToString(), entidad.RUTA_GUARDAR, entidad.NOMBRE_ARCHIVO);
                if (id_laser > 0)
                {
                    respuesta.Success = true;
                    respuesta.Id_Laser_Fiche = id_laser;
                    respuesta.Message = "Archivo se registro correctamente";
                }
                else
                {
                    respuesta.Success = false;
                    respuesta.Id_Laser_Fiche = 0;
                    respuesta.Message = "Se genero problemas al subir el archivo";
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