
using AssetsManagementSystem.DTOs.DataConsistencyCheckDTOs;

namespace AssetsManagementSystem.Services.DataConsistencyCheck
{
    public class DataConsistencyCheckService : BaseClassForServices
    {
        public DataConsistencyCheckService(IUnitOfWork unitOfWork, 
            Others.Interfaces.IAutoMapper.IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, mapper, httpContextAccessor)
        {
        }

        public async Task AddDataConsistencyCheckRecord(AddDataConsistencyCheckRequestDTO addDataConsistency) 
        {
            var DataConsistencyCheck=new Models.DbSets.DataConsistencyCheck() 
            {
                CheckDate=DateOnly.FromDateTime(DateTime.Now),
                PerformedById=Guid.Parse(UserId),
                Description=addDataConsistency.Description,
            };
            if (addDataConsistency.IssuesFound)
            {
                DataConsistencyCheck.IssuesFound = true;
               DataConsistencyCheck.Resolution = addDataConsistency.Resolution;
            }
            else
            {
                DataConsistencyCheck.IssuesFound= false;
                DataConsistencyCheck.Resolution=string.Empty;
            }
            await UnitOfWork.BeginTransactionAsync();
            try
            {
                await UnitOfWork.writeRepository<Models.DbSets.DataConsistencyCheck>()
                    .AddAsync(DataConsistencyCheck);
                await UnitOfWork.SaveChangeAsync();

                await UnitOfWork.CommitTransactionAsync();
            }
            catch
            {
                await UnitOfWork.RollbackTransactionAsync();
            }
             

            
        }









    }
}
