using Gestion_Rendimiento_Data;
using Gestion_Rendimiento_Entity;
using Gestion_Rendimiento_Entity.Model;
using Gestion_Rendimiento_IService;
using Gestion_Rendimiento_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_Service
{
    public class ProyectoService:Repository<Proyecto>, IProyectoService
    {
        public ProyectoService(DatabaseContext context) : base(context)
        {
        }
        public Proyecto Insertar(Proyecto item)
     => Add(item);
        public Proyecto Actualizar(Proyecto item)
    => Update(item, item.ID_PROYECTO);
        public string Proyecto_Min_Ano()
        {
            var detalle = _context.Set<Proyecto>().Min(x=> x.ANIO);
            return detalle;
        }
    }
}
