using Gestion_Rendimiento_Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gestion_Rendimiento_IService
{
    public interface IVariableService
    {
        IEnumerable<Variable> ListarVariable(string sistema, string campo);
    }
}
