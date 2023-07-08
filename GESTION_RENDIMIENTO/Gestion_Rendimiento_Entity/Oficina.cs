using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_Entity
{
    [Table("V_OFICINA_GR")]
    public class Oficina
    {
        [Key]
        public int ID_OFICINA { get; set; }
        public string NOMBRE_OFICINA { get; set; }
        public int ID_AREA { get; set; }
        public string NOMBRE_AREA { get; set; }
        public string TIPO { get; set; }
        public string TIPO_NOMBRE { get; set; }
        public string SIGLA { get; set; }
        public string CODIGO_PADRE { get; set; }
        public string CODIGO_OFICINA { get; set; }

    }
}
