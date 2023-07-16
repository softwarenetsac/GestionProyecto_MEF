using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_Entity.Model
{
    public class ProyectoModel
    {
        public int ID_PROYECTO { get; set; }
        public string DESCRIPCION { get; set; }
        public string ID_PERSONAL { get; set; }
        public int ID_OFICINA { get; set; }
        public int ID_ESTADO { get; set; }
        public string ID_EVALUADOR { get; set; }
        public string ANIO { get; set; }
    }
}
