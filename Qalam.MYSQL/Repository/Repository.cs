using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Qalam.Common.Helper;
using Qalam.Common.Extensions;
using Qalam.Common.Enums;
using Microsoft.EntityFrameworkCore.Internal;
//using MySql.Data.MySqlClient;
using Qalam.Common.Exceptions;

namespace Qalam.MYSQL.Repository
{
    public class Repository<TEntity> : IDisposable where TEntity : class, new()
    {
        private readonly DbContext _context;
        private bool _disposed;
        private readonly DbSet<TEntity> _dbSet;
        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> GetAllQuerable()
        {
            try
            {
                return _dbSet.AsNoTracking();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual TEntity Get(Expression<Func<TEntity, bool>> predicate, params string [] includes)
        {
            try
            {
                var data = _dbSet.Where(predicate);

                if(includes.Length > 0)
                {
                    data = data.IncludeMultiple(includes);
                }

                return data.AsNoTracking().SingleOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual TResult Get<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector, params string[] includes) where TResult : class
        {
            try
            {
                var data = _dbSet.Where(predicate);

                if (includes.Length > 0)
                {
                    data = data.IncludeMultiple(includes);
                }
                return data.AsNoTracking().Select(selector).SingleOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual List<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, params string [] includes)
        {
            try
            {
                var data = _dbSet.Where(predicate);
                
                if(includes.Length > 0)
                {
                    data = data.IncludeMultiple(includes);
                }

                return data.AsNoTracking().ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual List<TResult> GetAll<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector, params string[] includes) where TResult : class
        {
            try
            {
                var data = _dbSet.Where(predicate);
                if (includes.Length > 0)
                {
                    data = data.IncludeMultiple(includes);
                }
                return data.AsNoTracking().Select(selector).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public PageResult<TEntity> GetAllPaginated(Expression<Func<TEntity, bool>> predicate, int pageSize, int pageNo, params string[] includes)
        {
            try
            {
                pageNo = Math.Max(1, pageNo);
                pageSize = pageSize <= 0 ? 10 : pageSize;

                int count = _dbSet.Count(predicate);
                int skipCount = Math.Min((pageNo - 1) * pageSize, count);
                int takeCount = Math.Min(pageSize, count - Math.Min(skipCount, count));
                var data = _dbSet.Where(predicate);

                if (includes.Length > 0)
                {
                    data = data.IncludeMultiple(includes);
                }

                return new PageResult<TEntity>()
                {
                    Count = count,
                    PageSize = takeCount,
                    PageNo = pageNo,
                    PageData = data.AsNoTracking().Skip(skipCount).Take(takeCount).ToList()
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual PageResult<TResult> GetAllPaginated<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector, int pageSize, int pageNo, params string[] includes) where TResult : class
        {
            try
            {
                pageNo = Math.Max(1, pageNo);
                pageSize = pageSize <= 0 ? 10 : pageSize;

                int count = _dbSet.Count(predicate);
                int skipCount = Math.Min((pageNo - 1) * pageSize, count);
                int takeCount = Math.Min(pageSize, count - Math.Min(skipCount, count));
                var data = _dbSet.Where(predicate);

                if (includes.Length > 0)
                {
                    data = data.IncludeMultiple(includes);
                }

                return new PageResult<TResult>()
                {
                    Count = count,
                    PageSize = takeCount,
                    PageNo = pageNo,
                    PageData = data.AsNoTracking().Skip(skipCount).Take(takeCount).Select(selector).ToList()
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int Count()
        {
            try
            {
                return _dbSet.AsNoTracking().Count();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return _dbSet.AsNoTracking().Count(predicate);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual TEntity Add(TEntity entity)
        {
            try
            {
                return _dbSet.Add(entity).Entity;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual List<TEntity> Add(List<TEntity> entities)
        {
            List<TEntity> data = new List<TEntity>();
            try
            {
                foreach (var entity in entities)
                {
                    data.Add(_dbSet.Add(entity).Entity);
                }
                return data;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual void Update(TEntity entity)
        {
            try
            {
                _dbSet.Update(entity);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual void Update(List<TEntity> entities)
        {
            try
            {
                foreach (var entity in entities)
                {
                    _dbSet.Update(entity);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual void Update(TEntity entity, params Expression<Func<TEntity, object>>[] properties)
        {
            try
            {
                var entry = _dbSet.Attach(entity);

                foreach (var property in properties)
                {
                    entry.Property(property).IsModified = true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual void Delete(params int[] ids)
        {
            try
            {
                if (typeof(TEntity).GetProperty("Id") == null)
                {
                    throw new Exception("Entity Should have Property Id so you can't use this method with this entity");
                }
                var entities = (List<TEntity>)Activator.CreateInstance(typeof(List<TEntity>));
                foreach (var id in ids)
                {
                    var data = new TEntity();
                    data.GetType().GetProperty("Id").SetValue(data, id);
                    entities.Add(data);
                }

                _dbSet.RemoveRange(entities);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual void Delete(List<TEntity> entities)
        {
            try
            {
                _dbSet.RemoveRange(entities);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool SaveChanges()
        {
            //try
            //{
            //    return _context.SaveChanges() > 0;
            //}
            //catch(Exception e)
            //{
            //    if (e.InnerException is MySqlException innerException && (innerException.Number == 1169 || innerException.Number == 1062))
            //    {
            //        throw new DuplicateDataException(innerException.Message);
            //    }

            //    throw e;
            //}
            return true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
