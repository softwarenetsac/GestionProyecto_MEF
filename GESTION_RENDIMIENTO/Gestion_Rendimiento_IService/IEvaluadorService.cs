using Gestion_Rendimiento_Entity;
using Gestion_Rendimiento_Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_IService
{
 public interface    IEvaluadorService
    {

        /// <summary>
        /// Crear registro de Autorizador.
        /// </summary>
        /// <param name="Autorizador"></param>
        /// <returns></returns>
        Evaluador Insertar(Evaluador item);

        /// <summary>
        /// Actualizar regsitro de Autorizador.
        /// </summary>
        /// <param name="Autorizador"></param>
        /// <returns></returns>
        Evaluador Actualizar(Evaluador item);

        IEnumerable<Evaluador> GetAll();
        IQueryable<Evaluador> GetResponsable(int id_area, int id_oficina, string id_personal);
        IEnumerable<EvaluadorConsultaModel> GetAll(int id_area, int id_oficina, string id_personal);
        Evaluador Detalle(int id);

        Evaluador Anular(Evaluador item);

    }
}
