using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_Entity
{

    
    [Table("T_L_GESTION_PROYECTO")]
    public class TipoGestionRendimiento: BaseCampo
    {

        [Key]
        public int ID_TIPO_GESTION { get; set; }
        public string DESCRIPCION { get; set; }
        public string ABREVIATURA { get; set; }
    }
}
