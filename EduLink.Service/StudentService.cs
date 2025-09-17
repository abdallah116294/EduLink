using AutoMapper;
using EduLink.Core.Entities;
using EduLink.Core.IRepositories;
using EduLink.Core.IServices;
using EduLink.Core.Specifications;
using EduLink.Core.Specifications.Parames;
using EduLink.Repository.Data;
using EduLink.Utilities.DTO;
using EduLink.Utilities.DTO.Classes;
using EduLink.Utilities.DTO.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Service
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public StudentService(IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<ResponseDTO<object>> CreateStudent(CreateStudentDTO studentDTO)
        {
            try
            {
                var studentRepo = _unitOfWork.Repository<Student>();
                var studentMapped = _mapper.Map<Student>(studentDTO);
                await studentRepo.AddAsync(studentMapped);
                await _unitOfWork.CompleteAsync();
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Add Student Succesful",
                    Data = studentMapped
                };

            }catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accured {ex}",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDTO<object>> GetAllStudents()
        {
            try
            {
                var studentspec=new StudentWithSpecification();
                var students = await _unitOfWork.Repository<Student>().GetAllAsync(new StudentWithSpecification());
                if(students == null || !students.Any())
                {
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "No students found.",
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var result = students.Select(s => new StudentResponseDTO
                {
                    FullName=s.User.FullName,
                    AdmissionNumber=s.AdmissionNumber,
                    ClassId=s.ClassId,
                    ClassName=s.Class.ClassName,
                    DateOfBirth=s.DateOfBirth,
                    EnrollmentDate=s.EnrollmentDate,
                    Email=s.User.Email,
                    Id=s.Id,
                    ParentId=s.ParentId,
                    ParentName=s.Parent.User.FullName,
                    UserId=s.UserId               
                }).ToList();
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Students retrieved successfully.",
                    Data = result,
                };
            }
            catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message =$"An error occurred while retrieving students{ex.Message}",
                    Data = null,
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDTO<object>> GetStudentAttendances(int studentId)
        {
            try
            {
                var attendanceSpec = new AttendanceSpecification(new AttendanceParames 
                {
                     StudentId=studentId
                });
                var attendances = await _unitOfWork.Repository<Attendance>().GetAllAsync(attendanceSpec);
                if(attendances == null || !attendances.Any())
                {
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "No Attendance Found for this Student",
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var result = attendances.Select(a => new AttendanceResponsForSpecifcStudent
                {
                    Date = a.Date,
                    Status = a.Status,
                    StudentId = a.StudentId,
                    StudentName = a.Student.User.FullName,
                    Id = a.Id
                }).ToList();
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Student Attendances Retrieved Successfully",
                    Data = result
                };
            }
            catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accured {ex.Message}",
                    Data = null,
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDTO<object>>GetStudentById(StudentSpecParms studentSpecParms)
        {
            try
            {
                var studentSpec = new StudentWithSpecification(
                   studentSpecParms
                    );
                var student = await _unitOfWork.Repository<Student>().GetByIdAsync(studentSpec);
                if(student == null)
                {
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "Student Not Found",
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var result = new StudentResponseDTO
                {
                    FullName = student.User.FullName,
                    AdmissionNumber = student.AdmissionNumber,
                    ClassId = student.ClassId,
                    ClassName = student.Class.ClassName,
                    DateOfBirth = student.DateOfBirth,
                    EnrollmentDate = student.EnrollmentDate,
                    Email = student.User.Email,
                    Id = student.Id,
                    ParentId = student.ParentId,
                    ParentName = student.Parent.User.FullName,
                    UserId = student.UserId
                };
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Student Retrieved Successfully",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess=false,
                    Data=null,
                    Message=$"An Error Accured {ex.Message}",
                    ErrorCode=ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDTO<object>> GetStudentFees(int studentId)
        {
            try
            {
                var feeSpec = new FeesSpecifications(new FeeParames 
                {
                    StudentId= studentId
                });
                var fees = await _unitOfWork.Repository<Fee>().GetAllAsync(feeSpec);
                if(fees == null || !fees.Any())
                {
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "No Fees Found for this Student",
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var result = fees.Select(f => new FeesResponseForSpecificStudent
                {
                    StudentId = f.StudentId,
                    StudentName = f.Student.User.FullName,
                    Amount=f.Amount,
                    DueDate=f.DueDate,
                    Status=f.Status,
                    Id = f.Id
                }).ToList();
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Student Fees Retrieved Successfully",
                    Data = result
                };
            }
            catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accured {ex.Message}",
                    Data = null,
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDTO<object>> GetStudentGrades(int studentId)
        {
            try
            {
                var gradeSpec = new GradeSpecification(new GradeParames 
                {
                    StudentId=studentId
                });
                var grades = await _unitOfWork.Repository<Grade>().GetAllAsync(gradeSpec);
                if(grades == null || !grades.Any())
                {
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "No Grades Found for this Student",
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var result = grades.Select(g => new GradeResponseForSpecificStudent
                {
                    StudentId = g.StudentId,
                    StudentName = g.Student.User.FullName,
                    Date=g.Date,
                    ExamType=g.ExamType,
                    Score=g.Score,
                    Id = g.Id
                }).ToList();
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Student Grades Retrieved Successfully",
                    Data = result
                };

            }
            catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accured {ex.Message}",
                    Data = null,
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDTO<object>> GetStudentProfile(string studentId)
        {
            try
            {
                var result = await _userRepository.GetUserById(studentId);
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Get Student Data Succesful",
                    Data = result,

                };
            }
            catch (Exception)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = "An Error Accured",
                   ErrorCode=ErrorCodes.Exception
                };
            }
        }
    }
}
