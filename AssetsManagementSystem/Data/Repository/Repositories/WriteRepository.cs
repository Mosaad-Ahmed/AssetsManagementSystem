using AssetsManagementSystem.Others.Interfaces.IRepositories;

namespace AssetsManagementSystem.Data.Repository.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : class, IBaseEntityForGeneric, new()
    {
        private readonly ApplicationDbContext dbContext;

        public WriteRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private DbSet<T> table => dbContext.Set<T>();
        public async Task AddAsync(T entity)
        { 
 
            await table.AddAsync(entity);
           
        }

        public async Task AddRangeAsync(IList<T> entities)
        {
            await table.AddRangeAsync(entities);
        }
        public async Task<T> UpdateAsync(int id, T entity)
        {
            await Task.Run(() => table.Update(entity));
            dbContext.Entry(entity).State = EntityState.Modified;

            return entity;
        }
        public async Task DeleteAsync(T entity)
        {
            await Task.Run(() => table.Remove(entity));
        }

        public async Task DeleteRangeAsync(IList<T> Entities)
        {
            await Task.Run(() => table.RemoveRange(Entities));
        }
    }
}
