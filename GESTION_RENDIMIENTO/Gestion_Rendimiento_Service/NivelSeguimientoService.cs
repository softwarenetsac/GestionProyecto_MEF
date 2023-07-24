using Gestion_Rendimiento_Common;
using Gestion_Rendimiento_Data;
using Gestion_Rendimiento_Entity;
using Gestion_Rendimiento_Entity.Model;
using Gestion_Rendimiento_IService;
using Gestion_Rendimiento_Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_Service
{
    public class NivelSeguimientoService : Repository<NivelSeguimiento>, INivelSeguimientoService
    {
        public NivelSeguimientoService(DatabaseContext context) : base(context)
        {
        }
        public IEnumerable<NivelSeguimiento> GetListNivel()
            => FindAll(w => w.FLG_ESTADO == "1");
    }
}
