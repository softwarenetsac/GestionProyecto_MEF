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
    public class RendimientoService : Repository<Rendimiento>, IRendimientoService
    {

        public RendimientoService(DatabaseContext context) : base(context)
        {
        }

        public IEnumerable<RendimientoConsultaModel> GetRenmientoEvaluado(RendimientoModel modelo)
        {

            var lista = _context.RendimientoConsulta
        .Select(t => new RendimientoConsultaModel
        {
            ID_EVALUADOR = t.ID_EVALUADOR,
            ID_ESTADO = t.ID_ESTADO,
            ID_OFICINA = t.ID_OFICINA,
            ID_PERSONAL = t.ID_PERSONAL,
            ID_PROYECTO = t.ID_PROYECTO,
            ANIO = t.ANIO,
            FLG_ESTADO = t.FLG_ESTADO,
            NOMBRE_CARGO = t.NOMBRE_CARGO,
            NOMBRE_EVALUADO = t.NOMBRE_EVALUADO,
            NOMBRE_ESTADO = t.NOMBRE_ESTADO,
            NOMBRE_EVALUADOR = t.NOMBRE_EVALUADOR,
            DESCRIPCION = t.DESCRIPCION,
            PLAZO = t.PLAZO
        }).ToList().Where(x => x.ID_OFICINA == (modelo.ID_OFICINA == 0 ? x.ID_OFICINA : modelo.ID_OFICINA) &&
               x.ID_PERSONAL == (string.IsNullOrEmpty(modelo.ID_PERSONAL) ? modelo.ID_PERSONAL : modelo.ID_PERSONAL)).ToList();
            return lista;
        }


    }
}
