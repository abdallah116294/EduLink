using EduLink.Core.IServices;
using EduLink.Utilities.DTO.NonAcademicStaff;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduLink.API.Controllers
{
    [Route("api/non-academic-staff")]
    [ApiController]
    public class NonAcademicStaffController : BaseController
    {
        private readonly INonAcademicStaffService _nonAcademicStaffService;
        public NonAcademicStaffController(INonAcademicStaffService nonAcademicStaffService)
        {
            _nonAcademicStaffService = nonAcademicStaffService;
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddNonAcademicStaff([FromForm] CreateNonAcademicSteffDTO dto)
        {
            var result = await _nonAcademicStaffService.AddNonAcademicStaffAsync(dto);
            return CreateResponse(result);
        }
        [HttpGet("get-non-academic-steff")]
        public async Task<IActionResult> GetAllNonAcademicSteff()
        {
            var res = await _nonAcademicStaffService.GetAllNonAcademicStaff();
            return CreateResponse(res);
        }
        [HttpGet("get-non-academic-steff{id}")]
        public async Task<IActionResult>GetAcademicSteffById(int id)
        {
            var res = await _nonAcademicStaffService.GetNonAcademicSteffById(id);
            return CreateResponse(res);
        }
        [Authorize(Roles = "NONACADEMIC")]
        [HttpGet("get-non-academic-steff-user")]
        public async Task<IActionResult> GetNonAcademicSteffByUserId()
        {
            var userId = GetUserId();
            var res = await _nonAcademicStaffService.GetNonAcademicSteffByUserId(userId);
            return CreateResponse(res);
        }
        [HttpGet("get-non-academic-steff{departmentId}")]
        public async Task<IActionResult> GetNonAcademicSteffByDepartmentId(int departmentId)
        {
            
            var res = await _nonAcademicStaffService.GetNonAcademicStaffByDepartment(departmentId);
            return CreateResponse(res);
        }
    }
}
