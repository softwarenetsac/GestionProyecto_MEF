using Gestion_Rendimiento_Entity;
using Gestion_Rendimiento_Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_IService
{
    public interface IEvaluadoService
    {
        IEnumerable<EvaluadoModel> GetAll(EvaluadoModel modelo);
        IEnumerable<EvaluadoModel> GetAll_Evaluador(EvaluadoModel modelo);
    }
}
