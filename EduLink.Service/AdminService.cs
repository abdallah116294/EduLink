using EduLink.Core.Entities;
using EduLink.Core.IRepositories;
using EduLink.Core.IServices;
using EduLink.Utilities.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Service
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        public AdminService(IUnitOfWork unitOfWork, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<ResponseDTO<object>> AssignOrUpdateRoleForUser(string userId,string newRole)
        {
            try
            {
                var user =await _userManager.FindByIdAsync(userId);
                if (user == null)
                    return new ResponseDTO<object> 
                    {
                        IsSuccess=false,
                        Message="User Not Found",
                        ErrorCode=ErrorCodes.NotFound,
                    };
                var existsRole = await _roleManager.RoleExistsAsync(newRole);
                if (!existsRole)
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "Role is Exists Befor",
                        ErrorCode = ErrorCodes.BadRequest
                    };
                var result = await _userManager.AddToRoleAsync(user, newRole);
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Add Role Successful",
                    Data = result,

                };

            }catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accured {ex}",
                    ErrorCode = ErrorCodes.Exception,
                };
            }
        }

        public async Task<ResponseDTO<object>> GetAllUsers()
        {
            try
            {
                var userRepo = _unitOfWork.Repository<User>();
                var users = await userRepo.GetAllAsync();
                if (users == null)
                    return new ResponseDTO<object> {
                        IsSuccess=false,
                        Message="No Users Found",
                        ErrorCode=ErrorCodes.BadRequest,
                    };
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Get All Users",
                    Data = users,

                };
            }
            catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accured {ex}",
                    ErrorCode=ErrorCodes.Exception,
                };
            }
        }

        public async Task<ResponseDTO<object>> GetRoles()
        {
            try
            {
                var roles = await _roleManager.Roles.AsNoTracking().ToListAsync();
                if (roles == null && !roles.Any())
                    return new ResponseDTO<object> 
                    {
                        IsSuccess = false,
                        Message="No Roles Found",
                        ErrorCode=ErrorCodes.NotFound,
                    };
                return new ResponseDTO<object> 
                {
                    IsSuccess=true,
                    Message="Get All Roles ",
                    Data=roles.Select(r=>r.Name).ToList(),
                };
            }catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accured {ex}",
                    ErrorCode = ErrorCodes.Exception,
                };
            }
        }

        public async Task<ResponseDTO<object>> GetRolesOfUser(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                var roleOfUser = await _userManager.GetRolesAsync(user);
                return new ResponseDTO<object> 
                {
                    IsSuccess=true,
                    Message="User Roles",
                    Data=roleOfUser
                };

            }catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accured {ex}",
                    ErrorCode = ErrorCodes.Exception,
                };
            }
        }

        public async Task<ResponseDTO<object>> GetUserById(string userId)
        {
            try
            {
                var user=await _userManager.FindByIdAsync(userId);
                if (user == null)
                    return new ResponseDTO<object> 
                    {
                        IsSuccess=false,
                        Message="No User Found",
                        ErrorCode=ErrorCodes.NotFound,
                    };
                return new ResponseDTO<object> 
                {
                    IsSuccess=true,
                    Message="Get User Succesful",
                    Data=user,
                };
            }catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accured {ex}",
                    ErrorCode = ErrorCodes.Exception,
                };
            }
        }

        public async Task<ResponseDTO<object>> ReomveRoleFromUser(string userId, string role)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "No User Found",
                        ErrorCode = ErrorCodes.NotFound
                    };
                var result=   await _userManager.RemoveFromRoleAsync(user, role);
                if(result.Succeeded)
                    return new ResponseDTO<object>
                    {
                        IsSuccess = true,
                        Message = "Role Reomved Succesful",
                        
                    };
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Role Reomved Succesful",

                };
            }
            catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accured {ex}",
                    ErrorCode = ErrorCodes.Exception,
                };
            }
        }
    }
}
