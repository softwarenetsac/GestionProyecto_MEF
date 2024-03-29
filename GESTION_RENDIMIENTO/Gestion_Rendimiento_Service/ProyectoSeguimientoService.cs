﻿using Gestion_Rendimiento_Common;
using Gestion_Rendimiento_Data;
using Gestion_Rendimiento_Entity;
using Gestion_Rendimiento_Entity.Model;
using Gestion_Rendimiento_IService;
using Gestion_Rendimiento_Repository;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_Service
{
    public class ProyectoSeguimientoService : Repository<ProyectoSeguimiento>, IProyectoSeguimientoService
    {
  
        protected BaseResponse _baseResponse;
        public ProyectoSeguimientoService(DatabaseContext context) : base(context)
        {
          
        }
        public BaseResponse Insertar(ProyectoSeguimiento item)
        {

            BaseResponse _baseResponse = new BaseResponse();
            _baseResponse.Success = false;
            IDbContextTransaction transaction = null;
            try
            {
                transaction = _context.Database.BeginTransaction();
                transaction.CreateSavepoint("BeforeTransaccion");
     
                if (item.ID_SEGUIMIENTO <= 0)
                {
                    _context.Set<ProyectoSeguimiento>().Add(item);
                }
                else
                {
                    var existing = _context.Set<ProyectoSeguimiento>().Find(item.ID_SEGUIMIENTO);
                    if (existing != null)
                    {
                        _context.Entry(existing).CurrentValues.SetValues(item);
                    }
                }
                var code = _context.SaveChanges();
                if (code < 1)
                {
                    throw new Exception("Ocurrió un error al registrar horario");
                }

                transaction.Commit();
                _baseResponse.ID = item.ID_SEGUIMIENTO;
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
        public IEnumerable<ProyectoSeguimientoConsulta> GetAllSeguimientoProyecto(int ID_PROYECTO)
        {
            var lista = _context.ProyectoSeguimientoConsulta.ToList().Where(x => x.ID_PROYECTO == ID_PROYECTO && x.FLG_ESTADO == "1").ToList();
            return lista;
        }
        public BaseResponse DeleteSeguimiento(ProyectoSeguimiento item)
        {
            _baseResponse = new BaseResponse();
            _baseResponse.Success = false;
            IDbContextTransaction transaction = null;
            try
            {
                transaction = _context.Database.BeginTransaction();
                transaction.CreateSavepoint("BeforeTransaccion");
                var model = new ProyectoSeguimiento
                {
                    ID_SEGUIMIENTO = item.ID_SEGUIMIENTO,
                    FLG_ESTADO = item.FLG_ESTADO,
                    USUARIO_MODIFICACION = item.USUARIO_MODIFICACION,
                    FECHA_MODIFICACION = item.FECHA_MODIFICACION,
                    IP_MODIFICACION = item.IP_MODIFICACION,
                };
                _context.Attach(model);
                _context.Entry(model).Property(x => x.ID_SEGUIMIENTO).IsModified = false;
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
