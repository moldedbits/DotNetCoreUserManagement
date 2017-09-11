using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity.Infrastructure;
using EntityFramework.DynamicFilters;
using UserAppService.Context;

namespace UserAppService.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _dbContext;
        protected readonly IDbSet<T> _dbSet;

        public Repository(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public void Delete(T entity)
        {
            _dbContext.Database.Log = message => Trace.Write(message);
            _dbContext.Entry(entity).State = EntityState.Deleted;
            //_dbSet.Remove(entity);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            _dbContext.Database.Log = message => Trace.Write(message);
            return _dbSet.FirstOrDefault(predicate);
        }

        //public T FirstOrDefault(int id)
        //{
        //    _dbContext.Database.Log = message => Trace.Write(message);
        //    return _dbSet.FirstOrDefault(x => x.Id == id);
        //}

        public T Get(int id)
        {
            _dbContext.Database.Log = message => Trace.Write(message);
            return _dbSet.Find(id);
        }

        public IQueryable<T> GetAllQueryable()
        {
            _dbContext.Database.Log = message => Trace.Write(message);
            return _dbSet.AsQueryable();
        }

        public IQueryable<T> GetAllQueryable(Expression<Func<T, bool>> predicate)
        {
            _dbContext.Database.Log = message => Trace.Write(message);
            return _dbSet.Where(predicate).AsQueryable<T>();
        }

        public IEnumerable<T> GetAll()
        {
            _dbContext.Database.Log = message => Trace.Write(message);
            return _dbSet.AsEnumerable<T>();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            //_dbContext.Database.Log = message => Trace.Write(message);
            return _dbSet.Where(predicate).AsEnumerable<T>();
        }

        public T Insert(T entity)
        {
            //_dbContext.Database.Log = message => Trace.Write(message);
            return _dbSet.Add(entity);
        }

        //public int InsertAndGetId(T entity)
        //{
        //    T added = Insert(entity);
        //    return added.Id;
        //}

        public T InsertOrUpdate(T entity)
        {
            //_dbContext.Database.Log = message => Trace.Write(message);
            _dbSet.AddOrUpdate(entity);
            return _dbSet.Find(entity);
        }

        //public int InsertOrUpdateAndGetId(T entity)
        //{
        //    var addOrUpdate = InsertOrUpdate(entity);
        //    return addOrUpdate.Id;
        //}

        public T Single(Expression<Func<T, bool>> predicate)
        {
            //_dbContext.Database.Log = message => Trace.Write(message);
            return _dbSet.Single(predicate);
        }

        public T SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            //_dbContext.Database.Log = message => Trace.Write(message);
            return _dbSet.Where(predicate).SingleOrDefault();
        }

        //public T Update(T entity)
        //{
        //    //_dbContext.Database.Log = message => Trace.Write(message);
        //    _dbContext.Entry(entity).State = EntityState.Modified;
        //    return _dbSet.Find(entity.Id);
        //}

        public int Count()
        {
            //_dbContext.Database.Log = message => Trace.Write(message);
            return _dbSet.Count();
        }

        public virtual void Commit()
        {
            //_dbContext.Database.Log = message => Trace.Write(message);
            var result = _dbContext.SaveChanges();
        }

        /// <summary>
        /// Commit created for importing tool/Api end points only.
        /// </summary>
        /// <param name="importingType">Type of entity being imported.</param>
        //public virtual void ImportingCommit(Type importingType)
        //{
        //    var result = _dbContext.ImportingSaveChanges(importingType);
        //}

        public T GetActive(int id)
        {
            _dbContext.EnableFilter("SoftDeleteFilter");
            var entity = _dbSet.Find(id);
            _dbContext.DisableFilter("SoftDeleteFilter");

            _dbContext.Database.Log = message => Trace.Write(message);

            return entity;
        }

        /// <summary>
        /// Sets System user to DbContext for background and secondary thread.
        /// <remarks>Note: This should be used only by Background or secondary thread. Main thread will get user information from Http context.</remarks>  
        /// </summary>
        public void SetSystemUserToContext()
        {
            _dbContext.UserId = -1;
        }

        public void RefreshEntity(T entity)
        {
            var context = ((IObjectContextAdapter)_dbContext).ObjectContext;
            context.Refresh(RefreshMode.StoreWins, entity);
        }
    }
}
