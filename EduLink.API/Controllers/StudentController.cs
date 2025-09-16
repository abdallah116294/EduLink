using EduLink.Core.IServices;
using EduLink.Core.Specifications;
using EduLink.Utilities.DTO;
using EduLink.Utilities.DTO.Student;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduLink.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : BaseController
    {
      private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        [HttpGet("get-all-students")]
        public async Task<IActionResult> GetAllStudents()
        {
            var res = await _studentService.GetAllStudents();
            return CreateResponse(res);
        }
        [HttpPost("create-student")]
        public async Task<IActionResult> CreateStudent(CreateStudentDTO dto) 
        {
            var res = await _studentService.CreateStudent(dto);
            return CreateResponse(res);
        }
        [HttpGet("get-student-by-id")]
        public async Task<IActionResult> GetStudentById([FromQuery] StudentSpecParms parms) 
        {
            var res = await _studentService.GetStudentById(parms);
            return CreateResponse(res);
        }
        [HttpGet("{studentId}attendance")]
        public async Task<IActionResult> GetStudentAttendance(int studentId) 
        {
            var res = await _studentService.GetStudentAttendances(studentId);
            return CreateResponse(res);
        }
        [HttpGet("{studentId}grades")]
        public async Task<IActionResult> GetStudentGrades(int studentId) 
        {
            var res = await _studentService.GetStudentGrades(studentId);
            return CreateResponse(res);
        }
        [HttpGet("{studentId}Fees")]
        public async Task<IActionResult> GetStudentFees(int studentId) 
        {
            var res = await _studentService.GetStudentFees(studentId);
            return CreateResponse(res);
        }
        [Authorize(Roles = "STUDENT")]
        [HttpGet("Student-Profile")]
        public async Task<IActionResult> GetStudentProfile()
        {
            var userID = GetUserId();
            var result = await _studentService.GetStudentProfile(userID);
            return CreateResponse(result);
        }
    }
}
