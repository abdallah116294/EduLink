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
        [HttpPost("AddAcademicStaff")]
        public async Task<IActionResult> CreateAcademicStaff([FromForm] CreateAcademicStaffDTO dto) 
        {
            var res = await _academicStaffService.CreateAcademicStaff(dto);
            return CreateResponse(res);
        }
        [HttpGet("Get-All-AcademicSteff")]
        public async Task<IActionResult> GetAllAcademicStaff() 
        {
            var res=await _academicStaffService.GetAllAcademicStaff();
            return CreateResponse(res);
        }
        [HttpGet("Get-AcademicStaff{id}")]
        public async Task<IActionResult>GetAcademicStaffById(int id)
        {
            var res = await _academicStaffService.GetAcademicStaffById(id);
            return CreateResponse(res);
        }
        [Authorize(Roles = "ACADEMIC")]
        [HttpGet("GetAcademicSteffb")]
        public async Task<IActionResult> GetAcademicStaffByUserId() 
        {
            var userId = GetUserId();
            var res = await _academicStaffService.GetAcademicStaffByUserId(userId);
            return CreateResponse(res);
        }
        [HttpGet("Department{departmentId}")]
        public async Task<IActionResult>GetAcademicStaffByDepartment(int departmentId) 
        {
            var res = await _academicStaffService.GetAcademicStaffByDepartment(departmentId);
            return CreateResponse(res);
        }
    }
}
