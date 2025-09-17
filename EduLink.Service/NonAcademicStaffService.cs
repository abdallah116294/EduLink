using EduLink.Core.Entities;
using EduLink.Core.IRepositories;
using EduLink.Core.IServices;
using EduLink.Core.Specifications;
using EduLink.Core.Specifications.Parames;
using EduLink.Utilities.DTO;
using EduLink.Utilities.DTO.NonAcademicStaff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Service
{
    public class NonAcademicStaffService : INonAcademicStaffService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NonAcademicStaffService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDTO<object>> AddNonAcademicStaffAsync(CreateNonAcademicSteffDTO dto)
        {
            try
            {
                var mappedDTO = new NonAcademicStaff 
                {
                    DepartmentId = dto.DepartmentId,
                    JobTitle = dto.JobTitle,
                    UserId = dto.UserId
                };
                await _unitOfWork.Repository<NonAcademicStaff>().AddAsync(mappedDTO);
                await _unitOfWork.CompleteAsync();
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Non Academic Staff Added Successfully",
                    Data = mappedDTO,
                };
            }
            catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accured{ex.Message}",
                    Data = null,
                    ErrorCode=ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDTO<object>> DeleteNonAcademicStaff(int id)
        {
            try
            {
                var spec = new NonAcademicStaffSpecification(new NonAcademicSteffParames 
                {
                    Id=id
                });
                var nonAcademicSteff = await _unitOfWork.Repository<NonAcademicStaff>().GetByIdAsync(spec);
                if(nonAcademicSteff == null)
                {
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "Non Academic Staff not found",
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                await _unitOfWork.Repository<NonAcademicStaff>().DeleteAsync(id);
                await _unitOfWork.CompleteAsync();
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Delete Non Academic Staff",
                };
            }
            catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accured{ex.Message}",
                    Data = null,
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDTO<object>> GetAllNonAcademicStaff()
        {
            try
            {
                var spec = new NonAcademicStaffSpecification();
                var nonAcademicStaffs = await _unitOfWork.Repository<NonAcademicStaff>().GetAllAsync(spec);
                if (nonAcademicStaffs == null)
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "Non Academic Steff Added yet",
                        ErrorCode=ErrorCodes.NotFound
                    };
                var response = nonAcademicStaffs.Select(non => new NonAcademicResponse 
                {
                    Id=non.Id,
                   DepartmentId=non.DepartmentId,
                   DepartmentName=non.Department.DepartmentName,
                  JobTitle=non.JobTitle,
                  UserId=non.UserId,
                  FullName=non.User.FullName,
                  Email=non.User.Email,
                  PhoneNumber=non.User.PhoneNumber,
                }).ToList();
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Get All NonAcademicStaff ",
                    Data = response,
                };
            }
            catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accured{ex.Message}",
                    Data = null,
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDTO<object>> GetNonAcademicStaffByDepartment(int departmentId)
        {
            try
            {
                var spec = new NonAcademicStaffSpecification(new NonAcademicSteffParames
                {
                    DepartmentId = departmentId
                });
                var nonAcademicSteff = await _unitOfWork.Repository<NonAcademicStaff>().GetByIdAsync(spec);
                if (nonAcademicSteff == null)
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "Not Found Academic Steff by this ID",
                        ErrorCode = ErrorCodes.NotFound,
                    };
                var response = new NonAcademicResponse
                {
                    Id = nonAcademicSteff.Id,
                    DepartmentId = nonAcademicSteff.DepartmentId,
                    DepartmentName = nonAcademicSteff.Department.DepartmentName,
                    JobTitle = nonAcademicSteff.JobTitle,
                    UserId = nonAcademicSteff.UserId,
                    FullName = nonAcademicSteff.User.FullName,
                    Email = nonAcademicSteff.User.Email,
                    PhoneNumber = nonAcademicSteff.User.PhoneNumber,
                };
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Get NonAcademic Stafff By Id",
                    Data = response,

                };

            }
            catch (Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accured{ex.Message}",
                    Data = null,
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDTO<object>> GetNonAcademicSteffById(int id)
        {
            try
            {
                var spec = new NonAcademicStaffSpecification(new NonAcademicSteffParames 
                {
                    Id=id
                });
                var nonAcademicSteff = await _unitOfWork.Repository<NonAcademicStaff>().GetByIdAsync(spec);
                if (nonAcademicSteff == null)
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "Not Found Academic Steff by this ID",
                        ErrorCode = ErrorCodes.NotFound,
                    };
                var response = new NonAcademicResponse
                {
                    Id = nonAcademicSteff.Id,
                    DepartmentId = nonAcademicSteff.DepartmentId,
                    DepartmentName = nonAcademicSteff.Department.DepartmentName,
                    JobTitle = nonAcademicSteff.JobTitle,
                    UserId = nonAcademicSteff.UserId,
                    FullName = nonAcademicSteff.User.FullName,
                    Email = nonAcademicSteff.User.Email,
                    PhoneNumber = nonAcademicSteff.User.PhoneNumber,
                };
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Get NonAcademic Stafff By Id",
                    Data = response,

                };

            }catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accured{ex.Message}",
                    Data = null,
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDTO<object>> GetNonAcademicSteffByUserId(string userId)
        {
            try
            {
                var spec = new NonAcademicStaffSpecification(new NonAcademicSteffParames
                {
                    UserId = userId
                });
                var nonAcademicSteff = await _unitOfWork.Repository<NonAcademicStaff>().GetByIdAsync(spec);
                if (nonAcademicSteff == null)
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "Not Found Academic Steff by this ID",
                        ErrorCode = ErrorCodes.NotFound,
                    };
                var response = new NonAcademicResponse
                {
                    Id = nonAcademicSteff.Id,
                    DepartmentId = nonAcademicSteff.DepartmentId,
                    DepartmentName = nonAcademicSteff.Department.DepartmentName,
                    JobTitle = nonAcademicSteff.JobTitle,
                    UserId = nonAcademicSteff.UserId,
                    FullName = nonAcademicSteff.User.FullName,
                    Email = nonAcademicSteff.User.Email,
                    PhoneNumber = nonAcademicSteff.User.PhoneNumber,
                };
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Get NonAcademic Stafff By Id",
                    Data = response,

                };

            }
            catch (Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accured{ex.Message}",
                    Data = null,
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDTO<object>> UpdateNonAcademicStaff(int id, CreateNonAcademicSteffDTO dto)
        {
            try
            {
                var spec = new NonAcademicStaffSpecification(new NonAcademicSteffParames
                {
                    Id = id
                });
                var nonAcademicSteff = await _unitOfWork.Repository<NonAcademicStaff>().GetByIdAsync(spec);
                if (nonAcademicSteff == null)
                {
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "Non Academic Staff not found",
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                nonAcademicSteff.DepartmentId = dto.DepartmentId;
                nonAcademicSteff.JobTitle = dto.JobTitle;
                nonAcademicSteff.UserId = dto.UserId;
               await _unitOfWork.Repository<NonAcademicStaff>().UpdateAsync(id, entity =>
               {
                    entity.DepartmentId=dto.DepartmentId;
                   entity.JobTitle=dto.JobTitle;
               });
                await _unitOfWork.CompleteAsync();
                var updatedSpec = new NonAcademicStaffSpecification(new NonAcademicSteffParames
                {
                    Id = id
                });
                nonAcademicSteff = await _unitOfWork.Repository<NonAcademicStaff>().GetByIdAsync(updatedSpec);
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Non Academic Staff Updated Successfully",
                    Data = nonAcademicSteff,
                };

            }
            catch (Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accured{ex.Message}",
                    Data = null,
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }
    }
}
