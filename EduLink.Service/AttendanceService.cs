using EduLink.Core.Entities;
using EduLink.Core.IRepositories;
using EduLink.Core.IServices;
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
        public AttendanceService(IUnitOfWork unitOfWork)
        {
           _unitOfWork = unitOfWork;
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
                var attendancSpec = new AttendanceSpecification(new AttendanceParames 
                {
                    StudentId=dto.StudentId,
                });
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
