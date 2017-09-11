//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;

//namespace UserAppService.EntityFramework.InMemoryRepository
//{
//    /// <summary>
//    /// Predicate supplied to any repository methods have business logic, we need to cover those business logic in unit test.
//    /// Intention of this class to tackle business logic in predicates.
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    public class InMemoryRepository<T> : IDisposable, IRepository<T> where T : class
//    {
//        private readonly List<T> _resourceContent;
//        private int _id;

//        public InMemoryRepository()
//        {
//            _resourceContent = new List<T>();
//        }

//        public void SetSystemUserToContext()
//        {
//            // This method will be called for background process.
//            throw new NotImplementedException();
//        }

//        public IQueryable<T> GetAllQueryable()
//        {
//            return _resourceContent.AsQueryable();
//        }

//        public IQueryable<T> GetAllQueryable(Expression<Func<T, bool>> predicate)
//        {
//            return GetAllQueryable().Where(predicate).AsQueryable();
//        }

//        public IEnumerable<T> GetAll()
//        {
//            return _resourceContent.AsEnumerable();
//        }

//        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate)
//        {
//            return GetAllQueryable().Where(predicate);
//        }

//        //public T Get(int id)
//        //{
//        //    return _resourceContent.Find(x => x.Id == id);
//        //}

//        public T GetActive(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public T Single(Expression<Func<T, bool>> predicate)
//        {
//            return GetAllQueryable().Single(predicate);
//        }

//        public T SingleOrDefault(Expression<Func<T, bool>> predicate)
//        {
//            return GetAllQueryable().SingleOrDefault(predicate);
//        }

//        //public T FirstOrDefault(int id)
//        //{
//        //    return _resourceContent.FirstOrDefault(x => x.Id == id);
//        //}

//        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
//        {
//            return GetAllQueryable().FirstOrDefault(predicate);
//        }

//        /// <summary>
//        /// If you provide -1 (negative value) for Entity.Id, then we will set it to 0.
//        /// If Id > 0, then we will not modify Entity.Id.
//        /// This way we can test the possibility of ID having zero value.
//        /// </summary>
//        /// <param name="entity"></param>
//        /// <returns></returns>
//        public T Insert(T entity)
//        {
//            if (entity == null) return null;

//            if (entity.Id < 0)
//                entity.Id = 0;
//            else if (entity.Id == 0)
//            {
//                _id = ++_id;
//                entity.Id = _id;
//            }

//            _resourceContent.Add(entity);
//            return entity;
//        }

//        public int InsertAndGetId(T entity)
//        {
//            var insertedEntity = Insert(entity);
//            return insertedEntity.Id;
//        }

//        public T InsertOrUpdate(T entity)
//        {
//            return entity.Id == 0 ? Insert(entity) : Update(entity);
//        }

//        public int InsertOrUpdateAndGetId(T entity)
//        {
//            return InsertOrUpdate(entity).Id;
//        }

//        public T Update(T entity)
//        {
//            var oldEntity = _resourceContent.First(x => x.Id == entity.Id);
//            _resourceContent.Remove(oldEntity);
//            _resourceContent.Add(entity);
//            return entity;
//        }

//        public void Delete(T entity)
//        {
//            var oldEntity = _resourceContent.First(x => x.Id == entity.Id);
//            _resourceContent.Remove(oldEntity);
//        }

//        public int Count()
//        {
//            return _resourceContent.Count;
//        }

//        public void Commit()
//        {
//            // Do nothing.
//            _resourceContent.Where(x => x.Id == 0).ToList().ForEach(x => x.Id++);
//        }

//        //public void ImportingCommit(Type importingType)
//        //{
//        //    //This method used for Raven Syns, not required for InMemory.
//        //    throw new NotImplementedException();
//        //}

//        public void Dispose()
//        {
//            _resourceContent.Clear();
//        }

//        public void RefreshEntity(T entity)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}