
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gestion_Rendimiento_Entity
{
    [Table("T_M_EVALUADOR")]
    public class Evaluador : BaseCampo
    {
        [Key]
        public int ID_EVALUADOR { get; set; }


        public string ID_PERSONA_JEFE { get; set; }
        public string ID_PERSONA_ALTERNO { get; set; }
        public string FLG_AUTORIZADOR { get; set; }

        public DateTime? FECHA_INICIO_JEFE { get; set; }
        public DateTime? FECHA_FINAL_JEFE { get; set; }
        public DateTime? FECHA_INICIO_ALTERNO { get; set; }
        public DateTime? FECHA_FINAL_ALTERNO { get; set; }
        public string FLG_INDEFINADO_JEFE { get; set; }
        public string FLG_INDEFINADO_ALTERNO { get; set; }


        public string ID_OFICINA_JEFE { get; set; }


        public string ID_AREA_JEFE { get; set; }

        public int? ID_GRUPO { get; set; }
        public string FLG_TIPO { get; set; }

        public DateTime? FECHA_DOCUMENTO_JEFE { get; set; }
        public DateTime? FECHA_DOCUMENTO_ALTERNO { get; set; }
        public string NUMERO_DOCUMENTO_JEFE { get; set; }
        public string NUMERO_DOCUMENTO_ALTERNO { get; set; }
        public string OBSERVACION_JEFE { get; set; }
        public string OBSERVACION_ALTERNO { get; set; }


    }
}
