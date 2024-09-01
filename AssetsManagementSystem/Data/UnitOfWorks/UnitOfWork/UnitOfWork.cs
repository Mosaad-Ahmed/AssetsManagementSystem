using Microsoft.EntityFrameworkCore.Storage;

namespace AssetsManagementSystem.Data.UnitOfWorks.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext dbContext;
        private readonly AssetLifecycleInterceptor assetLifecycleInterceptor;
        private IDbContextTransaction contextTransaction;

        public UnitOfWork(ApplicationDbContext dbContext,
                          AssetLifecycleInterceptor assetLifecycleInterceptor)
        {
            this.dbContext = dbContext;
            this.assetLifecycleInterceptor = assetLifecycleInterceptor;

        }


        public async ValueTask DisposeAsync()=>await dbContext.DisposeAsync();


        public int SaveChange() => dbContext.SaveChanges();
        
        public async Task<int> SaveChangeAsync()=>await dbContext.SaveChangesAsync();


          IReadRepository<T> IUnitOfWork.readRepository<T>() => new ReadRepository<T>(dbContext);
         

          IWriteRepository<T> IUnitOfWork.writeRepository<T>()=>new WriteRepository<T>(dbContext);


        public async Task BeginTransactionAsync()
        {
            contextTransaction = await dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await dbContext.SaveChangesAsync();
                await contextTransaction.CommitAsync();
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                if (contextTransaction != null)
                {
                    await contextTransaction.DisposeAsync();
                    contextTransaction = null;
                }
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (contextTransaction != null)
            {
                await contextTransaction.RollbackAsync();
                await contextTransaction.DisposeAsync();
                contextTransaction = null;
            }
        }


    }
}
