using EduLink.Core.IServices;
using EduLink.Utilities.DTO.Department;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduLink.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : BaseController
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        [HttpGet("get-all-departments")]    
        public async Task<IActionResult> GetAllDepartments()
        {
            var res = await _departmentService.GetAllDepartments();
            return CreateResponse(res);
        }
        [HttpGet("get-department/{id}")]
        public async Task<IActionResult> GetDepartment(int id)
        {
            var res = await _departmentService.GetDepartment(id);
            return CreateResponse(res);
        }
        [HttpPost("create-department")]
        public async Task<IActionResult> CreateDepartment(CreateDepartmentDTO dto)
        {
            var res = await _departmentService.CreateDepartment(dto);
            return CreateResponse(res);
        }
        [HttpPut("update-department")]
        public async Task<IActionResult> UpdateDepartment(int id,UpdateDepartmentDTO dto)
        {
            var res = await _departmentService.UpdateDepartment(id,dto);
            return CreateResponse(res);
        }
        [HttpDelete("delete-department/{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var res = await _departmentService.DeleteDepartment(id);
            return CreateResponse(res);
        }
        [HttpPost("assign-head-academic-steff")]
        public async Task<IActionResult>AssignHeadDepartmentAcademicSteff(int steffId)
        {
            var res = await _departmentService.AssignAcademicSteffHeadToDepartment(steffId);
            return CreateResponse(res);
        }
        [HttpPost("assign-head-non-academic-steff")]
        public async Task<IActionResult> AssignHeadDepartmentNonAcademicSteff(int steffId)
        {
            var res = await _departmentService.AssignNonAcademicSteffHeadToDepartment(steffId);
            return CreateResponse(res);
        }
    }
}
