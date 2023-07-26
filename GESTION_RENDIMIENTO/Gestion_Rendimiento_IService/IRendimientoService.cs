using Gestion_Rendimiento_Entity;
using Gestion_Rendimiento_Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_IService
{
 public    interface IRendimientoService
    {
        RendimientoConsultaModel GetOne(int ID_PROYECTO);
        IEnumerable<RendimientoConsultaModel> GetRenmientoEvaluado(RendimientoModel modelo);
        IEnumerable<RendimientoConsultaModel> GetAll(RendimientoConsultaModel request);
        IEnumerable<RendimientoConsultaModel> GetAllProyectoEvaluadoAnio(string ID_PERSONAL, string ANIO);
     //   List<ReporteRendimientoModel> GetReporteRendimiento(string ID_PERSONAL, string ANIO);
    }
}
