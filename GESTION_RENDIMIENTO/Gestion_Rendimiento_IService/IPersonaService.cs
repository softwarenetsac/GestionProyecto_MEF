
using Gestion_Rendimiento_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_IService
{
  public   interface IPersonaService
    {
        List<Persona> GetTodos(string flg_estado = "_%", string locador = "_%");
         Persona Detalle(string id_Persona);
        List<Persona> GetTodosXRegimen(int id_regimen_laboral);
        Persona DetallePorUsuario(string usuario, string flg_estado = "1");
        List<Persona> GetTodosXUnidad(int id_area);

    }
}
