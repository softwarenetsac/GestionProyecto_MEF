using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_Entity
{
    [Table("T_L_NIVEL_SEGUIMIENTO")]
    public class NivelSeguimiento: BaseCampo
    {

        [Key]
        public int ID_TIPO_NIVEL { get; set; }
        public string DESCRIPCION { get; set; }
    }
}
