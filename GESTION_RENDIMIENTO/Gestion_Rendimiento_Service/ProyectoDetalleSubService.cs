using Gestion_Rendimiento_Data;
using Gestion_Rendimiento_Entity;
using Gestion_Rendimiento_IService;
using Gestion_Rendimiento_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_Service
{
    public class ProyectoDetalleSubService : Repository<Proyecto_Detalle_Sub>, IProyectoDetalleSubService
    {
        public ProyectoDetalleSubService(DatabaseContext context) : base(context)
        {
        }
        public Proyecto_Detalle_Sub Insertar(Proyecto_Detalle_Sub item)
=> Add(item);
        public Proyecto_Detalle_Sub Actualizar(Proyecto_Detalle_Sub item)
=> Update(item, item.ID_DETALLE_SUB);
        public IEnumerable<Proyecto_Detalle_Sub> GetEvidenciaXProyecto()
        {
            var detalle = _context.Set<Proyecto_Detalle_Sub>().ToList();
            return detalle;
        }

    }
}
