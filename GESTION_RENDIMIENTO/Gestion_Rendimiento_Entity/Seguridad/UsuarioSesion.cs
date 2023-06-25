using System;

namespace Gestion_Rendimiento_Entity
{
    public class UsuarioSesion
    {
        public string UsuarioLogin { get; set; }

        public string Menu { get; set; }
        public string Nombre { get; set; }

        public string IdPerfil { get; set; }

        public string ID_AREA { get; set; }
        public string NOMBRE_AREA { get; set; }
        public string ID_OFICINA { get; set; }
        public string NOMBRE_OFICINA { get; set; }
        public string Token { get; set; }
        public string ExpiracionToken { get; set; }
        public string ID_PERSONAL { get; set; }
        public string EjecucionLocal { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public int ID_SISTEMA { get; set; }
        public string Autenticacion { get; set; }
        public string Ip_Registro { get; set; }
        public int ID_USUARIO { get; set; }
        public string EjecucionLogin { get; set; }
        public string SIGLA { get; set; }
    }
}
