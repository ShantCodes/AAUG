using AAUG.Context.Context;
using AAUG.DataAccess.Implementations;
using AAUG.DataAccess.Implementations.UnitOfWork;
using AAUG.Service.General;
using AAUG.Service.Implementations;
using AAUG.Service.Implementations.EmailSender;
using AAUG.Service.Implementations.General;
using AAUG.Service.Implementations.Media;
using AAUG.Service.Implementations.Notification;
using AAUG.Service.Interfaces;
using AAUG.Service.Interfaces.EmailSender;
using AAUG.Service.Interfaces.General;
using AAUG.Service.Interfaces.Media;
using AAUG.Service.Interfaces.Notification;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("AaugDb"),
                b => b.MigrationsAssembly("AAUG.Api")));
            #endregion

            #region Add UnitOfWork

            services.AddScoped<IAaugUnitOfWork, AaugUnitOfWork>();

            #endregion

            #region Authentication DI
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmailSenderService, EmailSenderService>();
            #endregion

            #region Service DI

            services.AddTransient<IAaugTest, AaugTest>();
            services.AddTransient<INewsService, NewsService>();
            services.AddTransient<IAaugUserService, AaugUserService>();
            services.AddTransient<IEventService, EventService>();
            services.AddTransient<ISlideShowService, SlideShowService>();

            #region media service DI
            services.AddTransient<IMediaFileService, MediaFIleService>();
            #endregion       

            #endregion

            #region User Service DI
            services.AddScoped<IUserService, UserService>();

            #endregion
            #region push notification
            services.AddSingleton<NotificationService>(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var unitOfWork = sp.GetRequiredService<IAaugUnitOfWork>();
                var mapper = sp.GetRequiredService<IMapper>();

                return new NotificationService(unitOfWork, mapper, configuration);
            });

            services.AddTransient<INotificationService, NotificationService>();

            #endregion


            return services;
        }
    }

}
