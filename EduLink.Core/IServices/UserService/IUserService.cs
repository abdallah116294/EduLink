using EduLink.Core.Entities;
using EduLink.Utilities.DTO;
using EduLink.Utilities.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.IServices.UserService
{
    public interface IUserService
    {
        Task LogoutAsync();
        Task<ResponseDTO<object>> LoginAsync(LoginDTO dto);
        Task<ResponseDTO<object>> RegisterAsync(RegisterDTO dto, string userRole);
        Task<ResponseDTO<object>> ForgetPassword(ForgetPasswordDTO dto);
        Task<ResponseDTO<object>> ResetPasswordAsync(string email, string otp, string newPassword);
        Task<ResponseDTO<object>> GetAllUser();
        Task<ResponseDTO<object>> GetUserByID(string id);
    }
}
