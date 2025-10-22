using EduLink.Core.Entities;
using EduLink.Core.IRepositories;
using EduLink.Core.IServices;
using EduLink.Core.IServices.NotificationService;
using EduLink.Core.Specifications;
using EduLink.Core.Specifications.Parames;
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
        private readonly INotificationService _notificationService;
        public AttendanceService(IUnitOfWork unitOfWork, INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;
        }

        public async Task<ResponseDTO<object>> DeleteAttendance(int id)
        {
            try
            {
                var spec = new AttendanceSpecification(new AttendanceParames 
                { 
                    Id = id
                });
                var attendanceRepo = _unitOfWork.Repository<Attendance>();
                var attendance = await attendanceRepo.GetByIdAsync(spec);
                if(attendance == null)
                {
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "Attendance Record Not Found",
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                await attendanceRepo.DeleteAsync(id);
                await _unitOfWork.CompleteAsync();
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Attendance record deleted successfully",
                    Data = null
                };

            }
            catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"Error deleting attendance: {ex.Message}",
                    Data = null,
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDTO<object>> GetAllAttendance()
        {
            try
            {
                var spec = new AttendanceSpecification();
                var attendanceRepo = _unitOfWork.Repository<Attendance>();
                var attendance = await attendanceRepo.GetAllAsync(spec);
                if(attendance == null || attendance.Count==0)
                {
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "No Attendance Records Found",
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var result = attendance.Select(a => new AttendanceResponsForSpecifcStudent
                {
                    Id=a.Id,
                    StudentId=a.StudentId,
                    StudentName=a.Student.User.FullName,
                    Date=a.Date,
                    Status=a.Status
                }).ToList();
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Attendance Records fetched successfully",
                    Data = result
                };
            }
            catch (Exception ex) 
            { 
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"Error fetching attendance: {ex.Message}",
                    Data = null,
                    ErrorCode = ErrorCodes.Exception
                };
            }

        }

        public async Task<ResponseDTO<object>> GetAttendanceById(int id)
        {
            try
            {
                var spec = new AttendanceSpecification(new AttendanceParames 
                {
                    Id = id
                });
                var attendanceRepo = _unitOfWork.Repository<Attendance>();
                var attendance = await attendanceRepo.GetByIdAsync(spec);
                if(attendance == null)
                {
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "Attendance Record Not Found",
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var result = new AttendanceResponsForSpecifcStudent
                {
                    Id=attendance.Id,
                    StudentId=attendance.StudentId,
                    StudentName=attendance.Student.User.FullName,
                    Date=attendance.Date,
                    Status=attendance.Status
                };
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Attendance Record fetched successfully",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"Error fetching attendance by id: {ex.Message}",
                    Data = null,
                    ErrorCode = ErrorCodes.Exception
                };
            }
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
                    Data = result.ToList()
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
                var attendancSpec = new AttendanceSpecification(new AttendanceParames 
                {
                    StudentId=dto.StudentId,
                });
                //Using StudentSpec
                var studentSpec = new StudentWithSpecification(new StudentSpecParms {
                    StudentId=dto.StudentId,
                });
                //Get Student 
                var student = await _unitOfWork.Repository<Student>().GetByIdAsync(studentSpec);
                var attendanceRepo = _unitOfWork.Repository<Attendance>();
                if (student == null)
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "Student not found"
                    };
                var existingAttendance = (await _unitOfWork.Repository<Attendance>()
                    .GetAllAsync(attendancSpec))
                    .FirstOrDefault();
                Attendance attendance2;
                bool isUpdate = false;
                if (existingAttendance != null)
                {
                    existingAttendance.Status = dto.IsPresent ? "Present" : "Absent";
                    existingAttendance.Date = dto.Date;
                   await _unitOfWork.Repository<Attendance>().UpdateAsync(existingAttendance);
                    attendance2 = existingAttendance;
                    isUpdate = true;
                }
                else
                {
                    attendance2 = new Attendance
                    {
                        StudentId = dto.StudentId,
                        Date = dto.Date,
                        Status = dto.IsPresent ? "Present" : "Absent"
                    };
                    await _unitOfWork.Repository<Attendance>().AddAsync(attendance2);
                }
                var attendance = new Attendance
                {
                    StudentId = dto.StudentId,
                    Date = DateTime.Now,
                    Status = dto.IsPresent==true ? "Present" : "Absent",
                };
                //await attendanceRepo.AddAsync(attendance);
                await _unitOfWork.CompleteAsync();
                //Send Notification
                await SendAttendanceNotification(student,attendance2,dto.IsPresent);
                var attendanceResponse = await attendanceRepo.GetByIdAsync(attendancSpec);
                var result = new AttendanceResponsForSpecifcStudent
                {
                    Id=attendanceResponse.Id,
                    StudentId=attendanceResponse.StudentId,
                    Date= attendanceResponse.Date,
                    StudentName= attendanceResponse.Student.User.FullName,
                    Status=attendanceResponse.Status,
                    
                };
                var response = new AttendanceResponseDTO
                {
                    Id = attendance.Id,
                    StudentId = student.Id,
                    StudentName = student.User?.FullName ?? "Unknown",
                    AdmissionNumber = student.AdmissionNumber,
                    Date = attendance.Date,
                    Status = attendance.Status,
                    IsPresent = dto.IsPresent,
                    NotificationSent = true
                };
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Attendance marked successfully",
                    Data = response
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
        private async Task SendAttendanceNotification(Student student, Attendance attendance, bool isPresent)
        {
            var studentName = student.User?.FullName ?? "Student";
            var statusText = isPresent ? "Present" : "Absent";
            var dateText = attendance.Date.ToString("MMMM dd, yyyy");
            var studentNotification = new AttendanceNotificationDto
            {
                AttendanceId = attendance.Id,
                StudentName = studentName,
                AdmissionNumber = student.AdmissionNumber,
                Date = attendance.Date,
                IsPresent = isPresent,
                Status = attendance.Status,
                Message = $"Your attendance has been marked as {statusText} for {dateText}.",
                NotificationTime = DateTime.UtcNow,
                RecipientType = "Student"
            };
            var parentNotification = new AttendanceNotificationDto
            {
                AttendanceId = attendance.Id,
                StudentName = studentName,
                AdmissionNumber = student.AdmissionNumber,
                Date = attendance.Date,
                IsPresent = isPresent,
                Status = attendance.Status,
                Message = $"{studentName} (Admission No: {student.AdmissionNumber}) was marked {statusText} for {dateText}.",
                NotificationTime = DateTime.UtcNow,
                RecipientType = "Parent"
            };
            if (!string.IsNullOrEmpty(student.UserId))
            {
                await _notificationService.SendAttendanceNotificationAsync(
                    student.UserId,
                    studentNotification);
            }
            if (student.Parent != null && !string.IsNullOrEmpty(student.Parent.UserId))
            {
                await _notificationService.SendAttendanceNotificationAsync(
                    student.Parent.UserId,
                    parentNotification);
            }
        }
        public async Task<ResponseDTO<object>> UpdateAttendance(int id, AddAttendanceDTO dto)
        {
            try 
            {
                var spec= new AttendanceSpecification(new AttendanceParames 
                { 
                    Id = id
                });
                var attendanceRepo = _unitOfWork.Repository<Attendance>();
                var attendance = await attendanceRepo.GetByIdAsync(spec);
                if(attendance == null)
                {
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "Attendance Record Not Found",
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                //attendance.Status = dto.IsPresent ? "Present" : "Absent";
                //attendance.Date = DateTime.Now;
                await attendanceRepo.UpdateAsync(id, entity => 
                {
                    entity.Id = id;
                    entity.StudentId = dto.StudentId;
                    entity.Status = dto.IsPresent ? "Present" : "Absent";
                    entity.Date = DateTime.Now;
                });
                await _unitOfWork.CompleteAsync();
                
                var updatedAttendance = await attendanceRepo.GetByIdAsync(spec);
                var result = new AttendanceResponsForSpecifcStudent
                {
                    Id=updatedAttendance.Id,
                    StudentId=updatedAttendance.StudentId,
                    Date=updatedAttendance.Date,
                    StudentName=updatedAttendance.Student.User.FullName,
                    Status=updatedAttendance.Status
                };
                
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Attendance updated successfully",
                    Data = result
                };

            }
            catch(Exception ex) 
            { 
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"Error updating attendance: {ex.Message}",
                    Data = null,
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }
    }
}
