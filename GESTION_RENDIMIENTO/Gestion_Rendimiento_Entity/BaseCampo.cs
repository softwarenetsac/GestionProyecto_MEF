using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_Entity
{
   public  class BaseCampo
    {
        public string USUARIO_CREACION { get; set; }
        public string USUARIO_MODIFICACION { get; set; }
        public DateTime? FECHA_CREACION { get; set; }
        public DateTime? FECHA_MODIFICACION { get; set; }
        public string IP_CREACION { get; set; }
        public string IP_MODIFICACION { get; set; }
        public string FLG_ESTADO { get; set; }
    }
}
