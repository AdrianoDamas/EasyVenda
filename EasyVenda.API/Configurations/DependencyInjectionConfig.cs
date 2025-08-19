using _EasyVenda.Application.Business;
using _EasyVenda.Core.Business;
using _EasyVenda.Core.Repositories._EasyVenda;
using _EasyVenda.Infrastructure.MessageBus;
using _EasyVenda.Infrastructure.Persistence.Repositories._EasyVenda;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace _EasyVenda.API.Configurations
{
    public class DependencyInjectionConfig
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            #region Configure Services

            //DbContexts
            services.AddDbContext<Infrastructure.Persistence.Context._EasyVendaDbContext > (options =>
                options.UseSqlServer(configuration.GetConnectionString("EasyVenda")));

            // Authorization
            services.AddAuthorization();

            //Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            });

            services.AddControllers();

            //Api Endpoints Configuration
            services.AddEndpointsApiExplorer();

            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "EasyVenda API",
                    Description = "Developed by Adriano Damas - Owner @ Adriano Damas",
                    Contact = new OpenApiContact { Name = "Adriano Damas", Email = "contato@adrianodamas.com.br" },
                    License = new OpenApiLicense { Name = "Adriano Damas", Url = new Uri("https://adrianodamas.com.br/") }
                });
            });

            //Services
            //Repositories - EasyVenda (SQLSERVER)
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            //Business - EasyVenda
            services.AddScoped<IVendaBusiness, VendaBusiness>();

            //MessageBus
            services.AddScoped<IMessageBus, MessageBus>();

            //Configs
            services.AddAutoMapper(typeof(AutoMapperConfig).Assembly);
            #endregion
        }
    }
}
