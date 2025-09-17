using EduLink.Core.IServices.UserService;
using EduLink.Utilities.DTO;
using EduLink.Utilities.DTO.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace EduLink.API.Controllers
{
    [Route("api/acount")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("login")]
        public async Task<IActionResult>Login(LoginDTO dto)
        {
            var result=await _userService.LoginAsync(dto);
            return CreateResponse(result);
        }
        [HttpPost("add-admin")]
        public async Task<IActionResult> AddAdmin([FromForm]RegisterDTO dto)
        {
            var result = await _userService.RegisterAsync(dto, "Admin");
            return CreateResponse(result);
        }
        ///[Authorize(Roles = "ADMIN")]
        [HttpPost("register-academicStaff")]
        public async Task<IActionResult> RegisterAcademicStaff([FromForm] RegisterDTO dto)
        {
            var result = await _userService.RegisterAsync(dto, "Academic");
            return CreateResponse(result);
        }
        [Authorize(Roles = "ADMIN")]
        [HttpPost("register-Student")]
        public async Task<IActionResult> RegistetrStudent([FromBody] RegisterDTO dto)
        {
            var result = await _userService.RegisterAsync(dto, "Student");
            return CreateResponse(result);
        }
        [HttpPost("register-parent")]
        public async Task<IActionResult> RegistetrParent([FromBody] RegisterDTO dto)
        {
            var result = await _userService.RegisterAsync(dto, "Parent");
            return CreateResponse(result);
        }
       // [Authorize(Roles = "ADMIN")]
        [HttpPost("register-nonAcadmicstaff")]
        public async Task<IActionResult> RegistetrNonAcadmicStaff([FromForm] RegisterDTO dto)
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
        //[HttpGet("user/{id}")]
        //public async Task<IActionResult> GetUserById(string id)
        //{
        //    var response = await _userService.GetUserByID(id);
        //    return CreateResponse(response);
        //}
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutAsync();
            return CreateResponse(new ResponseDTO<object> 
            {
                IsSuccess=true,
                Message= "Logout Successful"
            });
        }
    }
}
