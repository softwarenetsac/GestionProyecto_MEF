using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace Gestion_Rendimiento_Laserfiche.Controllers
{
    public class ArchivoController : ApiController
    {







       // POST api/values

        public RespuestaAPI Post(string cadenaByteArchivo, string nombreArchivo, string keyWS)
        {
            var respuesta = new RespuestaAPI();
            try
            {
                var key = WebConfigurationManager.AppSettings["KeyWS"].ToString();
                if (string.IsNullOrEmpty(cadenaByteArchivo))
                {
                    throw new Exception("el Archivo a adjuntar no puede ser null");
                }
                if (string.IsNullOrEmpty(keyWS))
                {
                    throw new Exception("la credencia no puede ser null");
                }
                if (keyWS != key)
                {
                    throw new Exception("la credencia es incorrecta");
                }
                byte[] bytes = Encoding.ASCII.GetBytes(cadenaByteArchivo);

                // implemetar registro archivo 

                int id_laser = 1;
                respuesta.Id_Laser_Fiche = id_laser;
                respuesta.Message = "";
                if (id_laser > 0)
                {
                    respuesta.Success = false;
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


    }
}