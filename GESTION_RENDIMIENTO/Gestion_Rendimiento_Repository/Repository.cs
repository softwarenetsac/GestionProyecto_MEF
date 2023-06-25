
using Gestion_Rendimiento_Common;
using Gestion_Rendimiento_Data;
using Gestion_Rendimiento_Entity.Model;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DatabaseContext _context;
        private DbSet<T> _entities = null;

        public Repository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll() =>
        _context.Set<T>().ToList();


        public List<T> GetAllList() =>
      _context.Set<T>().ToList();


        public async Task<List<T>> GetAllAsync()
        {
            var lista = await _context.Set<T>().ToListAsync();
            return lista;
        }





        public T Get(object key) =>
        _context.Set<T>().Find(key);

        public T Find(Expression<Func<T, bool>> match) =>
        _context.Set<T>().SingleOrDefault(match);

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> match) =>
        _context.Set<T>().Where(match).ToList();


        public List<T> FindList(Expression<Func<T, bool>> match) =>
     _context.Set<T>().Where(match).ToList();


        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            _context.SaveChanges();
            return entities;
        }

        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                //_entities.Attach(entity);
                //_context.Entry(entity).State = (EntityState)EntityState.Modified;
                _context.Update(entity);
                _context.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public T Update(T entity, object key)
        {
            if (entity == null)
                return null;

            T existing = _context.Set<T>().Find(key);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(entity);
                _context.SaveChanges();
            }
            return existing;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            _context.SaveChanges();
        }

        public int Count() =>
        _context.Set<T>().Count();

        public PagedResult<T> GetPaged(IQueryable<T> query, int page, int pageSize, int start)
        {
            var result = new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = query.Count()
            };


            /*
            var count  = (int)Math.Truncate(Convert.ToDouble(result.RowCount / result.PageSize));
            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);
            var skip = (page - 1) * pageSize;

            if (skip == count)
            {
                skip = 0;
            }
            if (result.PageCount <= 0)
            {
                result.PageCount = 1;
            }
            */

            var lista = query.Skip(start).Take(pageSize).ToList();

            var dd = lista.Count();
            result.Results = lista;
            return result;
        }

        public PagedResult<T> GetPaged(IQueryable<T> query, PaginationModel pagination)
        {
            string orderColumnName = string.Empty;
            bool isAscending = true;
            if (pagination.order != null)
            {
                if (pagination.order.Count > 0)
                {
                    orderColumnName = pagination.order[0].name;
                    isAscending = pagination.order[0].sortColum;
                }
            }
            var result = new PagedResult<T>
            {
                CurrentPage = pagination.draw,
                PageSize = pagination.length,
                RowCount = query.Count()
            };
            var data = Helper.OrderBy(query, orderColumnName, isAscending);
            var lista = data.Skip(pagination.start).Take(pagination.length).ToList();
            result.Results = lista;
            return result;
        }

        #region Properties

        /// <summary>
        /// Gets an entity set
        /// </summary>
        protected virtual DbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<T>();

                return _entities;
            }
        }

        #endregion
    }
}
