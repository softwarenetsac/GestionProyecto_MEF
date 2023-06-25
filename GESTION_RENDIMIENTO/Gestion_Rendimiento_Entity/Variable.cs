using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gestion_Rendimiento_Entity
{
    [Table("T_L_VARIABLE")]
    public class Variable
    {
        [Key, Column(Order = 0)]
        public string SISTEMA { get; set; }
        [Key, Column(Order = 1)]
        public string CAMPO { get; set; }
        [Key, Column(Order = 2)]
        public string VALOR { get; set; }
        public string DESCRIPCION { get; set; }
        public string FLG_ESTADO { get; set; }
        public string ABREVIATURA { get; set; }
        public string FLG_VALIDACION { get; set; }

    }
}
