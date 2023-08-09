using Gestion_Rendimiento_Common;
using Gestion_Rendimiento_Data;
using Gestion_Rendimiento_Entity;
using Gestion_Rendimiento_IService;
using Gestion_Rendimiento_Repository;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_Service
{
    public class ProyectoDetalleService : Repository<Proyecto_Detalle>, IProyectoDetalleService
    {
        protected BaseResponse _baseResponse;
        public ProyectoDetalleService(DatabaseContext context) : base(context)
        {
        }
        public Proyecto_Detalle Insertar(Proyecto_Detalle item)
=> Add(item);
        public Proyecto_Detalle Actualizar(Proyecto_Detalle item)
=> Update(item, item.ID_DETALLE_PROYECTO);
        public IEnumerable<Proyecto_Detalle> GetProyectoXId(int id)
       => FindAll(w => w.ID_PROYECTO == id && w.FLG_ESTADO=="1");
        public BaseResponse DeleteDetalle(Proyecto_Detalle item)
        {
            _baseResponse = new BaseResponse();
            _baseResponse.Success = false;
            var transaction = _context.Database.BeginTransaction();
            transaction.CreateSavepoint("BeforeTransaccion");
            try
            {
                var model = new Proyecto_Detalle
                {
                    ID_DETALLE_PROYECTO = item.ID_DETALLE_PROYECTO,
                    FLG_ESTADO = "0",
                    USUARIO_MODIFICACION = item.USUARIO_MODIFICACION,
                    FECHA_MODIFICACION = item.FECHA_MODIFICACION,
                    IP_MODIFICACION = item.IP_MODIFICACION,
                };
                _context.Attach(model);
                _context.Entry(model).Property(x => x.ID_DETALLE_PROYECTO).IsModified = false;
                _context.Entry(model).Property(x => x.FLG_ESTADO).IsModified = true;
                _context.Entry(model).Property(x => x.USUARIO_MODIFICACION).IsModified = true;
                _context.Entry(model).Property(x => x.FECHA_MODIFICACION).IsModified = true;
                _context.Entry(model).Property(x => x.IP_MODIFICACION).IsModified = true;
                var code = _context.SaveChanges();

                if (code < 1)
                {
                    throw new Exception("Ocurrió un error al eliminar el registro ");
                }
                transaction.Commit();
                _baseResponse.ID = item.ID_DETALLE_PROYECTO;
                _baseResponse.Code = (int)EstadoProcesoEnum.Success;
                _baseResponse.Success = true;
                _baseResponse.Message = "Se ha procesado correctamente";
            }
            catch (Exception ex)
            {
                transaction.RollbackToSavepoint("BeforeTransaccion");
                Log.CreateLogger(ex.Message);
                _baseResponse.Code = (int)EstadoProcesoEnum.Failed;
                _baseResponse.Success = false;
                _baseResponse.Message = ex.Message;
            }
            return _baseResponse;

        }
    }
}
