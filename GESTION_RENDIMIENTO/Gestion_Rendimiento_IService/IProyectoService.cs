using Gestion_Rendimiento_Entity;
using Gestion_Rendimiento_Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Gestion_Rendimiento_IService
{
    public interface  IProyectoService
    {
        Proyecto Insertar(Proyecto item);
        Proyecto Actualizar(Proyecto item);
        string Proyecto_Min_Ano();
        BaseResponse ActualizarProyecto(Proyecto request);
    }
}
