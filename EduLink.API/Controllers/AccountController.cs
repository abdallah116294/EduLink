using EduLink.Core.IServices.UserService;
using EduLink.Utilities.DTO.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace EduLink.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("Login")]
        public async Task<IActionResult>Login(LoginDTO dto)
        {
            var result=await _userService.LoginAsync(dto);
            return CreateResponse(result);
        }
        [HttpPost("Add-Admin")]
        public async Task<IActionResult> AddAdmin(RegisterDTO dto)
        {
            var result = await _userService.RegisterAsync(dto, "Admin");
            return CreateResponse(result);
        }
        [Authorize(Roles = "ADMIN")]
        [HttpPost("register-AcademicStaff")]
        public async Task<IActionResult> RegisterAcademicStaff(RegisterDTO dto)
        {
            var result = await _userService.RegisterAsync(dto, "Academic");
            return CreateResponse(result);
        }
        [Authorize(Roles = "ADMIN")]
        [HttpPost("Register-Student")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> RegistetrStudent(RegisterDTO dto)
        {
            var result = await _userService.RegisterAsync(dto, "Student");
            return CreateResponse(result);
        }
        [HttpPost("Register-Parent")]
        public async Task<IActionResult> RegistetrParent(RegisterDTO dto)
        {
            var result = await _userService.RegisterAsync(dto, "Parent");
            return CreateResponse(result);
        }
        [Authorize(Roles = "ADMIN")]
        [HttpPost("Register-NonAcadmicStaff")]
        public async Task<IActionResult> RegistetrNonAcadmicStaff(RegisterDTO dto)
        {
            var result = await _userService.RegisterAsync(dto, "NonAcademic");
            return CreateResponse(result);
        }
        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordDTO dto)
        {
            var response = await _userService.ForgetPassword(dto);
            return CreateResponse(response);
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO dto)
        {
            var response = await _userService.ResetPasswordAsync(dto.Email, dto.otp, dto.NewPassword);
            return CreateResponse(response);
        }
    }
}
