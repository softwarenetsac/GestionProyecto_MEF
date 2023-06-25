
using Gestion_Rendimiento_Data;
using Gestion_Rendimiento_Entity;
using Gestion_Rendimiento_Entity.Model;
using Gestion_Rendimiento_IService;
using Gestion_Rendimiento_Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_Service
{
    public class OficinaService : Repository<Oficina>, IOficinaService
    {

        public OficinaService(DatabaseContext context) : base(context)
        {
        }



        public IEnumerable<Oficina> GetOficinasXId(int id)
           => FindAll(w => w.ID_OFICINA == id);
        public IEnumerable<Oficina> GetOrganos(string tipo)
         => FindAll(w => w.TIPO == tipo);

        public IEnumerable<Oficina> GetOrganosTodos()
         => GetAll();

        public IEnumerable<Oficina> GetUnidadOrganicas(int id_area, string tipo)
        {
            IQueryable<Oficina> query = Entities;

            query = query.Where(w => w.ID_AREA == id_area);

            return query;
        }


     public   IEnumerable<Oficina> GetTodos(OficinaModel modelo)
        {
            IQueryable<Oficina> query = Entities;

            if (modelo.ID_AREA > 0)
            {
                query = query.Where(w => w.ID_AREA == modelo.ID_AREA);
            }
            if (!string.IsNullOrEmpty(modelo.SIGLA))
            {
                query = query.Where(w => w.SIGLA.Contains(modelo.SIGLA.Trim().ToUpper()));
            }
            return query;
        }
    }
}
