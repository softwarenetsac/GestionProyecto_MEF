using Gestion_Rendimiento_Common;
using Gestion_Rendimiento_Data;
using Gestion_Rendimiento_Entity;
using Gestion_Rendimiento_Entity.Model;
using Gestion_Rendimiento_IService;
using Gestion_Rendimiento_Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_Service
{
    public class ProyectoService:Repository<Proyecto>, IProyectoService
    {
        protected readonly DatabaseContext _context;
        protected BaseResponse _baseResponse;
        public ProyectoService(DatabaseContext context, IConfiguration configurationt) : base(context)
        {
            _context = context;
        }
        public Proyecto Insertar(Proyecto item)
     => Add(item);
        public Proyecto Actualizar(Proyecto item)
    => Update(item, item.ID_PROYECTO);
        public string Proyecto_Min_Ano()
        {
            var detalle = _context.Set<Proyecto>().Min(x=> x.ANIO);
            return detalle;
        }
        public BaseResponse ActualizarProyecto(Proyecto item)
        {
            _baseResponse = new BaseResponse();
            _baseResponse.Success = false;

            var transaction = _context.Database.BeginTransaction();
            transaction.CreateSavepoint("BeforeTransaccion");
            try
            {
                var model = new Proyecto
                {
                    ID_PERSONAL = item.ID_PERSONAL,
                    ID_OFICINA = item.ID_OFICINA,
                    ID_PROYECTO = item.ID_PROYECTO,
                    USUARIO_MODIFICACION = item.USUARIO_MODIFICACION,
                    FECHA_MODIFICACION = item.FECHA_MODIFICACION,
                };
                _context.Attach(model);
                _context.Entry(model).Property(x => x.ID_PROYECTO).IsModified = false;
                _context.Entry(model).Property(x => x.ID_PERSONAL).IsModified = true;
                _context.Entry(model).Property(x => x.ID_OFICINA).IsModified = true;
                _context.Entry(model).Property(x => x.FLG_ESTADO).IsModified = true;
                _context.Entry(model).Property(x => x.USUARIO_MODIFICACION).IsModified = true;
                _context.Entry(model).Property(x => x.FECHA_MODIFICACION).IsModified = true;
                var code = _context.SaveChanges();

                if (code < 1)
                {
                    throw new Exception("Ocurrió un error al actualizar ");
                }
                transaction.Commit();
                _baseResponse.ID = item.ID_PROYECTO;
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
        public BaseResponse DeleteProyecto(Proyecto item)
        {
            _baseResponse = new BaseResponse();
            _baseResponse.Success = false;

            var transaction = _context.Database.BeginTransaction();
            transaction.CreateSavepoint("BeforeTransaccion");
            try
            {
                var model = new Proyecto
                {
                    ID_PROYECTO = item.ID_PROYECTO,
                    USUARIO_MODIFICACION = item.USUARIO_MODIFICACION,
                    FECHA_MODIFICACION = item.FECHA_MODIFICACION,
                };
                _context.Attach(model);
                _context.Entry(model).Property(x => x.ID_PROYECTO).IsModified = false;
                _context.Entry(model).Property(x => x.FLG_ESTADO).IsModified = true;
                _context.Entry(model).Property(x => x.USUARIO_MODIFICACION).IsModified = true;
                _context.Entry(model).Property(x => x.FECHA_MODIFICACION).IsModified = true;
                var code = _context.SaveChanges();

                if (code < 1)
                {
                    throw new Exception("Ocurrió un error al eliminar registro ");
                }
                transaction.Commit();
                _baseResponse.ID = item.ID_PROYECTO;
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
        public BaseResponse ActualizarEstadoProyecto(Proyecto item)
        {
            _baseResponse = new BaseResponse();
            _baseResponse.Success = false;

            var transaction = _context.Database.BeginTransaction();
            transaction.CreateSavepoint("BeforeTransaccion");
            try
            {
                var model = new Proyecto
                {
                    ID_PROYECTO = item.ID_PROYECTO,
                    ID_ESTADO = item.ID_ESTADO,
                    USUARIO_MODIFICACION = item.USUARIO_MODIFICACION,
                    FECHA_MODIFICACION = item.FECHA_MODIFICACION,
                };
                _context.Attach(model);
                _context.Entry(model).Property(x => x.ID_PROYECTO).IsModified = false;
                _context.Entry(model).Property(x => x.ID_ESTADO).IsModified = true;
                _context.Entry(model).Property(x => x.USUARIO_MODIFICACION).IsModified = true;
                _context.Entry(model).Property(x => x.FECHA_MODIFICACION).IsModified = true;
                var code = _context.SaveChanges();

                if (code < 1)
                {
                    throw new Exception("Ocurrió un error al eliminar registro ");
                }
                transaction.Commit();
                _baseResponse.ID = item.ID_PROYECTO;
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
