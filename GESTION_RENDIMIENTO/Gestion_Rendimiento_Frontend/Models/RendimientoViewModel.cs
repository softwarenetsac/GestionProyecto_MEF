using Gestion_Rendimiento_Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Gestion_Rendimiento_Frontend.Models
{
    public class RendimientoViewModel
    {
        public IEnumerable<Oficina> Oficinas { get; set; }
        public IEnumerable<Persona> Personas { get; set; }
        public List<SelectListItem> List_Anio { get; set; }
        public int ID_AREA { get; set; }
        public int ID_OFICINA { get; set; }
    }
}
