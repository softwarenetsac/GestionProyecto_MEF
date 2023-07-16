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
    public class ProyectoDetalleService : Repository<Proyecto_Detalle>, IProyectoDetalleService
    {
        public ProyectoDetalleService(DatabaseContext context) : base(context)
        {
        }
        public Proyecto_Detalle Insertar(Proyecto_Detalle item)
=> Add(item);
        public Proyecto_Detalle Actualizar(Proyecto_Detalle item)
=> Update(item, item.ID_PROYECTO);
        public IEnumerable<Proyecto_Detalle> GetProyectoXId(int id)
       => FindAll(w => w.ID_PROYECTO == id);
    }
}
