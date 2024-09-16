using System;

namespace Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> GenericRepository<TEntity>() where TEntity : class;
        bool Save();
    }
}
