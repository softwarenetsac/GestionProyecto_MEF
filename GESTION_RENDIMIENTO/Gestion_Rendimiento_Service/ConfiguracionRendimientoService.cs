using Gestion_Rendimiento_Common;
using Gestion_Rendimiento_Data;
using Gestion_Rendimiento_Entity;
using Gestion_Rendimiento_Entity.Model;
using Gestion_Rendimiento_IService;
using Gestion_Rendimiento_Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gestion_Rendimiento_Service
{
    public class ConfiguracionRendimientoService : Repository<ConfiguracionRendimiento>, IConfiguracionRendimientoService
    {

        public ConfiguracionRendimientoService(DatabaseContext context) : base(context)
        {
        }
        public ConfiguracionRendimiento Actualizar(ConfiguracionRendimientoModel item)
        {
            throw new NotImplementedException();
        }

        public ConfiguracionRendimiento Insertar(ConfiguracionRendimientoModel item)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<ConfiguracionRendimientoModel> GetAll(ConfiguracionRendimientoModel modelo)
        {

            IEnumerable<ConfiguracionRendimientoModel> listaDetalle;
            try
            {

                listaDetalle = (from e in _context.ConfiguracionRendimiento
                                join d in _context.TipoGestionRendimiento on e.ID_TIPO_GESTION equals d.ID_TIPO_GESTION
                                where ((d.ABREVIATURA.Trim().ToUpper().Equals(modelo.ABREVIATURA_TIPO.Trim().ToUpper()) &&
                                e.FLG_ESTADO == (string.IsNullOrEmpty(modelo.FLG_ESTADO) ? e.FLG_ESTADO : modelo.FLG_ESTADO)
                                ))
                                select new ConfiguracionRendimientoModel
                                {
                                    ID_CONFIGURACION = e.ID_CONFIGURACION,
                                    ID_TIPO_GESTION = e.ID_TIPO_GESTION,
                                    ABREVIATURA_TIPO = d.ABREVIATURA,
                                    ABREVIATURA_CON = e.ABREVIATURA,
                                    DESCRIPCION = e.DESCRIPCION,
                                    TIPO_DESCRIPCION = d.DESCRIPCION,
                                    FLG_ESTADO = e.FLG_ESTADO
                                });
            }
            catch (Exception ex)
            {
                Log.CreateLogger(ex.Message);
                listaDetalle = new List<ConfiguracionRendimientoModel>();
            }

            return listaDetalle;
        }

        public IEnumerable<ConfiguracionDetalleModel> GetAllDetalle(ConfiguracionDetalleModel modelo)
        {
            IEnumerable<ConfiguracionDetalleModel> listaDetalle;
            try
            {
                listaDetalle = (from e in _context.ConfiguracionRendimiento
                                join d in _context.ConfiguracionDetalle on e.ID_CONFIGURACION equals d.ID_CONFIGURACION
                                where (((d.ID_CONFIGURACION == (modelo.ID_CONFIGURACION == 0 ? d.ID_CONFIGURACION : modelo.ID_CONFIGURACION)) &&
                                d.FLG_ESTADO == (string.IsNullOrEmpty(modelo.FLG_ESTADO) ? d.FLG_ESTADO : modelo.FLG_ESTADO)
                                ))
                                select new ConfiguracionDetalleModel
                                {
                                    ID_CONFIGURAR_DETALLE= d.ID_CONFIGURAR_DETALLE,
                                    ID_CONFIGURACION = d.ID_CONFIGURACION,
                                    CONFIGURACION_DESCRIPCION= e.DESCRIPCION,
                                    DESCRIPCION= d.DESCRIPCION,
                                    FLG_ESTADO = d.FLG_ESTADO
                                });
              
            }
            catch (Exception ex)
            {
                Log.CreateLogger(ex.Message);
                listaDetalle = new List<ConfiguracionDetalleModel>();
            }

            return listaDetalle;
        }
    }
}
