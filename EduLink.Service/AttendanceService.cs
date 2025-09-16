using EduLink.Core.Entities;
using EduLink.Core.IRepositories;
using EduLink.Core.IServices;
using EduLink.Core.Specifications;
using EduLink.Utilities.DTO;
using EduLink.Utilities.DTO.Attendance;
using EduLink.Utilities.DTO.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Service
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AttendanceService(IUnitOfWork unitOfWork)
        {
           _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDTO<object>> GetStudentsAttanceByClass(StudentSpecParms studentSpecParms)
        {
            try
            {
                var studentSpec = new StudentWithSpecification(
                    studentSpecParms);
                var classRepo = _unitOfWork.Repository<Student>();
                var classes = await classRepo.GetAllAsync(studentSpec);
                if(classes == null || classes.Count==0)
                {
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "No Students Found in this class",
                        Data = null,
                        ErrorCode=ErrorCodes.NotFound
                    };
                }
                var result = classes.Select(c => new AttendanceForSpecificClass 
                {
                    ClassId=c.ClassId,
                    ClassName=c.Class.ClassName,
                    
                    StudentStatuses=c.Attendance.Select(s=> new AttendanceResponsForSpecifcStudent
                    {
                        Date=s.Date,
                        StudentId=s.Id,
                        StudentName=s.Student.User.FullName,
                        Status=s.Status,
                    }).ToList()
                }).ToList();
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Students Attendance fetched successfully",
                    Data = result
                };


            }
            catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"Error fetching attendance: {ex.Message}",
                    Data = null,
                    ErrorCode=ErrorCodes.Exception
                };
            }
        }

        public  async Task<ResponseDTO<object>> MarkAttendanceAsync(AddAttendanceDTO dto)
        {
            try
            {
                var attendancSpec = new AttendanceSpecification(dto.StudentId);
                var attendanceRepo = _unitOfWork.Repository<Attendance>();
                var attendance = new Attendance
                {
                    StudentId = dto.StudentId,
                    Date = DateTime.Now,
                    Status = dto.IsPresent==true ? "Present" : "Absent",
                };
                await attendanceRepo.AddAsync(attendance);
                await _unitOfWork.CompleteAsync();
                var attendanceResponse = await attendanceRepo.GetByIdAsync(attendancSpec);
                var result = new AttendanceResponsForSpecifcStudent
                {
                    Id=attendanceResponse.Id,
                    StudentId=attendanceResponse.StudentId,
                    Date= attendanceResponse.Date,
                    StudentName= attendanceResponse.Student.User.FullName,
                    Status=attendanceResponse.Status,
                    
                };
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Attendance marked successfully",
                    Data = result
                };
            }
            catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"Error marking attendance: {ex.Message}",
                    Data = null
                };
            }
        }
    }
}
