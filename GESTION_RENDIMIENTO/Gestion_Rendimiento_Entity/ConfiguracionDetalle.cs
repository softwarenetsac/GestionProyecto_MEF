using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_Entity
{
    [Table("T_D_CONFIGURAR_DETALLE")]
    public  class ConfiguracionDetalle: BaseCampo
    {
        [Key]
        public int ID_CONFIGURAR_DETALLE { get; set; }
        public string DESCRIPCION { get; set; }
        public int ID_CONFIGURACION { get; set; }
  
    }
}
