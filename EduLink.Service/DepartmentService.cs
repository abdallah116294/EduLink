using AutoMapper;
using EduLink.Core.Entities;
using EduLink.Core.IRepositories;
using EduLink.Core.IServices;
using EduLink.Core.Specifications;
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

        public async Task<ResponseDTO<object>> AssignAcademicSteffHeadToDepartment(int staffId)
        {
            try
            {
                var academicStaffRepo = _unitOfWork.Repository<AcademicStaff>();
               
                var AcademicSteff = await academicStaffRepo.GetByIdAsync(staffId);
             
                if (AcademicSteff == null )
                {
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "Staff Not Found",
                        ErrorCode = ErrorCodes.NotFound,
                    };
                }
                var sepc = new DepartmentSpecification(AcademicSteff.DepartmentId);
                var departmentRepo = _unitOfWork.Repository<Department>();
                var department = await departmentRepo.GetByIdAsync(sepc);
                if (department == null)
                {
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "Department Not Found",
                        ErrorCode = ErrorCodes.NotFound,
                    };
                }
                department.HeadOfDepartmentId = staffId;
                await departmentRepo.UpdateAsync(department);
                await _unitOfWork.CompleteAsync();
                //var departmentResponseDto = _mapper.Map<DepartmentResponseDTO>(department);
                var departmentResponseDto = new DepartmentResponseDTO
                {
                    Id = department.Id,
                    DepartmentName = department.DepartmentName,
                    HeadOfDepartmentId = department.HeadOfDepartmentId,
                    HeadOfDepartmentName = department.AcademicStaff.FirstOrDefault(s => s.Id == department.HeadOfDepartmentId)!.User.FullName,
                    AcademicStaff = department.AcademicStaff.Select(s => new StaffResponseDto
                    {
                        Id = s.Id,
                        FullName = s.User.FullName,
                        Role = s.User.Role.ToString()
                    }).ToList(),
                };
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Head of Department Assigned Successfully",
                    Data = departmentResponseDto
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

        public async Task<ResponseDTO<object>> AssignNonAcademicSteffHeadToDepartment(int staffId)
        {
            try
            {
                var nonAcademicStaffRepo = _unitOfWork.Repository<NonAcademicStaff>();

                var nonAcademicSteff = await nonAcademicStaffRepo.GetByIdAsync(staffId);

                if (nonAcademicSteff == null)
                {
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "Staff Not Found",
                        ErrorCode = ErrorCodes.NotFound,
                    };
                }
                var spec = new DepartmentSpecification(nonAcademicSteff.DepartmentId);
                var departmentRepo = _unitOfWork.Repository<Department>();
                var department = await departmentRepo.GetByIdAsync(spec);
                if (department == null)
                {
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "Department Not Found",
                        ErrorCode = ErrorCodes.NotFound,
                    };
                }
                department.HeadOfDepartmentId = staffId;
                await departmentRepo.UpdateAsync(department);
                await _unitOfWork.CompleteAsync();
                //var departmentResponseDto = _mapper.Map<DepartmentResponseDTO>(department);
                var departmentResponseDto = new DepartmentResponseDTO
                {
                    Id = department.Id,
                    DepartmentName = department.DepartmentName,
                    HeadOfDepartmentId = department.HeadOfDepartmentId,
                    HeadOfDepartmentName = department.NonAcademicStaff.FirstOrDefault(s => s.Id == department.HeadOfDepartmentId)!.User.FullName,
                    NonAcademicStaff = department.NonAcademicStaff.Select(s => new StaffResponseDto
                    {
                        Id = s.Id,
                        FullName = s.User.FullName,
                        Role = s.User.Role.ToString()
                    }).ToList(),
                };
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Head of Department Assigned Successfully",
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
                var spec=new DepartmentSpecification();
                var departmentRepo = _unitOfWork.Repository<Department>();
                var departments = await departmentRepo.GetAllAsync(spec);
                var departmentResponseDtos = departments.Select(d => new DepartmentResponseDTO
                {
                    Id = d.Id,
                    DepartmentName = d.DepartmentName,
                    HeadOfDepartmentId = d.HeadOfDepartmentId,
                    HeadOfDepartmentName =
    d.AcademicStaff.FirstOrDefault(s => s.Id == d.HeadOfDepartmentId)?.User?.FullName
    ?? d.NonAcademicStaff.FirstOrDefault(s => s.Id == d.HeadOfDepartmentId)?.User?.FullName
    ?? "N/A",
                    AcademicStaff = d.AcademicStaff.Select(s => new StaffResponseDto
                    {
                        Id = s.Id,
                        FullName = s.User.FullName,
                        Role=s.User.Role.ToString()
                    }).ToList(),
                    NonAcademicStaff= d.NonAcademicStaff.Select(s => new StaffResponseDto
                    {
                        Id = s.Id,
                        FullName = s.User.FullName,
                        Role = s.User.Role.ToString()
                    }).ToList(),
                }).ToList();
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
                var spc = new DepartmentSpecification(id);
                var departmentRepo = _unitOfWork.Repository<Department>();
                var department = await departmentRepo.GetByIdAsync(spc);
                if (department == null)
                {
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "Department Not Found",
                        ErrorCode = ErrorCodes.NotFound,
                    };
                }
                var departmentResponseDto = new DepartmentResponseDTO 
                {
                    Id = department.Id,
                    DepartmentName = department.DepartmentName,
                    HeadOfDepartmentId = department.HeadOfDepartmentId,

                                  HeadOfDepartmentName =
                    department.AcademicStaff.FirstOrDefault(s => s.Id == department.HeadOfDepartmentId)?.User?.FullName
    ?? department.NonAcademicStaff.FirstOrDefault(s => s.Id == department.HeadOfDepartmentId)?.User?.FullName
    ?? "N/A",
                    AcademicStaff = department.AcademicStaff.Select(s => new StaffResponseDto
                    {
                        Id = s.Id,
                        FullName = s.User.FullName,
                        Role = s.User.Role.ToString()
                    }).ToList(),
                    NonAcademicStaff = department.NonAcademicStaff.Select(s => new StaffResponseDto
                    {
                        Id = s.Id,
                        FullName = s.User.FullName,
                        Role = s.User.Role.ToString()
                    }).ToList(),
                };
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
