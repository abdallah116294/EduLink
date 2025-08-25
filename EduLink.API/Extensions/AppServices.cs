using EduLink.Core.IRepositories;
using EduLink.Core.IServices;
using EduLink.Core.IServices.UserService;
using EduLink.Repository.Repositories;
using EduLink.Service.UserService;
using EduLink.Utilities.DTO.User;
using EduLink.Utilities.Helpers;
using Microsoft.Extensions.Configuration;

namespace EduLink.API.Extensions
{
    public static class AppServices
    {
        public static void  AddAppServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.Configure<EmailConfiguration>(configuration.GetSection("EmialConfiguration"));
            var emailConfig = configuration.GetSection("EmialConfiguration").Get<EmailConfiguration>();
            services.AddSingleton(emailConfig); // Register EmailConfiguration as a singleton service
            //Repository
            services.AddScoped<IUserRepository, UserRepository>();
            //Service 
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUserService, UserService>();
            //Heleper
            services.AddTransient<TokenHelper>();
            services.AddScoped<RoleSeederService>();
        }
    }
}
