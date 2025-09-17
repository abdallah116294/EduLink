using EduLink.Core.IServices;
using EduLink.Core.Specifications;
using EduLink.Utilities.DTO.Attendance;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduLink.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : BaseController
    {
        private readonly IAttendanceService _attendanceService;
        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }
        [HttpPost("Mark-Attendance")]
        public async Task<IActionResult> MarkAttendance([FromForm] AddAttendanceDTO dto)
        {
            var result = await _attendanceService.MarkAttendanceAsync(dto);
            return CreateResponse(result);
        }
        [HttpGet("Attendance-By-ClassID")]
        public async Task<IActionResult> GetClassAttendance([FromQuery]StudentSpecParms parms)
        {
            var result = await _attendanceService.GetStudentsAttanceByClass(parms);
            return CreateResponse(result);
        }
        [HttpGet("Attendance-By-ID{id}")]
        public async Task<IActionResult> GetAttendanceById(int id)
        {
            var result = await _attendanceService.GetAttendanceById(id);
            return CreateResponse(result);
        }
        [HttpPut("Update-Attendance{id}")]
        public async Task<IActionResult> UpdateAttendance(int id, [FromForm] AddAttendanceDTO dto)
        {
            var result = await _attendanceService.UpdateAttendance(id, dto);
            return CreateResponse(result);
        }
        [HttpDelete("Delete-Attendance{id}")]
        public async Task<IActionResult> DeleteAttendance(int id)
        {
            var result = await _attendanceService.DeleteAttendance(id);
            return CreateResponse(result);
        }
        [HttpGet("Get-All-Attendance")]
        public async Task<IActionResult> GetAllAttendance()
        {
            var result = await _attendanceService.GetAllAttendance();
            return CreateResponse(result);
        }

    }
}
