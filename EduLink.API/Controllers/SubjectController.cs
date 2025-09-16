using EduLink.Core.IServices;
using EduLink.Utilities.DTO.Subject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduLink.API.Controllers
{
    [Route("api/subjects")]
    [ApiController]
    public class SubjectController : BaseController
    {
        private readonly ISubjectsService _subjectService;
         public SubjectController(ISubjectsService subjectService)
        {
            _subjectService = subjectService;
        }
        [HttpPost("add-subject")]
        public async Task<IActionResult> AddSubject([FromForm] CreateSubjectDTO dto)
        {
            var res = await _subjectService.CreateSubject(dto);
            return CreateResponse(res);
        }
        [HttpGet("get-all-subjects")]
        public async Task<IActionResult> GetAllSubjects()
        {
            var res = await _subjectService.GetAllSubject();
            return CreateResponse(res);
        }
        [HttpGet("get-subject-by{id}")]
        public async Task<IActionResult>GetSubjectById(int id)
        {
            var res = await _subjectService.GetSubjectById(id);
            return CreateResponse(res);
        }
        [HttpDelete("delet-subject{id}")]
        public async Task<IActionResult>DeleteSubjectById(int id)
        {
            var res = await _subjectService.DeleteSubject(id);
            return CreateResponse(res);
        }
        [HttpPut("assign-teacher-by")]
        public async Task<IActionResult>AssignTeacherById(int teacherId,int subjectId)
        {
            var res = await _subjectService.AssignTeacherToSubject(subjectId,teacherId);
            return CreateResponse(res);
        }
    }
}
