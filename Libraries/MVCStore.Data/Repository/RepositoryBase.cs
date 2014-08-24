using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Data;
using MVCStore.Core.Data;
using MVCStore.Data.Context;
using MVCStore.Data.Entities;

namespace MVCStore.Data.Repository
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        private AppEntities dataContext;
        private readonly IDbSet<T> dbSet;

        protected RepositoryBase(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            dbSet = DataContext.Set<T>();
        }
        
        protected IDatabaseFactory DatabaseFactory
        {
            get;
            private set;
        }

        protected AppEntities DataContext
        {
            get { return dataContext ?? (DatabaseFactory.Get()); }
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        /// <summary>
        /// Adds a new entity instance to the database on behalf of the parent type.
        /// </summary>
        /// <param name="entity">Any valid database object</param>
        public virtual T Add(T entity)
        {
            var newEntry = dbSet.Add(entity);
            dataContext.SaveChanges();
            return newEntry;
        }

        /// <summary>
        /// Deletes an existing instance of an entity from the database on behalf of the parent type.
        /// </summary>
        /// <param name="entity">Any valid database object</param>
        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
            dataContext.Commit();
        }

        /// <summary>
        /// Returns a specific instance of an entity from the database on behalf of the parent type.
        /// </summary>
        /// <param name="Id">The integer value of the entity's primary key</param>
        /// <returns>A database object (of type T)</returns>
        public virtual T GetById(long id)
        {
            return dbSet.Find(id);
        }

        /// <summary>
        /// Updates an existing entity instance in the database on behalf of the parent type.
        /// </summary>
        /// <param name="Entity">Any valid database object</param>
        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
            dataContext.Commit();
        }

        public virtual T GetById(string id)
        {
            return dbSet.Find(id);
        }

        public virtual T Get(Expression<Func<T, bool>> where)
        {
            return dbSet.FirstOrDefault(where);
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(int i)
        {
            throw new NotImplementedException();
        }
    }
}