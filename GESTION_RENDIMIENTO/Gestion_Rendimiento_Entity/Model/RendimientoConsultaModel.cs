using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_Entity.Model
{
  public   class RendimientoConsultaModel
    {
        public int ID_PROYECTO { get; set; }
        public string DESCRIPCION { get; set; }
        public string ID_PERSONAL { get; set; }
        public int ID_OFICINA { get; set; }
        public int ID_AREA { get; set; }
        public string ID_ESTADO { get; set; }
        public string ID_EVALUADOR { get; set; }
        public string FLG_ESTADO { get; set; }
        public string ANIO { get; set; }
        public string PLAZO { get; set; }
        public string NOMBRE_EVALUADO { get; set; }
        public string NOMBRE_CARGO { get; set; }
        public string NOMBRE_EVALUADOR { get; set; }
        public string NOMBRE_ESTADO { get; set; }
        public string TIPO { get; set; }
        public string NOMBRE_OFICINA { get; set; }
        
    }
}
