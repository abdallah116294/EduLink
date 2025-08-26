using AutoMapper;
using EduLink.Core.Entities;
using EduLink.Core.IRepositories;
using EduLink.Core.IServices;
using EduLink.Utilities.DTO;
using EduLink.Utilities.DTO.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDTO<object>> CreateDepartment(CreateDepartmentDTO dto)
        {
            try
            {
                var departmentRepo = _unitOfWork.Repository<Department>();
                var department = _mapper.Map<Department>(dto);
                await departmentRepo.AddAsync(department);
                await _unitOfWork.CompleteAsync();
                var departmentResponseDto = _mapper.Map<DepartmentResponseDTO>(department);
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Department Created Successfully",
                    Data = departmentResponseDto
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

        public async Task<ResponseDTO<object>> DeleteDepartment(int id)
        {
            try
            {
                var departmentRepo = _unitOfWork.Repository<Department>();
                var department = await departmentRepo.GetByIdAsync(id);
                if (department == null)
                {
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "Department Not Found",
                        ErrorCode = ErrorCodes.NotFound,
                    };
                }
                await departmentRepo.DeleteAsync(id);
                await _unitOfWork.CompleteAsync();
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Department Deleted Successfully",
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

        public async Task<ResponseDTO<object>> GetAllDepartments()
        {
            try
            {
                var departmentRepo = _unitOfWork.Repository<Department>();
                var departments = await departmentRepo.GetAllAsync();
                var departmentResponseDtos = _mapper.Map<List<DepartmentResponseDTO>>(departments);
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Departments Retrieved Successfully",
                    Data = departmentResponseDtos
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

        public async Task<ResponseDTO<object>> GetDepartment(int id)
        {
            try
            {
                var departmentRepo = _unitOfWork.Repository<Department>();
                var department = await departmentRepo.GetByIdAsync(id);
                if (department == null)
                {
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "Department Not Found",
                        ErrorCode = ErrorCodes.NotFound,
                    };
                }
                var departmentResponseDto = _mapper.Map<DepartmentResponseDTO>(department);
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Department Retrieved Successfully",
                    Data = departmentResponseDto
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

        public async Task<ResponseDTO<object>> UpdateDepartment(int id, UpdateDepartmentDTO dto)
        {
            try
            {
                var departmentRepo = _unitOfWork.Repository<Department>();
                var department = await departmentRepo.GetByIdAsync(id);
                if (department == null)
                {
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "Department Not Found",
                        ErrorCode = ErrorCodes.NotFound,
                    };
                }
                await departmentRepo.UpdateAsync(id, entity => 
                {
                  if (!string.IsNullOrEmpty(dto.DepartmentName)) entity.DepartmentName = dto.DepartmentName;
                  if (dto.HeadOfDepartmentId !=0) entity.HeadOfDepartmentId = dto.HeadOfDepartmentId;

                });
                await _unitOfWork.CompleteAsync();
                var updatedDepartment = await departmentRepo.GetByIdAsync(id);
                var departmentResponseDto = _mapper.Map<DepartmentResponseDTO>(updatedDepartment);
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Department Updated Successfully",
                    Data = departmentResponseDto
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
