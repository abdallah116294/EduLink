using AutoMapper;
using EduLink.Core.Entities;
using EduLink.Core.IRepositories;
using EduLink.Core.IServices;
using EduLink.Core.Specifications;
using EduLink.Core.Specifications.Parames;
using EduLink.Utilities.DTO;
using EduLink.Utilities.DTO.AcademicYear;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Service
{
    public class AcademicYearService : IAcademicYearService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AcademicYearService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDTO<object>> CreateAcademicYear(CreateAcademicYearDTO dto)
        {
            try
            {
                var academicYearRepo = _unitOfWork.Repository<AcadmicYear>();
                var academicYear = _mapper.Map<AcadmicYear>(dto);
                await academicYearRepo.AddAsync(academicYear);
                await _unitOfWork.CompleteAsync();
                var academicYearResponseDto = _mapper.Map<AcademicYearResponseDTO>(academicYear);
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Academic Year Created Successfully",
                    Data = academicYearResponseDto
                };

            }
            catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accured {ex}",
                    ErrorCode = ErrorCodes.Exception,
                };
            }
        }

        public async Task<ResponseDTO<object>> DeleteAcademicYear(int id)
        {
            try
            {
                var academicYearRepo = _unitOfWork.Repository<AcadmicYear>();
                var academicYear =await academicYearRepo.GetByIdAsync(id);
                if (academicYear == null)
                {
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "Academic Year Not Found",
                        ErrorCode = ErrorCodes.NotFound,
                    };
                }
                 await  academicYearRepo.DeleteAsync(id);
                 await   _unitOfWork.CompleteAsync();
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Academic Year Deleted Successfully",
                };
            }
            catch(Exception ex)
            { 
            return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accured {ex}",
                    ErrorCode = ErrorCodes.Exception,
                };
            }
        }

        public async Task<ResponseDTO<object>> GetAcademicYear(int id)
        {
            try
            {
                var spec = new AcademicYearSpecification(new AcademicYearParames
                {
                    Id = id
                });
                var academicYearRepo = _unitOfWork.Repository<AcadmicYear>();
                var academicYear = await academicYearRepo.GetByIdAsync(spec);
                if (academicYear == null)
                {
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "Academic Year Not Found",
                        ErrorCode = ErrorCodes.NotFound,
                    };
                }
                var academicYearResponseDtos = new AcademicYearResponseDTO
                {
                    Id = academicYear.Id,
                    YearName = academicYear.YearName,
                    StartDate = academicYear.StartDate,
                    EndDate = academicYear.EndDate,
                    Classes = academicYear.Class.Select(cl => new ClassResponseDTO
                    {
                        Id = cl.Id,
                        ClassName = cl.ClassName,
                        //GradeLevel=cl.GradeLevel
                    }).ToList()
                };
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Academic Year Retrieved Successfully",
                    Data = academicYearResponseDtos
                };

            }
            catch(Exception ex)
            {
              return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accured {ex}",
                    ErrorCode = ErrorCodes.Exception,
                };
            }
        }

        public async Task<ResponseDTO<object>> GetAllAcademicYears()
        {
            try
            {
                var spec = new AcademicYearSpecification();
                var academicYearRepo = _unitOfWork.Repository<AcadmicYear>();
                var academicYears = await academicYearRepo.GetAllAsync(spec);
                if (academicYears == null || !academicYears.Any())
                {
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "No Academic Years Found",
                        ErrorCode = ErrorCodes.NotFound,
                    };
                }
                var academicYearResponseDtos = academicYears.Select(ac => new AcademicYearResponseDTO
                {
                    Id = ac.Id,
                    YearName = ac.YearName,
                    StartDate = ac.StartDate,
                    EndDate = ac.EndDate,
                    Classes = ac.Class.Select(cl => new ClassResponseDTO {
                        Id=cl.Id,
                        ClassName=cl.ClassName,
                        //GradeLevel=cl.GradeLevel
                    }).ToList()
                }).ToList();
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Academic Years Retrieved Successfully",
                    Data = academicYearResponseDtos
                };
            }
            catch(Exception ex)
            {
            return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accured {ex}",
                    ErrorCode = ErrorCodes.Exception,
                };
            }
        }

        public async Task<ResponseDTO<object>> GetCurrentAcademicYear()
        {
            try
            {
                var today = DateTime.Now;
                var spec = new AcademicYearSpecification(new AcademicYearParames 
                {
                    today= today
                });
             
                var academicYear = await _unitOfWork.Repository<AcadmicYear>()
                  .GetByIdAsync(spec);
                if (academicYear == null)
                {
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "Academic Year Not Found",
                        ErrorCode = ErrorCodes.NotFound,
                    };
                }
                var academicYearResponseDtos = new AcademicYearResponseDTO
                {
                    Id = academicYear.Id,
                    YearName = academicYear.YearName,
                    StartDate = academicYear.StartDate,
                    EndDate = academicYear.EndDate,
                    Classes = academicYear.Class.Select(cl => new ClassResponseDTO
                    {
                        Id = cl.Id,
                        ClassName = cl.ClassName,
                        //GradeLevel=cl.GradeLevel
                    }).ToList()
                };
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Academic Year Retrieved Successfully",
                    Data = academicYearResponseDtos
                };
            }
            catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accured {ex}",
                    ErrorCode = ErrorCodes.Exception,
                };
            }
        }

        public async Task<ResponseDTO<object>> UpdateAcademicYear(UpdateAcademicYearDTO dto)
        {
            try
            {
                var academicYearRepo = _unitOfWork.Repository<AcadmicYear>();
                var academicYear = await academicYearRepo.GetByIdAsync(dto.Id);
                if (academicYear == null)
                {
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "Academic Year Not Found",
                        ErrorCode = ErrorCodes.NotFound,
                    };
                }
                academicYear.YearName = dto.YearName;
                academicYear.StartDate = dto.StartDate;
                academicYear.EndDate = dto.EndDate;
                await   academicYearRepo.UpdateAsync(academicYear);
                await _unitOfWork.CompleteAsync();
                var academicYearResponseDto = _mapper.Map<AcademicYearResponseDTO>(academicYear);
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Academic Year Updated Successfully",
                    Data = academicYearResponseDto
                };

            }
            catch(Exception ex)
            {
             return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accured {ex}",
                    ErrorCode = ErrorCodes.Exception,
                };
            }
        }

        public async Task<ResponseDTO<object>> UpdateAcademicYear(int id, UpdateAcademicYearDTO dto)
        {
            try
            {
                var academicYearRepo = _unitOfWork.Repository<AcadmicYear>();
                var academicYear = await academicYearRepo.GetByIdAsync(id);
                if (academicYear == null)
                {
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "Academic Year Not Found",
                        ErrorCode = ErrorCodes.NotFound,
                    };
                }
                await academicYearRepo.UpdateAsync(id, entity => 
                {
                    if (!string.IsNullOrEmpty(dto.YearName))
                        entity.YearName = dto.YearName;
                    if (dto.StartDate != default)
                        entity.StartDate = dto.StartDate;
                    if (dto.EndDate != default)
                        entity.EndDate = dto.EndDate;
                    foreach (var classIds in dto.ClassIds)
                    {
                        dto.ClassIds.Add(classIds);
                    }
                });
                await _unitOfWork.CompleteAsync();
                var academicYearResponseDto = _mapper.Map<AcademicYearResponseDTO>(academicYear);
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Academic Year Updated Successfully",
                    Data = academicYearResponseDto
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accured {ex}",
                    ErrorCode = ErrorCodes.Exception,
                };
            }
        }
    }
}
