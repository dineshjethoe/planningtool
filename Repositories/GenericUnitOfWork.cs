using System;

namespace Repositories
{
    /// <summary>
    /// This class expects an Entity type to get its repository so that CRUD operations can be applied on.
    /// The Unit of Work pattern is used.
    /// </summary>
    /// <typeparam name="T">This can be of any entity type from Entities lib.</typeparam>
    public class UnitOfWork<T> : IDisposable where T : class
    {
        private KlmPlanningToolDbContext context = new KlmPlanningToolDbContext();

        private GenericRepository<T> _repository;

        public GenericRepository<T> Repository
        {
            get
            {
                return this._repository ?? new GenericRepository<T>(context);
            }
        }

        public void Save()
        {
            context.SaveChanges();
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
    }
}
