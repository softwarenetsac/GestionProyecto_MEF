﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_Frontend.Controllers
{
    public class EvaluadoController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
