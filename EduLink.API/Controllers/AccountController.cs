using EduLink.Core.IServices.UserService;
using EduLink.Utilities.DTO;
using EduLink.Utilities.DTO.User;
using EduLink.Utilities.Helpers;
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
        private readonly IGoogleAuthService _authService;
        private readonly TokenHelper _tokenHelper;
        public AccountController(IUserService userService, IGoogleAuthService authService, TokenHelper tokenHelper)
        {
            _userService = userService;
            _authService = authService;
            _tokenHelper = tokenHelper;
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
        [HttpPost]
        public async Task<IActionResult>GoogleSignIn(GoogleSignInVM model)
        {
            var result = await _authService.GoogleSignIn(model);
            if (!result.IsSuccess || result.Data == null) 
            {
                return CreateResponse(result);
            }

            var tokenData = new TokenDTO
            {
                Email = result.Data.Email,
                Id = result.Data.Id,
                Role ="STUDENT"
            };
            var generateToken = _tokenHelper.GenerateToken(tokenData);
            return CreateResponse(new ResponseDTO<object> 
            {
                IsSuccess=true,
                Message="Sigin With Google Succesful",
                Data = new
                {
                    result,
                    generateToken
                }
               
            });
        }
    }
}
