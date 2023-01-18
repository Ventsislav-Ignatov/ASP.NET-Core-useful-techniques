namespace ASP.NET_Core.Useful.Techniques.DataLayer.Implementations
{
    using ASP.NET_Core.Useful.Techniques.DataLayer.Contracts;
    using ASP.NET_Core.Useful.Techniques.Models.Contracts;
    using ASP.NET_Core.Useful.Techniques.Models.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private DataContext context = new DataContext();
        private Repository<Author> authorRepository;

        public Repository<Author> AuthorRepository
        {
            get
            {

                if (this.authorRepository == null)
                {
                    this.authorRepository = new Repository<Author>(context);
                }
                return authorRepository;
            }
        }

        public async Task SaveAsync()
        {
            await this.ManageInterfaceEntities();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        private async Task ManageInterfaceEntities()
        {
            var auditable = context.ChangeTracker.Entries()
                .Where(e => e.Entity is IAuditable)
                .ToList();

            var addedEntities = auditable
                .Where(e => e.State == EntityState.Added)
                .Select(e => e.Entity)
                .ToList();

            foreach (IAuditable entity in addedEntities)
            {
                entity.CreatedDate = DateTime.UtcNow;
                entity.CreatedBy = "";
            }

            var modifiedEntities = auditable
                 .Where(e => e.State == EntityState.Modified)
                 .Select(e => e.Entity)
                 .ToList();

            foreach (IAuditable entity in modifiedEntities)
            {
                entity.ModifiedDate = DateTime.UtcNow;
                entity.ModifiedBy = "";
            }

            await context.SaveChangesAsync();
        }
    }
}
