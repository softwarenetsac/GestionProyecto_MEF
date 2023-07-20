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
        public RendimientoConsultaModel GetOne(int ID_PROYECTO)
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
            PLAZO = t.PLAZO,
        }).ToList().Where(x => x.ID_PROYECTO == ID_PROYECTO).First();
            return lista;
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
            PLAZO = t.PLAZO,
            NOMBRE_OFICINA = t.NOMBRE_OFICINA
        }).ToList().Where(x => x.ID_OFICINA == (modelo.ID_OFICINA == 0 ? x.ID_OFICINA : modelo.ID_OFICINA) &&
               x.ID_PERSONAL == (string.IsNullOrEmpty(modelo.ID_PERSONAL) ? modelo.ID_PERSONAL : modelo.ID_PERSONAL)).ToList();
            return lista;
        }
        public IEnumerable<RendimientoConsultaModel> GetAll(RendimientoConsultaModel request)
        {
            if (request.TIPO=="U")
            {
                var lista = _context.RendimientoConsulta
.Select(t => new RendimientoConsultaModel
{
ID_PROYECTO = t.ID_PROYECTO,
DESCRIPCION = t.DESCRIPCION,
ID_OFICINA = t.ID_OFICINA,
ID_PERSONAL = t.ID_PERSONAL,
ID_ESTADO = t.ID_ESTADO,
ID_EVALUADOR = t.ID_EVALUADOR,
FLG_ESTADO = t.FLG_ESTADO,
ANIO = t.ANIO,
PLAZO = t.PLAZO,
NOMBRE_EVALUADO = t.NOMBRE_EVALUADO,
NOMBRE_CARGO = t.NOMBRE_CARGO,
NOMBRE_EVALUADOR = t.NOMBRE_EVALUADOR,
NOMBRE_ESTADO = t.NOMBRE_ESTADO,
    NOMBRE_OFICINA = t.NOMBRE_OFICINA
}).ToList().Where(x => x.ID_PERSONAL == request.ID_PERSONAL).ToList();
                return lista;
            }
            else if (request.TIPO == "ORH")
            {
                var lista = _context.RendimientoConsulta
.Select(t => new RendimientoConsultaModel
{
ID_PROYECTO = t.ID_PROYECTO,
DESCRIPCION = t.DESCRIPCION,
ID_OFICINA = t.ID_OFICINA,
    ID_PERSONAL = t.ID_PERSONAL,
ID_ESTADO = t.ID_ESTADO,
ID_EVALUADOR = t.ID_EVALUADOR,
FLG_ESTADO = t.FLG_ESTADO,
ANIO = t.ANIO,
PLAZO = t.PLAZO,
NOMBRE_EVALUADO = t.NOMBRE_EVALUADO,
NOMBRE_CARGO = t.NOMBRE_CARGO,
NOMBRE_EVALUADOR = t.NOMBRE_EVALUADOR,
NOMBRE_ESTADO = t.NOMBRE_ESTADO,
    NOMBRE_OFICINA=t.NOMBRE_OFICINA
}).ToList().Where(x => x.ID_OFICINA== (request.ID_OFICINA == 0 ? x.ID_OFICINA : request.ID_OFICINA) && x.ANIO== request.ANIO).ToList();
                return lista;
            }
            else
            {
                var lista   = _context.RendimientoConsulta
.Select(t => new RendimientoConsultaModel
{
ID_PROYECTO = t.ID_PROYECTO,
DESCRIPCION = t.DESCRIPCION,
ID_OFICINA = t.ID_OFICINA,
ID_PERSONAL = t.ID_PERSONAL,
ID_ESTADO = t.ID_ESTADO,
ID_EVALUADOR = t.ID_EVALUADOR,
FLG_ESTADO = t.FLG_ESTADO,
ANIO = t.ANIO,
PLAZO = t.PLAZO,
NOMBRE_EVALUADO = t.NOMBRE_EVALUADO,
NOMBRE_CARGO = t.NOMBRE_CARGO,
NOMBRE_EVALUADOR = t.NOMBRE_EVALUADOR,
NOMBRE_ESTADO = t.NOMBRE_ESTADO,
    NOMBRE_OFICINA = t.NOMBRE_OFICINA
}).ToList();
                return lista;
            }
  


        }

    }
}
