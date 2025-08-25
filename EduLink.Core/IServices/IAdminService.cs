using EduLink.Utilities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.IServices
{
    public interface IAdminService
    {
        // Get All Users 
        Task<ResponseDTO<object>> GetAllUsers();
        Task<ResponseDTO<object>> GetUserById(string userId);
        Task<ResponseDTO<object>> GetRoles();
        Task<ResponseDTO<object>>GetRolesOfUser(string userId);
        Task<ResponseDTO<object>> AssignOrUpdateRoleForUser(string  userId,string newRole);
        Task<ResponseDTO<object>> ReomveRoleFromUser(string userId, string role);
    }
}
