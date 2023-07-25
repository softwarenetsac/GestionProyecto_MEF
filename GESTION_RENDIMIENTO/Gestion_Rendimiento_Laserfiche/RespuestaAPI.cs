using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gestion_Rendimiento_Laserfiche
{
    public class RespuestaAPI
    {

        public int  Id_Laser_Fiche { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}