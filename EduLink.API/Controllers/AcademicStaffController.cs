using EduLink.Core.IServices;
using EduLink.Utilities.DTO.AcademicStaff;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduLink.API.Controllers
{
    [Route("api/academic-staff")]
    [ApiController]
    public class AcademicStaffController : BaseController 
    {
        private readonly IAcademicStaffService _academicStaffService;

        public AcademicStaffController(IAcademicStaffService academicStaffService)
        {
            _academicStaffService = academicStaffService;
        }
        [HttpPost("academicstaff")]
        public async Task<IActionResult> CreateAcademicStaff([FromForm] CreateAcademicStaffDTO dto) 
        {
            var res = await _academicStaffService.CreateAcademicStaff(dto);
            return CreateResponse(res);
        }
        [HttpGet("get-all-academicstaff")]
        public async Task<IActionResult> GetAllAcademicStaff() 
        {
            var res=await _academicStaffService.GetAllAcademicStaff();
            return CreateResponse(res);
        }
        [HttpGet("get-academicstaff-by{id}")]
        public async Task<IActionResult>GetAcademicStaffById(int id)
        {
            var res = await _academicStaffService.GetAcademicStaffById(id);
            return CreateResponse(res);
        }
        [Authorize(Roles = "ACADEMIC")]
        [HttpGet("get-academicstaff")]
        public async Task<IActionResult> GetAcademicStaffByUserId() 
        {
            var userId = GetUserId();
            var res = await _academicStaffService.GetAcademicStaffByUserId(userId);
            return CreateResponse(res);
        }
        [HttpGet("department{departmentId}")]
        public async Task<IActionResult>GetAcademicStaffByDepartment(int departmentId) 
        {
            var res = await _academicStaffService.GetAcademicStaffByDepartment(departmentId);
            return CreateResponse(res);
        }
        [HttpDelete("delete-academicstaff{id}")]    
        public async Task<IActionResult> DeleteAcademicStaff(int id) 
        {
            var res = await _academicStaffService.DeleteAcademicStaff(id);
            return CreateResponse(res);
        }
        [HttpPut("update-academicstaff{id}")]
        public async Task<IActionResult> UpdateAcademicStaff(int id, [FromForm] CreateAcademicStaffDTO dto) 
        {
            var res = await _academicStaffService.UpdateAcademicStaff(id, dto);
            return CreateResponse(res);
        }
    }
}
