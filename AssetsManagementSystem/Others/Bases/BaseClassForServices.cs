namespace AssetsManagementSystem.Others.Bases

{
    public class BaseClassForServices
    {
        public IUnitOfWork UnitOfWork { get; }
        public Others.Interfaces.IAutoMapper.IMapper Mapper { get; }
        public IHttpContextAccessor HttpContextAccessor { get; }
        public string UserId { get; set; }

        public BaseClassForServices(IUnitOfWork unitOfWork,
            Others.Interfaces.IAutoMapper.IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
            HttpContextAccessor = httpContextAccessor;
            UserId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }


    }
}
