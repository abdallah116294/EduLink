using EduLink.Core.Entities;
using EduLink.Core.IRepositories;
using EduLink.Core.IServices;
using EduLink.Core.Specifications;
using EduLink.Core.Specifications.Parames;
using EduLink.Utilities.DTO;
using EduLink.Utilities.DTO.Classes;
using EduLink.Utilities.DTO.Grade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Service
{
    public class GradeService : IGradeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GradeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDTO<object>> AddGrade(CreateGradeDTO dto)
        {
            try
            {
               
                var  gradeRepo = _unitOfWork.Repository<Grade>();
                var newGrade = new Grade
                {
                    StudentId = dto.StudentId,
                    Score = dto.Score,
                    ExamType = dto.ExamType,
                    Date = dto.Date
                };
                await gradeRepo.AddAsync(newGrade);
                await _unitOfWork.CompleteAsync();
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Grade added successfully.",
                    Data = newGrade,
                };

            }
            catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An error occurred while adding the grade. {ex.Message}",
                    Data = null,
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDTO<object>> DeleteGrade(int id)
        {
            try
            {
                var spec = new GradeSpecification(new GradeParames 
                { 
                    Id=id
                });
                var grade = await _unitOfWork.Repository<Grade>().GetByIdAsync(spec);
                if(grade == null)
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "Grade Not Found",
                        ErrorCode = ErrorCodes.NotFound
                    };
               await _unitOfWork.Repository<Grade>().DeleteAsync(id);
                await _unitOfWork.CompleteAsync();
                 return new ResponseDTO<object>
                 {
                      IsSuccess = true,
                      Message = "Grade Deleted Successfully",
                 };

            }
            catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accured {ex.Message}",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDTO<object>> GetAllGrades()
        {
            try
            {
                var spec = new GradeSpecification();
                var Grades = await _unitOfWork.Repository<Grade>().GetAllAsync(spec);
                if (Grades == null)
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "No Grades Found",
                        ErrorCode = ErrorCodes.NotFound
                    };
                var gradeResponse = Grades.Select(g => new GradeResponsDTO 
                { 
                    Id=g.Id,
                    StudentId=g.StudentId,
                    Date = g.Date,
                    ExamType=g.ExamType,
                   Score=g.Score,
                   Student=new StudentResponseDto 
                   { 
                       FullName=g.Student.User.FullName,
                       Id=g.Student.Id
                   }
                }).ToList();
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Get All Grades",
                    Data = gradeResponse,

                };
            }catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accured {ex.Message}",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDTO<object>> GetGradeById(int id)
        {
            try
            {
                var sp = new GradeSpecification(new GradeParames 
                {
                    Id=id
                });
                var grade = await _unitOfWork.Repository<Grade>().GetByIdAsync(sp);
                if(grade == null)
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "Grade Not Found",
                        ErrorCode = ErrorCodes.NotFound
                    };
                var gradeResponse = new GradeResponsDTO
                {
                    Id = grade.Id,
                    StudentId = grade.StudentId,
                    Date = grade.Date,
                    ExamType = grade.ExamType,
                    Score = grade.Score,
                    Student = new StudentResponseDto
                    {
                        FullName = grade.Student.User.FullName,
                        Id = grade.Student.Id
                    }
                };
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Get Grade By Id",
                    Data = gradeResponse,
                };
            }
            catch(Exception ex)
            {
                               return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accured {ex.Message}",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDTO<object>> UpdateGrade(int id, CreateGradeDTO dto)
        {
            try
            {
                var spec = new GradeSpecification(new GradeParames {
                    Id=id
                });
                var grade = await _unitOfWork.Repository<Grade>().GetByIdAsync(spec);
                if (grade == null)
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "No Grade Found",
                        ErrorCode = ErrorCodes.NotFound
                    };
                await _unitOfWork.Repository<Grade>().UpdateAsync(id, entity =>
                {
                    entity.Id = id;
                    entity.StudentId= dto.StudentId;
                    entity.Score= dto.Score;
                    entity.Date = DateTime.Now;
                    entity.ExamType=dto.ExamType;
                    
                });
                await _unitOfWork.CompleteAsync();
                var newGrade = await _unitOfWork.Repository<Grade>().GetByIdAsync(spec);
                var gradResponse = new GradeResponsDTO
                {
                    Id = newGrade.Id,
                    ExamType = newGrade.ExamType,
                    Score =newGrade.Score,
                    Date = newGrade.Date,
                    StudentId = newGrade.StudentId,
                    Student = new StudentResponseDto
                    {
                        FullName = newGrade.Student.User.FullName,
                        Id = newGrade.Student.Id
                    }
                };
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Update Grade Succesful",
                    Data = newGrade,
                };
            }catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accured {ex.Message}",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }
    }
}
