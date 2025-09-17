using EduLink.Core.IRepositories;
using EduLink.Core.IServices;
using EduLink.Core.IServices.UserService;
using EduLink.Repository.Repositories;
using EduLink.Service;
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
            services.AddAutoMapper(typeof(MappingProfile));
            //Repository
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //Service 
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IParentService, ParentService>();
            services.AddScoped<IAcademicYearService, AcademicYearService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IClassService, ClassService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IAttendanceService, AttendanceService>();
            services.AddScoped<IAcademicStaffService, AcademicStaffService>();
            services.AddScoped<INonAcademicStaffService, NonAcademicStaffService>();
            services.AddScoped<ISubjectsService, SubjectsService>();
            services.AddScoped<IGradeService, GradeService>();
            services.AddScoped<IFeeService, FeeService>();
            //Heleper
            services.AddTransient<TokenHelper>();
            services.AddScoped<RoleSeederService>();
        }
    }
}
