using AutoMapper;
using EduLink.Core.Entities;
using EduLink.Core.IRepositories;
using EduLink.Core.IServices;
using EduLink.Core.Specifications;
using EduLink.Utilities.DTO;
using EduLink.Utilities.DTO.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Service
{
    public class ClassService : IClassService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClassService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDTO<object>> CreateClassAsync(CreateClassDTO createClassDTO)
        {
            try
            {
                var classRepository = _unitOfWork.Repository<Class>();
                var classEntity = _mapper.Map<Class>(createClassDTO);
                await classRepository.AddAsync(classEntity);
                await _unitOfWork.CompleteAsync();
                var classResponseDTO = _mapper.Map<ClassResponsDTO>(classEntity);
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Class created successfully",
                    Data = classResponseDTO
                };

            }
            catch(Exception ex)
            {
              return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"Error creating class: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<ResponseDTO<object>> DeleteClassAsync(int id)
        {
            try
            {
                var classReep = _unitOfWork.Repository<Class>();
                var classRe =await classReep.GetByIdAsync(id);
                if (classRe == null)
                    return new ResponseDTO<object>
                    {
                        IsSuccess=false,
                        Message="No Class with ID Foud",
                        ErrorCode=ErrorCodes.NotFound
                    };
                await classReep.DeleteAsync(id);
                await _unitOfWork.CompleteAsync();
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Delete Class Succesful"
                };

            }catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accoured{ex.Message}",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDTO<object>> GetAllClassesAsync()
        {
            try
            {
                var classSpec = new ClassesWithSpecification();
                var classRepo = _unitOfWork.Repository<Class>();
                var classes = await classRepo.GetAllAsync(classSpec);
                if (classes == null)
                    return new ResponseDTO<object>
                    {
                        IsSuccess = true,
                        Message = "No Classes Found",
                        Data =new List<object>()
                    };
                
                var result = classes.Select(c => new ClassResponsDTO
                {
                    ClassName = c.ClassName,
                    AcademicYearId = c.AcademicYearId,
                    AcademicYearName = c.AcadmicYear.YearName,
                    Id = c.Id,
                    GradeLevel = c.GradeLevel,
                    Students = c.Students.Select(c => new StudentResponseDto {
                        FullName=c.AdmissionNumber,
                        Id = c.Id
                    }).ToList(),
                    Subjects=c.Subject.Select(c=>new SubjectResponseDto
                    {
                        Id=c.Id,
                        SubjectName=c.SubjectName
                    }).ToList(),
                    Exams=c.Exam.Select(e=>new ExamResponseDto 
                    {
                        Id=e.Id,
                        ExamName=e.Name,
                        ExamDate=e.Date,
                    }).ToList()

                }).ToList();
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Get All Classes",
                    Data = result,

                };

            }catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accoured{ex.Message}",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDTO<object>> GetClassByIdAsync(int id)
        {
            try
            {
                var classSpec = new ClassesWithSpecification(id);
                var classRepo = _unitOfWork.Repository<Class>();
                var classReq = await classRepo.GetByIdAsync(classSpec);
                if (classReq == null)
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "Not Class found with this ID",
                        ErrorCode=ErrorCodes.NotFound
                    };
                // var mappedClass = _mapper.Map<ClassResponsDTO>(classReq);
                var result = new ClassResponsDTO 
                {
                    Id=classReq.Id,
                    ClassName=classReq.ClassName,
                    AcademicYearId=classReq.AcademicYearId,
                    AcademicYearName=classReq.AcadmicYear.YearName,
                    GradeLevel= classReq.GradeLevel,
                    Subjects=classReq.Subject.Select(classReq=>new SubjectResponseDto
                    {
                        Id=classReq.Id,
                        SubjectName=classReq.SubjectName
                    }).ToList(),
                    Students=classReq.Students.Select(classReq=>new StudentResponseDto 
                    {
                        Id=classReq.Id,
                        FullName=classReq.AdmissionNumber,
                    }).ToList(),
                    Exams=classReq.Exam.Select(classReq=>new ExamResponseDto 
                    {
                        Id=classReq.Id,
                        ExamName=classReq.Name,
                        ExamDate=classReq.Date,
                    }).ToList()
                };
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Get Class With Id Succesful",
                    Data = result
                };

            }catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accoured{ex.Message}",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDTO<object>> UpdateClassAsync(int id, UpdateClassDTO updateClassDTO)
        {
            try
            {
                var classRepo = _unitOfWork.Repository<Class>();
                await classRepo.UpdateAsync(id, entity => 
                {
                    if (!string.IsNullOrEmpty(updateClassDTO.ClassName)) entity.ClassName = updateClassDTO.ClassName;
                    if (!string.IsNullOrEmpty(updateClassDTO.GradeLevel)) entity.GradeLevel = updateClassDTO.GradeLevel;
                    if (updateClassDTO.AcademicYearId == null) entity.AcademicYearId = updateClassDTO.AcademicYearId;
                });
                await _unitOfWork.CompleteAsync();
                var classreq =await classRepo.GetByIdAsync(id);
                var mappedClass = _mapper.Map<ClassResponsDTO>(classreq);
                return new ResponseDTO<object>
                {
                    IsSuccess=true,
                    Message="Update Class Succesful",
                    Data=mappedClass
                };
            }catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accoured{ex.Message}",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }
    }
}
