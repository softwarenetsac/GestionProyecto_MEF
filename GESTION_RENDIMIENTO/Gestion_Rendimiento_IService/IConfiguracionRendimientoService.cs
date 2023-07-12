using Gestion_Rendimiento_Entity;
using Gestion_Rendimiento_Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_IService
{
  public interface IConfiguracionRendimientoService
    {
        ConfiguracionRendimiento Insertar(ConfiguracionRendimientoModel item);
        ConfiguracionRendimiento Actualizar(ConfiguracionRendimientoModel item);
        IEnumerable<ConfiguracionRendimientoModel> GetAll(ConfiguracionRendimientoModel modelo);
        IEnumerable<ConfiguracionDetalleModel> GetAllDetalle(ConfiguracionDetalleModel modelo);
    }
}
