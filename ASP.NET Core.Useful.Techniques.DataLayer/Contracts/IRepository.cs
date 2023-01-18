namespace ASP.NET_Core.Useful.Techniques.DataLayer.Contracts
{
    using ASP.NET_Core.Useful.Techniques.Models.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public interface IRepository<T> where T : class, new()
    {
        IQueryable<T> All(params Expression<Func<T, object>>[] includeExpressions);

        IQueryable<T> All(bool includeInactive, params Expression<Func<T, object>>[] includeExpressions);

        Task<T> GetByIdAsync(object id);

        T Add(T entity);

        void AddRange(IEnumerable<T> entities);

        bool Exists(T entity);

        void Delete(T entity);

        void DeleteRange(IEnumerable<T> entities);

        void ChangeState(T entity, EntityState state);

        Task<T> FindOrCreate(object id);
    }
}
