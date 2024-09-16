using Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ContextDb context;
        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();
        private bool disposed = false;

        public UnitOfWork()
        {
            this.context = new ContextDb();
        }

        public IGenericRepository<T> GenericRepository<T>() where T : class
        {
            try
            {
                if (repositories.Keys.Contains(typeof(T)))
                {
                    return repositories[typeof(T)] as IGenericRepository<T>;
                }

                IGenericRepository<T> repo = new GenericRepository<T>(context);
                repositories.Add(typeof(T), repo);
                return repo;
            }
            catch (Exception e)
            {
                throw;
            }
            
        }

        public bool Save()
        {
            bool returnValue = true;
            using (var dbContextTransaction = this.context.Database.BeginTransaction())
            {
                try
                {
                    this.context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception e)
                {
                    returnValue = false;
                    dbContextTransaction.Rollback();
                    throw;
                }
            }
            return returnValue;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                this.context.Dispose();
            }
            this.disposed = true;
        }
    }
}
