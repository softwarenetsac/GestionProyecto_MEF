using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_Entity
{
    [Table("T_D_PROYECTO_SUB")]
    public class Proyecto_Detalle_Sub: BaseCampo
    {
        [Key]
        public int ID_DETALLE_SUB { get; set; }
        public int ID_DETALLE_PROYECTO { get; set; }
        public string EVIDENCIA { get; set; }
    }
}
