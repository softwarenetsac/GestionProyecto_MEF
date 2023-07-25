using Gestion_Rendimiento_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_IService
{
    public interface IProyectoDetalleSubService
    {
        Proyecto_Detalle_Sub Insertar(Proyecto_Detalle_Sub item);
        Proyecto_Detalle_Sub Actualizar(Proyecto_Detalle_Sub item);
        IEnumerable<Proyecto_Detalle_Sub> GetEvidenciaXProyecto();
    }
}
