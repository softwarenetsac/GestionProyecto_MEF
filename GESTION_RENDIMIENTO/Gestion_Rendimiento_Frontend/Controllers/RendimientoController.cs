using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_Frontend.Controllers
{
    public class RendimientoController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

    }
}
