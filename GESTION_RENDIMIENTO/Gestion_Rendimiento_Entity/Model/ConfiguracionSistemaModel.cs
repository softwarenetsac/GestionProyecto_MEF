using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_Entity.Model
{
  public   class ConfiguracionSistemaModel
    {
        public static ConfiguracionSistemaModel ConfiguracionActual;

        public string NombreSistema { get; set; }
        public string AbreviaturaSistema { get; set; }
        public string EsquemaPrincipalBD { get; set; }
        public ConfiguracionCorreo Correo { get; set; }
        public ConfiguracionErrores Errores { get; set; }
        public ConfiguracionSSO SSO { get; set; }
        public ConfiguracionJWT JWT { get; set; }
        public ConfiguracionSharePoint SharePoint { get; set; }
        public ConfiguracionPIDE PIDE { get; set; }
        public ConfiguracionSeguridad Seguridad { get; set; }
        public AccesoPrueba UsuarioPrueba { get; set; }
        public Variable VariableGeneral { get; set; }
        public ApiLF ApiLaserfiche { get; set; }
        public class ConfiguracionCorreo
        {
            public string Cuenta { get; set; }
            public string Contrasenia { get; set; }
            public string NombreCuenta { get; set; }
            public string Servidor { get; set; }
            public int Puerto { get; set; }
            public bool SSL { get; set; }
            public string Remitente { get; set; }
            public string Clave { get; set; }
            public bool EnableSsl { get; set; }
            public string Copia { get; set; }
            public string CopiaOculta { get; set; }
            public string PruebaLocal { get; set; }
        }

        public class ConfiguracionErrores
        {
            public bool NotificarPorCorreo { get; set; }
            public string CorreosDestinatarios { get; set; }
            public string CorreosCopia { get; set; }
        }
        public class ApiLF
        {
            public string ApiUrl { get; set; }
            public string TokenLf { get; set; }
        }
        public class AccesoPrueba
        {
            public string Id_Persona { get; set; }
            public string Nombre { get; set; }
            public string UsuarioLogin { get; set; }

        }
        public class ConfiguracionSSO
        {
            public string URLSistema { get; set; }
            public string ServicioSeguridad { get; set; }
            public string IdAplicacion { get; set; }
            public string Usuario { get; set; }
            public string Contrasenia { get; set; }
        }

        public class ConfiguracionJWT
        {
            public string Llave { get; set; }
        }



        public class Variable
        {
            public int Dia { get; set; }
            public int HoraPermitido { get; set; }
            public int DiaPermitido { get; set; }
            public int IdAdelantoVacacionXDia { get; set; }
            public int IdFraccionamientoVacXHora { get; set; }
            public int IdFraccionamientoVacXDia { get; set; }
            public decimal TareoxFila { get; set; }
            public decimal SumaTareoxDia { get; set; }

        }



        public class ConfiguracionSharePoint
        {
            public string Servidor { get; set; }
            public string BibliotecaOficios { get; set; }
            public string Usuario { get; set; }
            public string Contrasenia { get; set; }
            public string Dominio { get; set; }
        }

        public class ConfiguracionPIDE
        {
            public string Usuario { get; set; }
            public string Password { get; set; }
        }

        public class ConfiguracionSeguridad
        {
            public int CodigoSistema { get; set; }
            public string EjecucionLogin { get; set; }
            public string TokenLocal { get; set; }
            public string NoAutorizado { get; set; }
            public string LoginUrl { get; set; }
            public string UrlSistema { get; set; }
            public string UrlExterno { get; set; }
            public string Dominio { get; set; }
            public string PathLogin { get; set; }
            public bool IsEssential { get; set; }
            public bool HttpOnly { get; set; }
            public bool Secure { get; set; }

            public string LDAP { get; set; }

            public string Nombre_Rol { get; set; }
            public string FlgCrearRol { get; set; }
        }


    }
}
