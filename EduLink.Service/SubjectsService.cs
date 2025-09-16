using EduLink.Core.Entities;
using EduLink.Core.IRepositories;
using EduLink.Core.IServices;
using EduLink.Core.Specifications;
using EduLink.Utilities.DTO;
using EduLink.Utilities.DTO.AcademicStaff;
using EduLink.Utilities.DTO.AcademicYear;
using EduLink.Utilities.DTO.Classes;
using EduLink.Utilities.DTO.Department;
using EduLink.Utilities.DTO.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Service
{
    public class SubjectsService : ISubjectsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubjectsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDTO<object>> AssignTeacherToSubject(int subjectId, int teacherId)
        {
            try
            {
                var academicSteff = await _unitOfWork.Repository<AcademicStaff>().GetByIdAsync(teacherId);
                var subject = await _unitOfWork.Repository<Subject>().GetByIdAsync(subjectId);
                if (academicSteff == null&&subject==null)
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "Non Data found",
                        ErrorCode=ErrorCodes.NotFound
                    };
                await _unitOfWork.Repository<Subject>().UpdateAsync(subjectId, entity => 
                {
                    entity.AcadmicStaffId = teacherId;
                });
                await _unitOfWork.CompleteAsync();
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "teacher Assigend to subject",

                };

            }catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<ResponseDTO<object>> CreateSubject(CreateSubjectDTO dto)
        {
            try
            { 
                dto.Code= GenerateRandomCode();
                var subjectDTO = new Subject
                {
                    SubjectName = dto.SubjectName,
                    Code = GenerateRandomCode(),
                    ClassId = dto.ClassId,
                    AcadmicStaffId=dto.AcadmicStaffId
                };
                 await _unitOfWork.Repository<Subject>().AddAsync(subjectDTO);
                await _unitOfWork.CompleteAsync();
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Subject created successfully",
                    Data = subjectDTO
                };

            }
            catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<ResponseDTO<object>> DeleteSubject(int id)
        {
            try
            {
                var subject = await _unitOfWork.Repository<Subject>().GetByIdAsync(id);
                if (subject == null)
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "No Subject Found ",
                        ErrorCode = ErrorCodes.NotFound
                    };
                await _unitOfWork.Repository<Subject>().DeleteAsync(id);
                await _unitOfWork.CompleteAsync();
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Delete Subject Successful"
                };
            }catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null,
                    ErrorCode=ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDTO<object>> GetAllSubject()
        {
            try
            {
                var spec = new SubjectSpecification();
                var subjects = await _unitOfWork.Repository<Subject>().GetAllAsync(spec);
                if (subjects == null)
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "No Subjects Found",
                        ErrorCode=ErrorCodes.NotFound
                    };
                var subjectresponse = subjects.Select(s => new SubjectResponseDTO 
                { 
                    Id=s.Id,
                    SubjectName=s.SubjectName,
                    Code=s.Code,
                   ClassId=s.ClassId,
                   Class=new ClassResponsDTO 
                   {
                       AcademicYearId=s.Class.AcademicYearId,
                       ClassName=s.Class.ClassName,
                       Id=s.Class.Id,
                       GradeLevel=s.Class.GradeLevel,
                       AcademicYearName=s.Class.AcadmicYear.YearName, 
                   },
                   AcademicSteff=new AcademicStaffResponse
                   {
                       Id=s.AcademicStaff.Id,
                       FullName=s.AcademicStaff.User.FullName,
                       Email=s.AcademicStaff.User.Email,
                       PhoneNumber=s.AcademicStaff.User.PhoneNumber,
                       DepartmentId=s.AcademicStaff.DepartmentId,
                       Specialization=s.AcademicStaff.Specialization,
                       UserId = s.AcademicStaff.UserId,
                       //Department=new DepartmentResponseDTO 
                       //{
                       //    Id=s.AcademicStaff.Department.Id,
                       //    DepartmentName=s.AcademicStaff.Department.DepartmentName,
                       //}
                   },
                    
                }).ToList();
                return new ResponseDTO<object>
                {
                    IsSuccess=true,
                    Message="Ge All Subjects",
                    Data=subjectresponse
                };
            }
            catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<ResponseDTO<object>> GetSubjectById(int id)
        {
            try
            {
                var spec = new SubjectSpecification(id);
                var subjects = await _unitOfWork.Repository<Subject>().GetByIdAsync(spec);
                if (subjects == null)
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "No Subjects Found",
                        ErrorCode = ErrorCodes.NotFound
                    };
                var subjectresponse =  new SubjectResponseDTO
                {
                    Id = subjects.Id,
                    SubjectName = subjects.SubjectName,
                    Code = subjects.Code,
                    ClassId = subjects.ClassId,
                    Class = new ClassResponsDTO
                    {
                        AcademicYearId = subjects.Class.AcademicYearId,
                        ClassName = subjects.Class.ClassName,
                        Id = subjects.Class.Id,
                        GradeLevel = subjects.Class.GradeLevel,
                        AcademicYearName = subjects.Class.AcadmicYear.YearName,
                    },
                    AcademicSteff = new AcademicStaffResponse
                    {
                        Id = subjects.AcademicStaff.Id,
                        FullName = subjects.AcademicStaff.User.FullName,
                        Email = subjects.AcademicStaff.User.Email,
                        PhoneNumber = subjects.AcademicStaff.User.PhoneNumber,
                        DepartmentId = subjects.AcademicStaff.DepartmentId,
                        Specialization = subjects.AcademicStaff.Specialization,
                        UserId = subjects.AcademicStaff.UserId,
                        //Department=new DepartmentResponseDTO 
                        //{
                        //    Id=s.AcademicStaff.Department.Id,
                        //    DepartmentName=s.AcademicStaff.Department.DepartmentName,
                        //}
                    },

                };
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Ge All Subjects",
                    Data = subjectresponse
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }
        //Random integer  code Generated 
        private int GenerateRandomCode()
        {
            Random random = new Random();
            return random.Next(1000, 9999); // Generates a random integer between 1000 and 9999
        }
    }
}
