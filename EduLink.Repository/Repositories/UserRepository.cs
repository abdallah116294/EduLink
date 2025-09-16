using EduLink.Core.Entities;
using EduLink.Core.IRepositories;
using EduLink.Core.IServices;
using EduLink.Core.Specifications;
using EduLink.Utilities.DTO.Student;
using EduLink.Utilities.DTO.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emEmailService;
        private readonly IUnitOfWork _unitOfWork;

        public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, IEmailService emEmailService,IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emEmailService = emEmailService;
            _unitOfWork=unitOfWork;
        }

        public async Task<User> FindByEmailAsync(string email)
        {
           return await _userManager.FindByEmailAsync(email);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(User user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public Task<List<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetOtpAsyn(User user)
        {
            return await _userManager.GetAuthenticationTokenAsync(user, "EduLink", "ResetPassword");

        }

        public async Task<UserReponseDTO> GetUserById(string id)
        {
         
            var userSpec = new UserWithDetailsSpecification(id);
            var user = await _userManager.Users.Include(u => u.Student)
           .ThenInclude(s => s.Parent)
            .ThenInclude(p => p.User)
           .Include(u => u.Student)
              .ThenInclude(s => s.Class)
         .FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return null;
           
            var userDto = new UserReponseDTO
            {
                CreateAt = user.CreateAt,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role.ToString(),
                Student = new StudentResponseDTO
                {
                    AdmissionNumber = user.Student.AdmissionNumber,
                    Email = user.Email,
                    FullName = user.FullName,
                    Id = user.Student.Id,
                    DateOfBirth = user.Student.DateOfBirth,
                    EnrollmentDate = user.Student.EnrollmentDate,
                    ParentId = user.Student.ParentId,
                    ParentName = user.Student.Parent.User.FullName,
                    ClassId = user.Student.ClassId,
                    ClassName = user.Student.Class.ClassName,
                    UserId = user.Id
                }
            };
            return userDto;
        }

        public async Task<bool> LoginAsync(LoginDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                return false;
            }
            var result = await _userManager.CheckPasswordAsync(user, dto.Password);
            return result;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> RegisterAsync(RegisterDTO dto, string userRole)
        {

            var normalizedRole = userRole.ToUpper();
            if (!await _roleManager.RoleExistsAsync(normalizedRole))
            {
                await _roleManager.CreateAsync(new IdentityRole(normalizedRole));
            }
            if (!Enum.TryParse<UserRole>(normalizedRole, true, out var roleEnum))
            {
                throw new ArgumentException($"Invalid role '{normalizedRole}'");
            }
            var user = new User
            {
                UserName = dto.Email.ToUpper(),
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                FullName = dto.FirstName + " " + dto.LastName,
                Role = roleEnum,
                CreateAt = DateTime.UtcNow,

            };
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (result.Succeeded)
            {
                var addToRoleResult = await _userManager.AddToRoleAsync(user, normalizedRole);
                if (!addToRoleResult.Succeeded) 
                {
                    // Log the specific error
                    var errors = string.Join(", ", addToRoleResult.Errors.Select(e => e.Description));
                    Console.WriteLine($"Failed to add user to role '{normalizedRole}': {errors}");

                    await _userManager.DeleteAsync(user);
                    return addToRoleResult;
                }
                var assignedRoles = await _userManager.GetRolesAsync(user);
                Console.WriteLine($"User assigned roles: {string.Join(", ", assignedRoles)}");
            }
            return result;
        }

        public async Task<bool> RemoveOtpAsync(User user)
        {
            var result = await _userManager.RemoveAuthenticationTokenAsync(user, "EduLink", "ResetPassword");
            return result.Succeeded;

        }

        public async Task<IdentityResult> ResetPasswordAsync(User user, string resetToken, string newPassword)
        {
            return await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
        }

        public async Task<bool> SetOtpAync(User user, string otpCode)
        {
            var result = await _userManager.SetAuthenticationTokenAsync(user, "EduLink", "ResetPassword", otpCode);
            return result.Succeeded;
        }
    }
}
