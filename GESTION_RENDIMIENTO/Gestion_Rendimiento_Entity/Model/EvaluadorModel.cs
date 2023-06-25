using System;
using System.Collections.Generic;
using System.Text;

namespace Gestion_Rendimiento_Entity.Model
{
  public   class EvaluadorModel
    {


        public int ID_EVALUADOR { get; set; }
        public string ID_PERSONA_JEFE { get; set; }
        public string ID_PERSONA_ALTERNO { get; set; }
        public string FLG_AUTORIZADOR { get; set; }
        public string FLG_ESTADO { get; set; }
        public DateTime? FECHA_INICIO_JEFE { get; set; }
        public DateTime? FECHA_FINAL_JEFE { get; set; }
        public DateTime? FECHA_INICIO_ALTERNO { get; set; }
        public DateTime? FECHA_FINAL_ALTERNO { get; set; }

        public int ID_GRUPO { get; set; }
        public string FLG_TIPO { get; set; }
        public string FLG_INDEFINADO_ALTERNO { get; set; }

        public string FLG_INDEFINADO_JEFE { get; set; }

        public string USUARIO_CREACION { get; set; }
        public string USUARIO_MODIFICACION { get; set; }
        public string IP_CREACION { get; set; }
        public string IP_MODIFICACION { get; set; }
        public string ID_OFICINA_JEFE { get; set; }

        public string ID_AREA_JEFE { get; set; }
        public string FECHA_INICIO_JEFE_CADENA { get; set; }
        public string FECHA_FINAL_JEFE_CADENA { get; set; }
        public string FECHA_INICIO_ALTERNO_CADENA { get; set; }
        public string FECHA_FINAL_ALTERNO_CADENA { get; set; }


        public DateTime? FECHA_DOCUMENTO_JEFE { get; set; }
        public DateTime? FECHA_DOCUMENTO_ALTERNO { get; set; }

        public string ID_ESTADO { get; set; }
        public string FECHA_DOCUMENTO_JEFE_CADENA { get; set; }
        public string FECHA_DOCUMENTO_ALTERNO_CADENA { get; set; }
        public string NUMERO_DOCUMENTO_JEFE { get; set; }
        public string NUMERO_DOCUMENTO_ALTERNO { get; set; }
        public string OBSERVACION_JEFE { get; set; }
        public string OBSERVACION_ALTERNO { get; set; }
        public string CORREO_INSTITUCIONAL { get; set; }
        public string NOMBRE_COMPLETO { get; set; }
    }
}
