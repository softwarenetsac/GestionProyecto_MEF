
using Gestion_Rendimiento_Data;
using Gestion_Rendimiento_Entity;
using Gestion_Rendimiento_IService;
using Gestion_Rendimiento_Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Gestion_Rendimiento_Service
{
    public class PersonaService : Repository<Persona>, IPersonaService
    {

        public PersonaService(DatabaseContext context) : base(context)
        {
        }

        public Persona Detalle(string id_Persona)
       => Get(id_Persona);

        public List<Persona> GetTodos(string flg_estado= "_%", string locador= "_%" )
        {
            try
            {
               // var lista = this.GetAllList();
                var lista = FindList(x => x.FLG_ESTADO == (flg_estado == "_%" ? x.FLG_ESTADO : flg_estado) &&  x.LOCADOR == (locador == "_%" ? x.LOCADOR : locador));
                return lista;
            }
            catch (Exception)
            {
                return new List<Persona>();
            }
        }
        public List<Persona> GetTodosXRegimen(int id_regimen_laboral)
        {
            try
            {
                var lista = FindList(p => p.ID_SITUACION_LABORAL == (id_regimen_laboral == 0 ? p.ID_SITUACION_LABORAL : id_regimen_laboral));
                return lista;
            }
            catch (Exception)
            {
                return new List<Persona>();
            }
        }
        public Persona DetallePorUsuario(string usuario, string flg_estado="1")
        {
            try
            {
                usuario = usuario == null ? "" : usuario.Trim().ToUpper();
                /*
                Expression<Func<Persona, bool>> filtro = f => f.USUARIO_LOGIN.ToUpper().Equals(usuario.ToUpper());
                return Find(filtro);
                */
                usuario = usuario == null ? "" : usuario.Trim();
               var data=  this.Find(x => x.USUARIO_LOGIN.ToUpper().Equals(usuario) && x.FLG_ESTADO.Trim()==flg_estado);
                return data;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public List<Persona> GetTodosXUnidad(int id_area)
        {
            try
            {
                var lista = FindList(p => p.ID_SITUACION_LABORAL == 0 && p.ID_AREA == id_area);
                return lista;
            }
            catch (Exception)
            {
                return new List<Persona>();
            }
        }

    }
}
