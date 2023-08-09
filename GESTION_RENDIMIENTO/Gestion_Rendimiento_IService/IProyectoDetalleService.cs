using Gestion_Rendimiento_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_IService
{
    public interface IProyectoDetalleService
    {
        Proyecto_Detalle Insertar(Proyecto_Detalle item);
        Proyecto_Detalle Actualizar(Proyecto_Detalle item);
        IEnumerable<Proyecto_Detalle> GetProyectoXId(int id);
        BaseResponse DeleteDetalle(Proyecto_Detalle item);
        // 
    }
}
