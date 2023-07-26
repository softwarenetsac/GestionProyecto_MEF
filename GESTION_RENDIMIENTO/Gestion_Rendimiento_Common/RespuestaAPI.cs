using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_Common
{
    public class RespuestaAPI
    {
        public long Id_Laser_Fiche { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public byte[] archivo_lf { get; set; }
        
    }
}
