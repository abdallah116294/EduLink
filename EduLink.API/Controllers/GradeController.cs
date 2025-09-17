using EduLink.Core.IServices;
using EduLink.Utilities.DTO.Grade;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduLink.API.Controllers
{
    [Route("api/grades")]
    [ApiController]
    public class GradeController : BaseController
    {
        private readonly IGradeService _gradeService;

        public GradeController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }
        [HttpPost("add-grade")]
        public async Task<IActionResult>AddGrade(CreateGradeDTO dto)
        {
            var res = await _gradeService.AddGrade(dto);
            return CreateResponse(res);
        }
        [HttpGet("get-all-grades")]
        public async Task<IActionResult> GetAllGrades()
        {
            var res= await _gradeService.GetAllGrades();
            return CreateResponse(res);
        }
        [HttpGet("get-grade-by{id}")]
        public async Task<IActionResult>GetGradeById(int id)
        {
            var res = await _gradeService.GetGradeById(id);
            return CreateResponse(res);
        }
        [HttpPut("update-grade")]
        public async Task<IActionResult>UpdateGrade(int id,CreateGradeDTO dto)
        {
            var res = await _gradeService.UpdateGrade(id, dto);
            return CreateResponse(res);
        }
        [HttpDelete("delete-grade{id}")]
        public async Task<IActionResult>DeleteGrade(int id)
        {
            var res=await _gradeService.DeleteGrade(id);
            return CreateResponse(res);
        }

    }
}
