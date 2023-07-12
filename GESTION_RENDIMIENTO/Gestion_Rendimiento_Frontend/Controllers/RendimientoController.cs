using Gestion_Rendimiento_Entity.Model;
using Gestion_Rendimiento_IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_Frontend.Controllers
{
    public class RendimientoController : BaseController
    {


        private readonly IConfiguracionRendimientoService _configuracionRendimientoService;

        public RendimientoController(
       IConfiguracionRendimientoService configuracionRendimientoService,
          IOptions<ConfiguracionSistemaModel> configuracionSistema,
           IHttpContextAccessor contextAccessor
            )
        {
            _configuracionRendimientoService = configuracionRendimientoService;
            _configuracionSistemaBase = configuracionSistema.Value;
            _contextAccessor = contextAccessor;
        }




        #region PROGRAMACION

        public IActionResult Programacion()
        {

            //var modelo = new ConfiguracionDetalleModel
            //{
            // FLG_ESTADO= "1",
            //  ID_CONFIGURACION=1
            //};

            //var data = _configuracionRendimientoService.GetAllDetalle(modelo).FirstOrDefault();

            //ViewBag.PRIORIDAD_ANUAL = data.DESCRIPCION;



            return View();
        }
        #endregion




        #region SEGUIMIENTO

        public IActionResult Seguimiento()
        {

            //var modelo = new ConfiguracionDetalleModel
            //{
            // FLG_ESTADO= "1",
            //  ID_CONFIGURACION=1
            //};

            //var data = _configuracionRendimientoService.GetAllDetalle(modelo).FirstOrDefault();

            //ViewBag.PRIORIDAD_ANUAL = data.DESCRIPCION;



            return View();
        }
        #endregion



        #region     EVALUACION

        public IActionResult Evaluacion()
        {

            //var modelo = new ConfiguracionDetalleModel
            //{
            // FLG_ESTADO= "1",
            //  ID_CONFIGURACION=1
            //};

            //var data = _configuracionRendimientoService.GetAllDetalle(modelo).FirstOrDefault();

            //ViewBag.PRIORIDAD_ANUAL = data.DESCRIPCION;



            return View();
        }
        #endregion








        #region RENDIMIENTO ORH

        public IActionResult RendimientoORH()
        {

            //var modelo = new ConfiguracionDetalleModel
            //{
            // FLG_ESTADO= "1",
            //  ID_CONFIGURACION=1
            //};

            //var data = _configuracionRendimientoService.GetAllDetalle(modelo).FirstOrDefault();

            //ViewBag.PRIORIDAD_ANUAL = data.DESCRIPCION;



            return View();
        }
        #endregion



    }
}
