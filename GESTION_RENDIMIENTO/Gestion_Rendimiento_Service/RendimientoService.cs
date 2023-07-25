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
        public IEnumerable<RendimientoConsultaModel> GetAllProyectoEvaluadoAnio(string ID_PERSONAL, string ANIO)
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
           }).ToList().Where(x => x.ID_PERSONAL == ID_PERSONAL && x.ANIO== ANIO && x.FLG_ESTADO=="1").ToList();
            return lista;
        }

        public List<ReporteRendimientoModel> GetReporteRendimiento(string ID_PERSONAL, string ANIO)
        {
            var lista = _context.ReporteRendimiento
           .Select(t => new ReporteRendimientoModel
           {
               ID_PROYECTO = t.ID_PROYECTO ?? 0,
               NOMBRE_ENTIDAD = string.IsNullOrEmpty(t.NOMBRE_ENTIDAD) ? "" : t.NOMBRE_ENTIDAD,
               DESCRIPCION = string.IsNullOrEmpty(t.DESCRIPCION) ? "" : t.DESCRIPCION,
               ID_PERSONAL = string.IsNullOrEmpty(t.ID_PERSONAL) ? "" : t.ID_PERSONAL,
               ID_OFICINA = t.ID_OFICINA ?? 0,
               ID_ESTADO = string.IsNullOrEmpty(t.ID_ESTADO) ? "" : t.ID_ESTADO,
               FLG_ESTADO = string.IsNullOrEmpty(t.FLG_ESTADO) ? "" : t.FLG_ESTADO,
               ID_EVALUADOR = string.IsNullOrEmpty(t.ID_EVALUADOR) ? "" : t.ID_EVALUADOR,
               ANIO = string.IsNullOrEmpty(t.ANIO) ? "" : t.ANIO,
               FECHA_REGISTRO = string.IsNullOrEmpty(t.FECHA_REGISTRO) ? "" : t.FECHA_REGISTRO,
               DNI_EVALUADO = string.IsNullOrEmpty(t.DNI_EVALUADO) ? "" : t.DNI_EVALUADO,
               NOMBRE_EVALUADO = string.IsNullOrEmpty(t.NOMBRE_EVALUADO) ? "" : t.NOMBRE_EVALUADO,
               NOMBRE_CARGO_EVALUADO = string.IsNullOrEmpty(t.NOMBRE_CARGO_EVALUADO) ? "" : t.NOMBRE_CARGO_EVALUADO,
               NOMBRE_SEGMENTO_EVALUADO = string.IsNullOrEmpty(t.NOMBRE_SEGMENTO_EVALUADO) ? "" : t.NOMBRE_SEGMENTO_EVALUADO,
               NOMBRE_ORGANO_EVALUADO = string.IsNullOrEmpty(t.NOMBRE_ORGANO_EVALUADO) ? "" : t.NOMBRE_ORGANO_EVALUADO,
               NOMBRE_EVALUADOR = string.IsNullOrEmpty(t.NOMBRE_EVALUADOR) ? "" : t.NOMBRE_EVALUADOR,
               NOMBRE_CARGO_EVALUADOR = string.IsNullOrEmpty(t.NOMBRE_CARGO_EVALUADOR) ? "" : t.NOMBRE_CARGO_EVALUADOR,
               NOMBRE_SEGMENTO_EVALUADOR = string.IsNullOrEmpty(t.NOMBRE_SEGMENTO_EVALUADOR) ? "" : t.NOMBRE_SEGMENTO_EVALUADOR,
               NOMBRE_ORGANO_EVALUADOR = string.IsNullOrEmpty(t.NOMBRE_ORGANO_EVALUADOR) ? "" : t.NOMBRE_ORGANO_EVALUADOR,
               NOMBRE_ESTADO = string.IsNullOrEmpty(t.NOMBRE_ESTADO) ? "" : t.NOMBRE_ESTADO,
               //ID_DETALLE_PROYECTO = t.ID_DETALLE_PROYECTO,
               INDICADOR_PRODUCTO = string.IsNullOrEmpty(t.INDICADOR_PRODUCTO) ? "" : t.INDICADOR_PRODUCTO,
               VALOR = t.VALOR ?? 0,
               PESO = t.PESO ?? 0,
               EVIDENCIA = string.IsNullOrEmpty(t.EVIDENCIA) ? "" : t.EVIDENCIA,
               PLAZO = string.IsNullOrEmpty(t.PLAZO) ? "" : t.PLAZO,
               TIPO_FORMULA = string.IsNullOrEmpty(t.TIPO_FORMULA) ? "" : t.TIPO_FORMULA
           }).ToList().Where(x => x.ID_PERSONAL == ID_PERSONAL && x.ANIO == ANIO).ToList();
            return lista;

        }
    }
}
