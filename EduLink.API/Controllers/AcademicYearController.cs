using EduLink.Core.IServices;
using EduLink.Utilities.DTO.AcademicYear;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduLink.API.Controllers
{
    [Route("api/academic-years")]
    [ApiController]
    public class AcademicYearController : BaseController
    {
        private readonly IAcademicYearService _academicYearService;
        public AcademicYearController(IAcademicYearService academicYearService)
        {
            _academicYearService = academicYearService;
        }
        [HttpPost("create-academic-year")]
        public async Task<IActionResult> CreateAcademicYear(CreateAcademicYearDTO dto)
        {
            var res = await _academicYearService.CreateAcademicYear(dto);
            return CreateResponse(res);
        }
        [HttpGet("get-all-academic-years")]
        public async Task<IActionResult> GetAllAcademicYears()
        {
            var res = await _academicYearService.GetAllAcademicYears();
            return CreateResponse(res);
        }
        [HttpGet("get-academic-year/{id}")]
        public async Task<IActionResult> GetAcademicYear(int id)
        {
            var res = await _academicYearService.GetAcademicYear(id);
            return CreateResponse(res);
        }
        [HttpPut("update-academic-year")]
        public async Task<IActionResult> UpdateAcademicYear(UpdateAcademicYearDTO dto)
        { 
            var res = await _academicYearService.UpdateAcademicYear(dto);
            return CreateResponse(res);
        }
        [HttpDelete("delete-academic-year/{id}")]
        public async Task<IActionResult> DeleteAcademicYear(int id)
        {
            var res = await _academicYearService.DeleteAcademicYear(id);
            return CreateResponse(res);
        }
        [HttpGet("get-current-academic-year")]
        public async Task<IActionResult> GetCurrentAcademicYear() 
        {
            var res = await _academicYearService.GetCurrentAcademicYear();
            return CreateResponse(res);
        }

    }
}
