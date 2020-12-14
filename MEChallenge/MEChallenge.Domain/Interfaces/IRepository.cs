using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MEChallenge.Domain.Interfaces
{
    /// <summary>
    /// Generic Repository Interface
    /// </summary>
    /// <typeparam name="T">Model</typeparam>
    /// <typeparam name="U">Index type</typeparam>
    public interface IRepository<T, U>
    {
        Task<T> GetById(U id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> filter);
        Task Save(T entity);
        Task Delete(U id);
    }
}
