using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace UserAppService
{
    public interface IRepository<T> where T : class
    {
        #region Getting system user for background threads.
        /// <summary>
        /// Sets System user to DbContext for background and secondary thread.
        /// <remarks>Note: This should be used only by Background or secondary thread. Main thread will get user information from Http context.</remarks>  
        /// </summary>
        void SetSystemUserToContext();

        #endregion

        #region Query/Get/Select

        IQueryable<T> GetAllQueryable();

        IQueryable<T> GetAllQueryable(Expression<Func<T, bool>> predicate);

        IEnumerable<T> GetAll();

        IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate);

        T Get(int id);

        T GetActive(int id);

        T Single(Expression<Func<T, bool>> predicate);

        T SingleOrDefault(Expression<Func<T, bool>> predicate);

        //T FirstOrDefault(int id);

        T FirstOrDefault(Expression<Func<T, bool>> predicate);

        #endregion

        #region Insert

        T Insert(T entity);

        //int InsertAndGetId(T entity);

        T InsertOrUpdate(T entity);

        //int InsertOrUpdateAndGetId(T entity);

        #endregion

        #region Update

        //T Update(T entity);

        #endregion

        #region Delete

        void Delete(T entity);

        #endregion

        #region Total Count
        int Count();
        #endregion

        void Commit();

        /// <summary>
        /// Commit created for importing tool/Api end points only.
        /// </summary>
        /// <param name="importingType">Type of entity being imported.</param>
        //void ImportingCommit(Type importingType);

        /// <summary>
        /// It will reload entity from database
        /// </summary>
        /// <param name="entity"></param>
        void RefreshEntity(T entity);
    }
}
