using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_Entity.Model
{
    public class RendimientoDetalleSubModel
    {
        public int ID_DETALLE { get; set; }
        public int ID_DETALLE_SUB { get; set; }
        
        public string EVIDENCIA  { get; set; }
        public string PLAZOS { get; set; }
        public string INDICADOR { get; set; }
        public int VALOR { get; set; }
    }
}
