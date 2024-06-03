using AAUG.Context.Context;
using AAUG.DataAccess.Implementations;
using AAUG.Service.General;
using AAUG.Service.Interfaces.General;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace AAUG.ServiceExtentions
{
    public static class DependencyInjectionServiceExtension
    {
        public static IServiceCollection AddDependency(this IServiceCollection services, IConfiguration configuration)
        {
            #region Add Context
            services.AddDbContext<AaugContext>(options => options.UseSqlServer(configuration.GetConnectionString("AaugDb")));
            #endregion

            #region Add UnitOfWork

            services.AddScoped<IAaugUnitOfWork, AaugUnitOfWork>();


            #endregion

            #region Service DI

            services.AddTransient<IAaugTest, AaugTest>();
            services.AddTransient<INewsService, NewsService>();

            #endregion


            return services;
        }
    }

}
