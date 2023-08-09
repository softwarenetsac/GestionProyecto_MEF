using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_Entity.Model
{
    public class ReporteRendimientoModel
    {
        public int ID_PROYECTO { get; set; }
        public string NOMBRE_ENTIDAD { get; set; }
        public string DESCRIPCION { get; set; }
        public string ID_PERSONAL { get; set; }
        public int ID_OFICINA { get; set; }
        public string ID_ESTADO { get; set; }
        public string ID_EVALUADOR { get; set; }
        public string FLG_ESTADO { get; set; }
        public string ANIO { get; set; }
        public string FECHA_REGISTRO { get; set; }
        public string DNI_EVALUADO { get; set; }
        public string NOMBRE_EVALUADO { get; set; }
        public string NOMBRE_CARGO_EVALUADO { get; set; }
        public string NOMBRE_SEGMENTO_EVALUADO { get; set; }
        public string NOMBRE_ORGANO_EVALUADO { get; set; }
        public string NOMBRE_EVALUADOR { get; set; }
        public string NOMBRE_CARGO_EVALUADOR { get; set; }
        public string NOMBRE_SEGMENTO_EVALUADOR { get; set; }
        public string NOMBRE_ORGANO_EVALUADOR { get; set; }
        public string NOMBRE_ESTADO { get; set; }
        public int ID_DETALLE_PROYECTO { get; set; }
        public string INDICADOR_PRODUCTO { get; set; }
        public int VALOR { get; set; }
        public int PESO { get; set; }
        public string EVIDENCIA { get; set; }
        public string PLAZO { get; set; }
        public string TIPO_FORMULA { get; set; }
    }
}
