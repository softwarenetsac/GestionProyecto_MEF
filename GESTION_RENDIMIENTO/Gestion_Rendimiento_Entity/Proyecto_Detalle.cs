using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Gestion_Rendimiento_Entity
{
    [Table("T_D_PROYECTO")]
    public class Proyecto_Detalle: BaseCampo
    {
        [Key]
        public int ID_DETALLE_PROYECTO { get; set; }
        public int ID_PROYECTO { get; set; }
        public string INDICADOR_PRODUCTO { get; set; }
        public int VALOR { get; set; }
        public int PESO { get; set; }
        public string EVIDENCIA { get; set; }
        public string PLAZO { get; set; }
        
    }
}
