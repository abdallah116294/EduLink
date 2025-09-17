using EduLink.Core.IServices;
using EduLink.Utilities.DTO.Exam;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduLink.API.Controllers
{
    [Route("api/exams")]
    [ApiController]
    public class ExamController : BaseController
    {
        private readonly IExamService _examService;

        public ExamController(IExamService examService)
        {
            _examService = examService;
        }

        [HttpPost("add-exam")]
        public async Task<IActionResult>AddExam(AddExamDTO dto)
        {
            var res = await _examService.CreateExam(dto);
            return CreateResponse(res);
        }
        [HttpGet("get-all-exam")]
        public async Task<IActionResult> GetAllExam()
        {
            var res=await _examService.GetAllExam();
            return CreateResponse(res);
        }
        [HttpGet("get-exam-by{id}")]
        public async Task<IActionResult>GetExamById(int id)
        {
            var res = await _examService.GetExamById(id);
            return CreateResponse(res);
        }
        [HttpPut("edit-exam")]
        public async Task<IActionResult>EditExam(int id ,AddExamDTO dto)
        {
            var res = await _examService.UpdateExam(id, dto);
            return CreateResponse(res);
        }
        [HttpDelete("delete-exam")]
        public async Task<IActionResult>DeleteExam(int id)
        {
            var res = await _examService.DeleteExame(id);
            return CreateResponse(res);
        }

    }
}
