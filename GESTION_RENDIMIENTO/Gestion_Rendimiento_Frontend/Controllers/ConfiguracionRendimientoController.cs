using Gestion_Rendimiento_Common;
using Gestion_Rendimiento_Entity;
using Gestion_Rendimiento_Entity.Model;
using Gestion_Rendimiento_IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_Frontend.Controllers
{
    public class ConfiguracionRendimientoController : BaseController
    {


        private readonly IConfiguracionRendimientoService _configuracionRendimientoService;

        public ConfiguracionRendimientoController(
        IConfiguracionRendimientoService configuracionRendimientoService,
         IOptions<ConfiguracionSistemaModel> configuracionSistema,
           IHttpContextAccessor contextAccessor
        )
        {
            // _logger = logger;
            _configuracionRendimientoService = configuracionRendimientoService;
            _configuracionSistemaBase = configuracionSistema.Value;
            _contextAccessor = contextAccessor;
        }

        public IActionResult Index()
        {

            return View();
        }




        //public IActionResult Procesar([FromBody] ConfiguracionDetalleModel modelo)
        //{
        //    var result = new MethodResponseModel<string> { };
        //    try
        //    {
        //        var flg_estado = (int)StatusEnums.Active;
        //        var id_estado = modelo.ID_CONFIGURACION;
        //        var usuario_login = UsuarioActual == null ? "" : UsuarioActual.UsuarioLogin;
        //        var entidad = new ConfiguracionDetalle
        //        {
        //            ID_CONFIGURACION = modelo.ID_CONFIGURACION,
        //            ID_CONFIGURAR_DETALLE = modelo.ID_CONFIGURAR_DETALLE,
        //            DESCRIPCION = modelo.DESCRIPCION == null ? "" : modelo.DESCRIPCION.Trim(),
        //            FLG_ESTADO = flg_estado.ToString(),
        //            USUARIO_CREACION = usuario_login,
        //            USUARIO_MODIFICACION = usuario_login,
        //            IP_CREACION = IP,
        //            IP_MODIFICACION = IP,
        //            FECHA_CREACION = GetFechaActual(),
        //            FECHA_MODIFICACION = GetFechaActual()
        //        };

        //        if (id_estado <= 0)
        //        {
        //            var respuesta = _configuracionRendimientoService.Insertar(entidad);

        //            id_estado = respuesta.ID_CONFIGURAR_DETALLE;
        //        }
        //        else
        //        {
        //            var respuesta = _configuracionRendimientoService.Actualizar(entidad);


        //        }
        //        if (id_estado > 0)
        //        {
        //            result.Success = true;
        //            result.Message = "Se ha registrado correctamente";
        //            result.Result = id_estado.ToString();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Log.CreateLogger(ex.Message);
        //        result.Message = ex.Message;
        //        result.Code = (int)HttpStatusCode.InternalServerError;
        //        result.Result = "";
        //    }

        //    return Ok(result);


        //}

  



        //public IActionResult GetAll()
        //{
        //    var result = new MethodResponseModel<IEnumerable<ConfiguracionDetalle>> { Result = null };
        //    try
        //    {
        //        var items = _configuracionRendimientoService.GetGrupos().OrderBy(p => p.DESCRIPCION).ToList();
        //        result.Result = items;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Message = ex.Message;
        //        result.Code = (int)HttpStatusCode.InternalServerError;
        //        result.Result = null;
        //    }
        //    return Ok(result);

        //}

   
    
    }
}
