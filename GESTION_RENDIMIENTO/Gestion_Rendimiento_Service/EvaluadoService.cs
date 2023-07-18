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
    public class EvaluadoService : Repository<Evaluado>, IEvaluadoService
    {
        public EvaluadoService(DatabaseContext context) : base(context)
        {
        }
        public IEnumerable<EvaluadoModel> GetAll(EvaluadoModel modelo)
        {

            var lista = _context.Evaluado
.Select(t => new EvaluadoModel
{
    NOMBRE_COMPLETO = t.NOMBRE_COMPLETO,
    NOMBRE_CARGO = t.NOMBRE_CARGO,
    NOMBRE_CATEGORIA = t.NOMBRE_CATEGORIA,
    CORREO_INSTITUCIONAL = t.CORREO_INSTITUCIONAL,
    NOMBRE_OFICINA = t.NOMBRE_OFICINA,
    NOMBRE_AREA = t.NOMBRE_AREA,
    NOMBRE_EVALUADOR = t.NOMBRE_EVALUADOR,
    NUMERO_DNI = t.NUMERO_DNI,
    ANIO = t.ANIO,
    FECHA_INGRESO = t.FECHA_INGRESO,
    ID_EVALUADOR = t.ID_EVALUADOR,
    ID_AREA = t.ID_AREA,
    ID_SITUACION_LABORAL = t.ID_SITUACION_LABORAL,
    ID_OFICINA = t.ID_OFICINA,
    ID_PERSONAL = t.ID_PERSONAL,
}).ToList().Where(
                x => x.ID_OFICINA == (modelo.ID_OFICINA == 0 ? x.ID_OFICINA : modelo.ID_OFICINA) &&
                x.ID_AREA == (modelo.ID_AREA == 0 ? x.ID_AREA : modelo.ID_AREA) &&
                x.ID_EVALUADOR == (string.IsNullOrEmpty(modelo.ID_EVALUADOR) ? x.ID_EVALUADOR : modelo.ID_EVALUADOR)
                ).ToList().Distinct();
            return lista;
        }


        public IEnumerable<EvaluadoModel> GetAllxEvaluador(EvaluadoModel modelo)
        {

       var lista = _context.Evaluado
   .Select(t => new EvaluadoModel
   {
       NOMBRE_COMPLETO = t.NOMBRE_COMPLETO,
       NOMBRE_CARGO = t.NOMBRE_CARGO,
       NOMBRE_CATEGORIA = t.NOMBRE_CATEGORIA,
       CORREO_INSTITUCIONAL = t.CORREO_INSTITUCIONAL,
       NOMBRE_OFICINA = t.NOMBRE_OFICINA,
       NOMBRE_AREA = t.NOMBRE_AREA,
       NUMERO_DNI = t.NUMERO_DNI,
       ANIO = t.ANIO,
       FECHA_INGRESO = t.FECHA_INGRESO,
       ID_AREA = t.ID_AREA,
       ID_SITUACION_LABORAL = t.ID_SITUACION_LABORAL,
       ID_OFICINA = t.ID_OFICINA,
       ID_PERSONAL = t.ID_PERSONAL,
   }).ToList().Where(
                   x => x.ID_OFICINA == (modelo.ID_OFICINA == 0 ? x.ID_OFICINA : modelo.ID_OFICINA) &&
                   x.ID_AREA == (modelo.ID_AREA == 0 ? x.ID_AREA : modelo.ID_AREA) &&
                   x.ID_EVALUADOR == (string.IsNullOrEmpty(modelo.ID_EVALUADOR) ? x.ID_EVALUADOR : modelo.ID_EVALUADOR)
                   ).ToList().Distinct();
            return lista;
        }
 
}

}
