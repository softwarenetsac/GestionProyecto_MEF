using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Gestion_Rendimiento_Common
{
  public   class Log
    {
        //public static void Mensaje(string ex)
        //{
        //    try
        //    {

        //        Microsoft.AspNetCore.Http.PathString
        //        String file = "";
        //        if (!Directory.Exists(HttpContext.Current.Request.PhysicalApplicationPath + "\\Log\\"))
        //        {
        //            Directory.CreateDirectory(HttpContext.Current.Request.PhysicalApplicationPath + "\\Log");
        //        }
        //        file = HttpContext.Current.Request.PhysicalApplicationPath + "\\Log\\" + "LOG_" + DateTime.Today.Date.ToString("yyyyMMdd") + ".log";

        //        StreamWriter sw = new StreamWriter(file, true);

        //        sw.WriteLine(DateTime.Now + ": [mensaje: " + ex);

        //        sw.Close();
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        public static void CreateLogger(string ex)
        {
            try
            {

                //string rutaBase = AppDomain.CurrentDomain.BaseDirectory;
                string rutaBase = System.IO.Directory.GetCurrentDirectory();

                String file = "";
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
