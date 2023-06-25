
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_Entity.Models
{
    public class ParametroGeneralViewModel
    {
        public IEnumerable<Oficina> Oficinas { get; set; }

        public string FECHA_INICIO_CADENA { get; set; }
        public string FECHA_FINAL_CADENA { get; set; }
        public string ID_PERSONAL { get; set; }
        public int ID_OFICINA { get; set; }
        public int ID_AREA { get; set; }
        public int ID_ESTADO { get; set; }
        public string NOMBRE_AREA { get; set; }
        public string NOMBRE_OFICINA { get; set; }
       
    }
}
