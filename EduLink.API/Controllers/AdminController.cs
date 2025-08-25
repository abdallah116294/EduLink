using EduLink.Core.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduLink.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : BaseController
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [Authorize(Roles ="ADMIN")]
        [HttpGet("get-all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var res = await _adminService.GetAllUsers();
            return CreateResponse(res);
        }
        [Authorize(Roles = "ADMIN")]
        [HttpGet("get-user-byId")]
        public async Task<IActionResult>GetUserByID(string userId)
        {
            var res = await _adminService.GetUserById(userId);
            return CreateResponse(res);
        }
        [Authorize(Roles = "ADMIN")]
        [HttpGet("get-all-roles")]
        public async Task<IActionResult>GetAllRoles()
        {
            var res = await _adminService.GetRoles();
            return CreateResponse(res);
        }
        [Authorize(Roles = "ADMIN")]
        [HttpPost("assign-role-to-user")]
        public async Task<IActionResult>AssignRoleToUser(string userId, string roleName)
        {
            var res= await _adminService.AssignOrUpdateRoleForUser(userId,roleName);
            return CreateResponse(res);
        }
        [Authorize(Roles = "ADMIN")]
        [HttpGet("get-roles-of-users")]
        public async Task<IActionResult>GetRolesOfUsers(string userID)
        {
            var res=await _adminService.GetRolesOfUser(userID);
            return CreateResponse(res);
        }
        [Authorize(Roles = "ADMIN")]
        [HttpDelete("remove-role-from-user")]
        public async Task<IActionResult>ReomveRoleFromUser(string userId,string role)
        {
            var res = await _adminService.ReomveRoleFromUser(userId, role);
            return CreateResponse(res);
        }
    }
}
