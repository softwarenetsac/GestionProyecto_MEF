
using Gestion_Rendimiento_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Gestion_Rendimiento_Frontend.Models
{
    public class EvaluadoViewModel
    {
        public IEnumerable<Oficina> Oficinas { get; set; }
        public IEnumerable<Persona> Personas { get; set; }
    }
}
