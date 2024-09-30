using Microsoft.Extensions.Options;

namespace AssetsManagementSystem.Data
{
    public static class Registeration
    {
        public static void AddDataLayer(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(
                opt =>
                {
                   // opt.UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                    opt.UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("DefaultConnection")/*
                                                                                                                    , sqlServerOptions =>
                  sqlServerOptions.EnableRetryOnFailure(
                      maxRetryCount: 5, // Number of retry attempts
                      maxRetryDelay: TimeSpan.FromSeconds(10), // Delay between retries
                      errorNumbersToAdd: null // Optional: SQL error numbers to trigger a retry
                  )*/ );
                }
          );
             
            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
 
            services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 2;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireDigit = false;
                opt.SignIn.RequireConfirmedEmail = false;
            })
                .AddRoles<Role>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();




        }
    }
}
