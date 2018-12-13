using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Repositories.Interfaces
{
    /// <summary>
    /// This interface provides the functions for CRUD operations on an entity
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IService<T>
    {
        //The Get method is overloaded so that the caller can pass custom queries.
        List<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, 
            IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");

        T GetById(int id);

        void Create(T entity);

        void Update(T entity);

        void Delete(int id);
    }
}
