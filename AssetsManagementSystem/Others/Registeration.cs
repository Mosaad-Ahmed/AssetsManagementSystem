using AssetsManagementSystem.Others.Storage;
using System.Text;

namespace AssetsManagementSystem.Others
{
    public static class Registeration
    {
        public static void AddOthersServices(this IServiceCollection services, IConfiguration configuration)
        {
           
            services.AddTransient<ExceptionMiddleware>();
            services.Configure<TokenSettings>(configuration.GetSection("JWT"));
            services.AddTransient<ITokenService, TokenService>();
            services.AddSingleton<Interfaces.IAutoMapper.IMapper, AutoMapper.Mapper>();
             services.AddSingleton<IFileService, FileService>();

            #region Authenticationس
            services.AddAuthentication
                (
                opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
                ).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
                opt =>
                {
                    opt.SaveToken = true;
                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"])),
                        ValidIssuer = configuration["JWT:Issuer"],
                        ValidAudience = configuration["JWT:Audience"],
                        ClockSkew = TimeSpan.Zero
                    };
                }
                );
            #endregion
        }
    }
}
