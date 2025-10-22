using ClosedXML.Excel;
using EduLink.Core.IServices;
using EduLink.Core.Specifications;
using EduLink.Utilities.DTO.Attendance;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduLink.API.Controllers
{
    [Route("api/attendance")]
    [ApiController]
    public class AttendanceController : BaseController
    {
        private readonly IAttendanceService _attendanceService;
        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }
        [HttpPost("mark-attendance")]
        public async Task<IActionResult> MarkAttendance([FromForm] AddAttendanceDTO dto)
        {
            var result = await _attendanceService.MarkAttendanceAsync(dto);
            return CreateResponse(result);
        }
        [HttpGet("attendance-by-classId")]
        public async Task<IActionResult> GetClassAttendance([FromQuery]StudentSpecParms parms)
        {
            var result = await _attendanceService.GetStudentsAttanceByClass(parms);
            return CreateResponse(result);
        }
        [HttpGet("attendance-by{id}")]
        public async Task<IActionResult> GetAttendanceById(int id)
        {
            var result = await _attendanceService.GetAttendanceById(id);
            return CreateResponse(result);
        }
        [HttpPut("update-attendance{id}")]
        public async Task<IActionResult> UpdateAttendance(int id, [FromForm] AddAttendanceDTO dto)
        {
            var result = await _attendanceService.UpdateAttendance(id, dto);
            return CreateResponse(result);
        }
        [HttpDelete("delete-attendance{id}")]
        public async Task<IActionResult> DeleteAttendance(int id)
        {
            var result = await _attendanceService.DeleteAttendance(id);
            return CreateResponse(result);
        }
        [HttpGet("get-all-attendance")]
        public async Task<IActionResult> GetAllAttendance()
        {
            var result = await _attendanceService.GetAllAttendance();
            return CreateResponse(result);
        }
        [HttpGet("export/{classId}")]
        public async Task<IActionResult> ExportClassAttendees([FromQuery] StudentSpecParms parms) 
        {
            var response = await _attendanceService.GetStudentsAttanceByClass(parms);
            if (!response.IsSuccess || response.Data == null) 
            { return BadRequest(response.Message); }
            // 2. Cast Data to your expected type 
            var attendanceData = response.Data as List<AttendanceForSpecificClass>;
            if (attendanceData == null) { return BadRequest("Invalid data format"); }

            // 3. Create Excel file
            using var workbook = new XLWorkbook();
            foreach (var classAttendance in attendanceData)
            {
                var worksheet = workbook.Worksheets.Add(classAttendance.ClassName);
                // Headers
                worksheet.Cell(1, 1).Value = "Student ID";
                worksheet.Cell(1, 2).Value = "Student Name"; 
                worksheet.Cell(1, 3).Value = "Date";
                worksheet.Cell(1, 4).Value = "Status"; 
                int row = 2;
                foreach (var student in classAttendance.StudentStatuses)
                {
                    worksheet.Cell(row, 1).Value = student.StudentId;
                    worksheet.Cell(row, 2).Value = student.StudentName;
                    worksheet.Cell(row, 3).Value = student.Date.ToString("yyyy-MM-dd");
                    worksheet.Cell(row, 4).Value = student.Status; 
                    row++;
                }
                worksheet.Columns().AdjustToContents();
            }
            // 4. Save to stream
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;
            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Class_Attendance.xlsx");
            // return CreateResponse(response);
        }
    }
}
