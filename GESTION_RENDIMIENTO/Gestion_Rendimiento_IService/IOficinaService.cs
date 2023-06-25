
using Gestion_Rendimiento_Entity;
using Gestion_Rendimiento_Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_IService
{
  public   interface IOficinaService 
    {


        IEnumerable<Oficina> GetOrganosTodos();
        IEnumerable<Oficina> GetOrganos(string tipo);
        IEnumerable<Oficina> GetUnidadOrganicas(int id_area, string tipo);
        IEnumerable<Oficina> GetOficinasXId(int  id);
        IEnumerable<Oficina> GetTodos(OficinaModel modelo);


        //   IEnumerable<Oficina> GetUnidadOrganicaAsync(int id_area, string tipo);

    }
}
