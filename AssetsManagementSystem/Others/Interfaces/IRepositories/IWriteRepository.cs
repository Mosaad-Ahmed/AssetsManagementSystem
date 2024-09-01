namespace AssetsManagementSystem.Others.Interfaces.IRepositories
{
    public interface IWriteRepository<T> where T : class, IBaseEntityForGeneric, new()
    {
        Task AddAsync(T entity);
        Task AddRangeAsync(IList<T> entities);
        Task<T> UpdateAsync(int id, T entity);
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(IList<T> Entities);


    }
}
