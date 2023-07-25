using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Gestion_Rendimiento_Laserfiche
{
    public  static class AplicacionLog
    {


        public static void Mensaje(string ex)
        {
            try
            {

             
                string rutaBase = Directory.GetCurrentDirectory();

                string file = "";
                if (!Directory.Exists(rutaBase + "\\Log\\"))
                {
                    Directory.CreateDirectory(rutaBase + "\\Log");
                }
                file = rutaBase + "\\Log\\" + "LOG_" + DateTime.Today.Date.ToString("yyyyMMdd") + ".log";

                StreamWriter sw = new StreamWriter(file, true);

                sw.WriteLine(DateTime.Now + ": [mensaje: " + ex);

                sw.Close();
            }
            catch (Exception)
            {
            }
        }

    }
}