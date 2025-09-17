using EduLink.Core.Entities;
using EduLink.Core.IRepositories;
using EduLink.Core.IServices;
using EduLink.Core.Specifications;
using EduLink.Utilities.DTO;
using EduLink.Utilities.DTO.Classes;
using EduLink.Utilities.DTO.Exam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Service
{
    public class ExamService : IExamService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExamService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDTO<object>> CreateExam(AddExamDTO examDto)
        {
            try
            {
                var exam = new Exam 
                {
                    ClassId = examDto.ClassId,
                    Name= examDto.Name,
                    Date= examDto.Date,
                    
                    
                };
                await _unitOfWork.Repository<Exam>().AddAsync(exam);
                await _unitOfWork.CompleteAsync();
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Exam created successfully",
                    Data = exam,
                };

            }
            catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Data = null,
                    ErrorCode=ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDTO<object>> DeleteExame(int examId)
        {
            try
            {
                var spec = new ExamSpecification(examId);
                var exam = await _unitOfWork.Repository<Exam>().GetByIdAsync(spec);
                if (exam == null)
                {
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "Exam not found",
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                await _unitOfWork.Repository<Exam>().DeleteAsync(examId);
                await _unitOfWork.CompleteAsync();
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Delete Exam",
                };
            }
            catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Data = null,
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDTO<object>> GetAllExam()
        {
            try
            {
                var spec= new ExamSpecification();
                var exams = await _unitOfWork.Repository<Exam>().GetAllAsync(spec);
              
                var examrespons= exams.Select(exams => new ExamResponseDTO
                {
                    Id=exams.Id,
                   Name= exams.Name,
                   Date =exams.Date,
                Class  =new ClassResponsDTO
                   {
                    Id=exams.Class.Id,
                   ClassName= exams.Class.ClassName,    
                   AcademicYearId= exams.Class.AcademicYearId,
                     AcademicYearName= exams.Class.AcadmicYear.YearName,
                     GradeLevel= exams.Class.GradeLevel,
                        Students= exams.Class.Students.Select(s => new StudentResponseDto
                        {
                            Id= s.Id,
                            FullName= s.User.FullName,
                        }).ToList(),

                }
            
            }).ToList();
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Exams retrieved successfully",
                    Data = new { Total = examrespons.Count, Exams = examrespons }
                };
            }
            catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Data = null,
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async  Task<ResponseDTO<object>> GetExamById(int examId)
        {
            try
            {
                var spec= new ExamSpecification(examId);
                var exam = await _unitOfWork.Repository<Exam>().GetByIdAsync(spec);
                if (exam == null)
                {
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "Exam not found",
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var examrespons = new ExamResponseDTO
                {
                    Id = exam.Id,
                    Name = exam.Name,
                    Date = exam.Date,
                    Class = new ClassResponsDTO
                    {
                        Id = exam.Class.Id,
                        ClassName = exam.Class.ClassName,
                        AcademicYearId = exam.Class.AcademicYearId,
                        AcademicYearName = exam.Class.AcadmicYear.YearName,
                        GradeLevel = exam.Class.GradeLevel,
                        Students = exam.Class.Students.Select(s => new StudentResponseDto
                        {
                            Id = s.Id,
                            FullName = s.User.FullName,
                        }).ToList(),
                    }
                };
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Exam retrieved successfully",
                    Data = examrespons
                };

            }
            catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Data = null,
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDTO<object>> UpdateExam(int examId, AddExamDTO examDto)
        {
            try
            {
                var spec = new ExamSpecification(examId);
                var exam = await _unitOfWork.Repository<Exam>().GetByIdAsync(spec);
                if (exam == null)
                {
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "Exam not found",
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                await _unitOfWork.Repository<Exam>().UpdateAsync(examId, entity => 
                {
                    entity.Name = examDto.Name;
                    entity.ClassId = examDto.ClassId;
                    entity.Date = DateTime.Now;
                });
                await _unitOfWork.CompleteAsync();
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Update Exame Succesfull",
                };

            }
            catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Data = null,
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }
    }
}
