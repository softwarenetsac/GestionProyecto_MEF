using Gestion_Rendimiento_Common;
using Gestion_Rendimiento_Entity;
using Gestion_Rendimiento_Entity.Model;
using Gestion_Rendimiento_Frontend.Extensiones;
using Gestion_Rendimiento_Frontend.Models;
using Gestion_Rendimiento_IService;
using Microsoft.AspNetCore.Authentication; 
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_Frontend.Controllers
{
    public class LoginController : Controller
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger _logger;
        private readonly ConfiguracionSistemaModel _configuracionSistema;
        private readonly IUsuarioService _usuarioService;
        private IHeaderDictionary Headers { get; set; }
        private readonly IPersonaService _personaService;


        public LoginController(
           IHttpContextAccessor httpContextAccessor,
          IOptions<ConfiguracionSistemaModel> configuracionSistema,
          ILogger<LoginController> logger,
           IUsuarioService usuarioService,
              IPersonaService personaService
        )
        {
            _logger = logger;
            _configuracionSistema = configuracionSistema.Value;
            _httpContextAccessor = httpContextAccessor;
            _usuarioService = usuarioService;
            _personaService = personaService;
        }


        public IActionResult Index()
        {


            var autenficacion = _configuracionSistema.Seguridad.EjecucionLogin;
            if (autenficacion == "1")
            {
                Conexion_Ldap();
            }
            else
            {

                Conexion_Local();
            }
            return View();
        }



        private void Conexion_Ldap()
        {

            var user = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Login")?.Value;
            if (!string.IsNullOrEmpty(user))
            {
                Log.CreateLogger("USUARIO ACTIVO RECONEXION CON  " + user);
                var baseUrl = GetBaseUrl();
                var url = baseUrl + "/Panel/Index";
                HttpContext.Response.Redirect(url);
            }
        }


        #region LOCAL

        private void Conexion_Local()
        {

            var entitySession = ObtenerDatos_Local();
            entitySession.Autenticacion = _configuracionSistema.Seguridad.EjecucionLogin;
            entitySession.Token = entitySession.UsuarioLogin;
            Log.CreateLogger("CONEXION LOCAL CON EL USUARIO " + entitySession.UsuarioLogin);
            AsignarUsuario(entitySession);
            var baseUrl = GetBaseUrl();
            var url = baseUrl + "/Panel/Index";
            HttpContext.Response.Redirect(url);
        }

        private UsuarioSesion ObtenerDatos_Local()
        {
            string usuario = _configuracionSistema.UsuarioPrueba.UsuarioLogin;
            string id_personal = _configuracionSistema.UsuarioPrueba.Id_Persona;
            UsuarioSesion entitySession = new UsuarioSesion();
            entitySession.UsuarioLogin = usuario;
            entitySession.Nombre = _configuracionSistema.UsuarioPrueba.Nombre;
            entitySession.ID_PERSONAL = id_personal;
            string nombres = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(entitySession.Nombre);
            entitySession.Nombre = nombres;
            return entitySession;
        }


        #endregion



        public IActionResult Iniciar(LoginModel modelo)
        {
            BaseResponse respuesta = new BaseResponse { Success = false };
            try
            {

                var clave = modelo.Clave;
                var usuario = modelo.Usuario;
                usuario = usuario == null ? "" : usuario.Trim();
                clave = clave == null ? "" : clave.Trim();
                if (string.IsNullOrEmpty(usuario))
                {
                    throw new Exception("Debe ingresar el usuario");
                }
                if (string.IsNullOrEmpty(clave))
                {
                    throw new Exception("Debe ingresar la contraseña");
                }
                respuesta = AuthenticateUser(usuario, clave);
                //respuesta.Success = true;
                if (respuesta.Success)
                {
                    respuesta.Success = true;
                    var id_sistema = _configuracionSistema.Seguridad.CodigoSistema;
                    var entity_user = _usuarioService.Usuario_Detalle(usuario, id_sistema);
                    if (entity_user != null)
                    {
                        CargarData(entity_user);
                    }
                    else
                    {
                        var persona_bitacora = _personaService.DetallePorUsuario(usuario, "1");
                        if (persona_bitacora != null)
                        {
                            var entity_user_bitacora = new UsuarioSesion
                            {
                                ID_USUARIO = 0,
                                UsuarioLogin = persona_bitacora.USUARIO_LOGIN,
                                ID_PERSONAL = persona_bitacora.ID_PERSONAL,
                                ID_OFICINA = persona_bitacora.ID_OFICINA.ToString(),
                                Nombre = persona_bitacora.NOMBRE,
                                NOMBRE_OFICINA = persona_bitacora.NOMBRE_OFICINA,
                                SIGLA = ""
                            };
                            CargarData_Bitacora(entity_user_bitacora);
                        }
                        else
                        {
                            throw new Exception("El usuario no tiene acceso al sistema");
                        }


                    }
                }
                else
                {
                    throw new Exception("Usuario / Contraseña de la red es incorrecta");
                }
            }
            catch (Exception ex)
            {

                respuesta.Message = ex.Message;
                respuesta.Success = false;
                Log.CreateLogger(ex.Message);
            }
            return Ok(respuesta);
        }

        private BaseResponse AuthenticateUser(string user, string password)
        {
            string ldap = _configuracionSistema.Seguridad.LDAP;
            if (string.IsNullOrEmpty(ldap))
            {
                Log.CreateLogger("LINK DE LDAP ESTA VACIO");
            }

            BaseResponse respuesta = new BaseResponse { Success = false };
            try
            {
                DirectoryEntry de = new DirectoryEntry("LDAP://" + ldap, user, password, AuthenticationTypes.Secure);
                DirectorySearcher ds = new DirectorySearcher(de);
                SearchResult result = ds.FindOne();
                respuesta.Success = true;
                return respuesta;
            }
            catch (Exception ex)
            {
                string mensaje = "login==> " + ex.Message;
                mensaje += " si el error de codigo es : 525,52e,530,531,532,533,701,773,775";
                mensaje += " la explicacion del error en el  siguiente link";
                mensaje += " https://support.infrasightlabs.com/troubleshooting/common-error-codes-for-active-directory-authentication/";
                Log.CreateLogger(mensaje);
                respuesta.Success = false;
                respuesta.Message = "Usuario / Contraseña  es incorrecta";
            }
            return respuesta;
        }


        private void CargarData(UsuarioSesion usuario)
        {
            var id_sistema = _configuracionSistema.Seguridad.CodigoSistema;
            var baseUrl = GetBaseUrl() + "/";
            var modulo = _usuarioService.Modulo(usuario.ID_USUARIO, id_sistema);


            if (modulo.Count <= 0)
            {
                CargarData_Bitacora(usuario);
            }
            else
            {
                string menu = OpcionesMenu.GetMenu(modulo, baseUrl);
                if (string.IsNullOrEmpty(menu))
                {
                    throw new Exception("El usuario no cuenta roles");
                }
                usuario.Menu = menu;
                usuario.Autenticacion = _configuracionSistema.Seguridad.EjecucionLogin;
                AsignarUsuario(usuario);
            }


        }

        private void CargarData_Bitacora(UsuarioSesion usuario)
        {
            var id_sistema = _configuracionSistema.Seguridad.CodigoSistema;
            var nombre_rol = _configuracionSistema.Seguridad.Nombre_Rol;
            var baseUrl = GetBaseUrl() + "/";
            var modulo = _usuarioService.Modulo_x_Rol(nombre_rol, id_sistema);
            string menu = OpcionesMenu.GetMenu(modulo, baseUrl);
            if (string.IsNullOrEmpty(menu))
            {
                throw new Exception("El usuario no cuenta roles");
            }

            usuario.Menu = menu;
            usuario.Autenticacion = _configuracionSistema.Seguridad.EjecucionLogin;
            AsignarUsuario(usuario);

        }

        private void AsignarUsuario(UsuarioSesion usuario)
        {

            var ejecucion = _configuracionSistema.Seguridad.EjecucionLogin;
            usuario.ID_PERSONAL = usuario.ID_PERSONAL == null ? "" : usuario.ID_PERSONAL;
            usuario.Nombre = usuario.Nombre == null ? "" : usuario.Nombre;
            usuario.UsuarioLogin = usuario.UsuarioLogin == null ? "" : usuario.UsuarioLogin;
            usuario.NOMBRE_OFICINA = usuario.NOMBRE_OFICINA == null ? "" : usuario.NOMBRE_OFICINA;
            usuario.Autenticacion = usuario.Autenticacion == null ? "" : usuario.Autenticacion;
            usuario.Token = usuario.Token == null ? "" : usuario.Token;
            usuario.Menu = usuario.Menu == null ? "" : usuario.Menu;
            usuario.SIGLA = usuario.SIGLA == null ? "" : usuario.SIGLA;
            usuario.ID_OFICINA = usuario.ID_OFICINA == null ? "" : usuario.ID_OFICINA;
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim("Id_Personal", usuario.ID_PERSONAL));
            identity.AddClaim(new Claim("Nombre", usuario.Nombre));
            identity.AddClaim(new Claim("Login", usuario.UsuarioLogin));
            identity.AddClaim(new Claim("Nombre_Oficina", usuario.NOMBRE_OFICINA));
            identity.AddClaim(new Claim("EjecucionLogin", ejecucion));
            identity.AddClaim(new Claim("Autenticacion", usuario.Autenticacion));
            identity.AddClaim(new Claim("Sigla", usuario.SIGLA));
            identity.AddClaim(new Claim("Menu", usuario.Menu));
            identity.AddClaim(new Claim("ID_OFICINA", usuario.ID_OFICINA));
            identity.AddClaim(new Claim(ClaimTypes.Name, usuario.Nombre));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Nombre));
            var principal = new ClaimsPrincipal(identity);
            Log.CreateLogger("USUARIO= " + usuario.UsuarioLogin);

            // Log.CreateLogger("Nombre= " + usuario.Nombre);
            // Log.CreateLogger("Tipo de Ejeucion= " + ejecucion);

            HttpContext.SignInAsync(principal, new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddDays(1),

            });


        }
        public string GetBaseUrl()
        {
            var request = HttpContext.Request;
            var host = request.Host.ToUriComponent();
            var pathBase = request.PathBase.ToUriComponent();
            return $"{request.Scheme}://{host}{pathBase}";
        }

        public IActionResult NoAutorizado()
        {

            var baseUrl = GetBaseUrl();
            var url_login = baseUrl + "/login/Index";
            ViewBag.UrlIntranet = url_login;
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();

        }

        public ActionResult Caducidad()
        {
            return View();
        }





        public IActionResult Salir()
        {

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var baseUrl = GetBaseUrl();
            var url = baseUrl + "/login/Index";
            return Redirect(url);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }

}
