using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_Entity
{
    [Table("V_EVALUADOR_CONSULTA_GR")]
    public   class EvaluadorConsulta
    {
        [Key]
        public int ID_EVALUADOR { get; set; }
        public int ID_AREA { get; set; }
        public int ID_OFICINA { get; set; }
        public string NOMBRE_AREA { get; set; }
        public string NOMBRE_OFICINA { get; set; }
        public string ID_PERSONA { get; set; }
        public string APELLIDO_PATERNO { get; set; }
        public string APELLIDO_MATERNO { get; set; }
        public string NOMBRE { get; set; }
        public string NOMBRE_COMPLETO { get; set; }
        public string NUMERO_DNI { get; set; }
        public string NOMBRE_CARGO { get; set; }
        public string NOMBRE_CATEGORIA { get; set; }
        public string CORREO_INSTITUCIONAL { get; set; }
        public string USUARIO_LOGIN { get; set; }
        public string ID_SEXO { get; set; }
        public int ID_SITUACION_LABORAL { get; set; }
        public string FECHA_INGRESO { get; set; }
        public string FECHA_NACIMIENTO { get; set; }

        public int TOTAL_EVALUADO { get; set; }
    }
}
