
using Gestion_Rendimiento_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_Frontend.Models
{
    public class EvaluadorViewModel
    {
        public IEnumerable<Oficina> Oficinas { get; set; }
        public IEnumerable<Persona> Personas { get; set; }
      //  public IEnumerable<GrupoAsistenciaModel> Grupos { get; set; }
      //  public AutorizadorTareoConsultaModel Autorizador { get; set; }
    }
}
