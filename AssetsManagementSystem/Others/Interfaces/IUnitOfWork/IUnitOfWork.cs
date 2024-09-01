using AssetsManagementSystem.Others.Interfaces.IRepositories;

namespace AssetsManagementSystem.Others.Interfaces.IUnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IReadRepository<T> readRepository<T>() where T : class, IBaseEntityForGeneric, new();
        public IWriteRepository<T> writeRepository<T>() where T : class, IBaseEntityForGeneric, new();

        public Task<int> SaveChangeAsync();
        public int SaveChange();

        public   Task RollbackTransactionAsync();
        public Task CommitTransactionAsync();
        public Task BeginTransactionAsync();
    }
}
