using Gestion_Rendimiento_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_IService
{
    public interface IProyectoSeguimientoService
    {
        BaseResponse Insertar(ProyectoSeguimiento item);
        IEnumerable<ProyectoSeguimientoConsulta> GetAllSeguimientoProyecto(int ID_PROYECTO);
        BaseResponse DeleteSeguimiento(ProyectoSeguimiento request);
    }
}
