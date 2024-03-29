﻿using Gestion_Rendimiento_Entity;
using Gestion_Rendimiento_Entity.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public IEnumerable<EvaluadorConsultaModel> Evaluadores { get; set; }
        public List<SelectListItem> List_Anio { get; set; }
    }
}