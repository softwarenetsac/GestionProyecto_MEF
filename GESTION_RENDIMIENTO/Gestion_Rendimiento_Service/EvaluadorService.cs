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
    public class EvaluadorService : Repository<Evaluador>, IEvaluadorService
    {

        public EvaluadorService(DatabaseContext context) : base(context)
        {
        }


        public Evaluador Actualizar(Evaluador item)
    => Update(item, item.ID_EVALUADOR);

        public Evaluador Insertar(Evaluador item)
         => Add(item);

        public IQueryable<Evaluador> GetResponsable(int id_area, int id_oficina, string id_personal)
        {
            IQueryable<Evaluador> query = Entities;
            if (id_area > 0)
            {
                query = query.Where(w => w.ID_AREA_JEFE.Equals(id_area.ToString()));
            }
            if (id_oficina > 0)
            {
                query = query.Where(w => w.ID_OFICINA_JEFE.Equals(id_oficina.ToString())
                );
            }
            if (!string.IsNullOrEmpty(id_personal))
            {
                query = query.Where(w => w.ID_PERSONA_JEFE.Equals(id_personal)
                && (string.IsNullOrEmpty(w.ID_PERSONA_ALTERNO) || w.ID_PERSONA_ALTERNO == id_personal)
                );
            }
            return query;
        }

        public Evaluador Detalle(int id)
        {
            var detalle = _context.Set<Evaluador>().Find(id);
            return detalle;
        }

        public Evaluador Anular(Evaluador item)
        {
            var entidad = Detalle(item.ID_EVALUADOR);
            entidad.FLG_ESTADO = item.FLG_ESTADO;
            return Update(entidad, entidad.ID_EVALUADOR);
        }

        public IEnumerable<EvaluadorConsultaModel> GetAll(int id_area, int id_oficina, string id_personal)
        {

            var lista = _context.EvaluadorConsulta
.Select(t => new EvaluadorConsultaModel
{
    ID_EVALUADOR = t.ID_EVALUADOR,
    ID_AREA = t.ID_AREA,
    ID_OFICINA = t.ID_OFICINA,
    NOMBRE_AREA = t.NOMBRE_AREA,
    NOMBRE_OFICINA = t.NOMBRE_OFICINA,
    ID_PERSONA = t.ID_PERSONA,
    APELLIDO_PATERNO = t.APELLIDO_PATERNO,
    APELLIDO_MATERNO = t.APELLIDO_MATERNO,
    NOMBRE = t.NOMBRE,
    NOMBRE_COMPLETO = t.NOMBRE_COMPLETO,
    NUMERO_DNI = t.NUMERO_DNI,
    NOMBRE_CARGO = t.NOMBRE_CARGO,
    NOMBRE_CATEGORIA = t.NOMBRE_CATEGORIA,
    CORREO_INSTITUCIONAL = t.CORREO_INSTITUCIONAL,
    USUARIO_LOGIN = t.USUARIO_LOGIN,
    ID_SEXO = t.ID_SEXO,
    ID_SITUACION_LABORAL = t.ID_SITUACION_LABORAL,
    FECHA_INGRESO =t.FECHA_INGRESO,
    FECHA_NACIMIENTO = t.FECHA_NACIMIENTO,
    TOTAL_EVALUADO= t.TOTAL_EVALUADO
}).ToList().Where(x => x.ID_AREA == (id_area == 0 ? x.ID_AREA : id_area) &&
            x.ID_OFICINA == (id_oficina == 0 ? x.ID_OFICINA : id_oficina) &&
            x.ID_PERSONA == (string.IsNullOrEmpty(id_personal) ? x.ID_PERSONA : id_personal)).ToList();

            return lista;

        }
    }
}
