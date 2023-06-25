
using Gestion_Rendimiento_Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gestion_Rendimiento_IService
{

   public interface   IUsuarioService
    {
        UsuarioSesion Usuario_Detalle(string usuario, int id_sistema);
        List<Modulo> Modulo(int id_usuario, int id_sistema);
        List<Modulo> Modulo_x_Rol(string nombre_rol, int id_sistema);
        BaseResponse GenerarUsuario(UsuarioSeguridad item);
        BaseResponse AnularUsuario(UsuarioSeguridad item);
    }
}
