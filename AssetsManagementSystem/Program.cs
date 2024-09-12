using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace AssetsManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            #region Add services to the container

            builder.Services.AddCors();

            builder.Services.AddDataProtection();

            builder.Services.AddOthersServices(builder.Configuration);

            builder.Services.AddDataLayer(builder.Configuration);

            builder.Services.AddFolderOfServices();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
           
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();


            builder.Services.AddControllers()
               .AddJsonOptions(config =>
                   config.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            #endregion
             
            #region Swagger Configuration

            builder.Services.AddSwaggerGen(c =>
            {
                c.UseInlineDefinitionsForEnums();
                c.SchemaFilter<EnumSchemaFilter>();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Asset Management System API", Version = "v1", Description = "AMS API swagger client." });

                // Adding Bearer token authentication
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "'You can type 'Bearer' and enter the token after leaving a space \r\n\r\n For example: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\""
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

            });

            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
 
            app.MapControllers();

            app.ExceptionHandleConfiguration();

            app.Run();
        }
    }
}
