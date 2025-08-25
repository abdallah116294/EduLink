using EduLink.Core.Entities;
using EduLink.Repository.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace EduLink.API.Extensions
{
    public static class AppConnectionString
    {
        public static void AddConnectionString(this IServiceCollection service,IConfiguration configuration)
        {
            service.AddDbContext<EduLinkDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("EduLinkConnectionString"));
            });
            service.AddIdentityCore<User>(u =>
            {
                u.User.RequireUniqueEmail = true;
                u.Password.RequireDigit = true;
                u.Password.RequireLowercase = true;
                u.Password.RequireUppercase = true;
                u.Password.RequireNonAlphanumeric = false;
                u.Password.RequiredLength = 6;
                u.Password.RequiredUniqueChars = 1;
            }).AddRoles<IdentityRole>()
           .AddEntityFrameworkStores<EduLinkDbContext>()
           .AddSignInManager<SignInManager<User>>()
           .AddDefaultTokenProviders();
        }
    }
}
