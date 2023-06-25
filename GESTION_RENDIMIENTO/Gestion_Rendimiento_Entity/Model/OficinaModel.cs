using System;
using System.Collections.Generic;
using System.Text;

namespace Gestion_Rendimiento_Entity.Model
{
   public  class OficinaModel
    {
        public int ID_OFICINA { get; set; }
        public int ID_AREA { get; set; }
        public string NOMBRE_OFICINA { get; set; }
        public int NOMBRE_AREA { get; set; }
        public string TIPO { get; set; }
        public string SIGLA { get; set; }
      //  public List<AsignarCoordinadorConsulta> Oficinas { get; set; }
    }
}
