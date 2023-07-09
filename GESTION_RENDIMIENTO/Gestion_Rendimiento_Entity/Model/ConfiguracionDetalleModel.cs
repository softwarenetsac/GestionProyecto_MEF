using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_Entity.Model
{
  public   class ConfiguracionDetalleModel
    {
        public int ID_CONFIGURAR_DETALLE { get; set; }
        public string DESCRIPCION { get; set; }
        public int ID_CONFIGURACION { get; set; }
        public string CONFIGURACION_DESCRIPCION{ get; set; }

        public string FLG_ESTADO { get; set; }
    }
}
