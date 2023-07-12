using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_Entity.Model
{
   public  class ConfiguracionRendimientoModel
    {
        public int ID_CONFIGURACION { get; set; }
        public string DESCRIPCION { get; set; }
        public string ABREVIATURA_CON { get; set; }
        public string ABREVIATURA_TIPO { get; set; }
        public string TIPO_DESCRIPCION { get; set; }
        public int ID_TIPO_GESTION { get; set; }

        public string USUARIO_CREACION { get; set; }
        public string USUARIO_MODIFICACION { get; set; }
        public DateTime? FECHA_CREACION { get; set; }
        public DateTime? FECHA_MODIFICACION { get; set; }
        public string IP_CREACION { get; set; }
        public string IP_MODIFICACION { get; set; }
        public string FLG_ESTADO { get; set; }
        public string ANIO { get; set; }

    }
}
