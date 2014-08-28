using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MVCStore.Core.Data
{
    public interface IRepository<T> where T : class
    {
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(int i);
        T GetById(long Id);
        T GetById(string Id);
        T Get(Expression<Func<T, bool>> where);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);
        byte[] GetImageById(int id);
    }
}