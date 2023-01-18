namespace ASP.NET_Core.Useful.Techniques.DataLayer.Implementations
{
    using ASP.NET_Core.Useful.Techniques.DataLayer.Contracts;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Linq;
    using System.Threading.Tasks;
    using ASP.NET_Core.Useful.Techniques.Models.Contracts;

    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly DbContext context;
        private readonly DbSet<T> dbSet;

        public Repository(DataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public IQueryable<T> All(params Expression<Func<T, object>>[] includeExpressions)
        {
            return this.All(false, includeExpressions);
        }

        public IQueryable<T> All(bool includeInactive, params Expression<Func<T, object>>[] includeExpressions)
        {
            IQueryable<T> set = this.dbSet;

            foreach (var includeExpression in includeExpressions)
            {
                set = set.Include(includeExpression);
            }

            return set;
        }

        public async Task<T> GetByIdAsync(object id)
        {
            var entity = await this.dbSet.FindAsync(id);

            return entity;
        }

        public T Add(T entity)
        {
            return this.dbSet.Add(entity).Entity;
        }

        public void AddRange(IEnumerable<T> entities)
        {
            this.dbSet.AddRangeAsync(entities);
        }

        public bool Exists(T entity)
            => this.dbSet.Contains(entity);

        public void Delete(T entity)
        {
            this.dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                ChangeState(entity, EntityState.Deleted);
            }
        }

        public async Task<T> FindOrCreate(object id)
        {
            var entity = await this.GetByIdAsync(id);

            return entity != null ? entity : new T();
        }

        public void ChangeState(T entity, EntityState state)
        {
            var entry = this.context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                this.dbSet.Attach(entity);
            }

            entry.State = state;
        }
    }
}
