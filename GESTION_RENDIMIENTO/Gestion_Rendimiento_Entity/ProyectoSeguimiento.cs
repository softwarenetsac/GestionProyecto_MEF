using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Gestion_Rendimiento_Entity
{
    [Table("T_D_SEGUIMIENTO")]
    public class ProyectoSeguimiento: BaseCampo
    {
        [Key]
        public int ID_SEGUIMIENTO { get; set; }
        public int ID_PROYECTO { get; set; }
        public string DETALLE_NOTA { get; set; }
        public int ID_TIPO_NIVEL { get; set; }
        public long ID_ARCHIVO { get; set; }
        public string ID_EVALUADOR { get; set; }
    }
}
