using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_Entity
{
    [Table("V_SEGUIMIENTO_PROYECTO")]
    public class ProyectoSeguimientoConsulta
    {
        [Key]
        public int ID_SEGUIMIENTO { get; set; }
        public int ID_PROYECTO { get; set; }
        public string DES_PROYECTO { get; set; }
        public string DETALLE_NOTA { get; set; }
        public string ID_EVALUADOR { get; set; }
        public string EVALUADOR { get; set; }
        public long ID_ARCHIVO { get; set; }
        public int ID_TIPO_NIVEL { get; set; }
        public string DES_NIVEL { get; set; }
        public string FLG_ESTADO { get; set; }

    }
}
