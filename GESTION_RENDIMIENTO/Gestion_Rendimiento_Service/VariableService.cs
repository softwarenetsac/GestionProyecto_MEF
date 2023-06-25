
using Gestion_Rendimiento_Data;
using Gestion_Rendimiento_Entity;
using Gestion_Rendimiento_IService;
using Gestion_Rendimiento_Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gestion_Rendimiento_Service
{
    public class VariableService : Repository<Variable>, IVariableService
    {
        public VariableService(DatabaseContext context) : base(context)
        {
        }
        public IEnumerable<Variable> ListarVariable(string sistema, string campo)
           => FindAll(w => w.SISTEMA == sistema && w.CAMPO == campo && w.FLG_ESTADO == "1");
    }
}
