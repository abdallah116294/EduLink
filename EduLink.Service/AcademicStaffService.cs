using EduLink.Core.Entities;
using EduLink.Core.IRepositories;
using EduLink.Core.IServices;
using EduLink.Core.Specifications;
using EduLink.Core.Specifications.Parames;
using EduLink.Utilities.DTO;
using EduLink.Utilities.DTO.AcademicStaff;
using EduLink.Utilities.DTO.Classes;
using EduLink.Utilities.DTO.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Service
{
    public class AcademicStaffService : IAcademicStaffService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AcademicStaffService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDTO<object>> CreateAcademicStaff(CreateAcademicStaffDTO dto)
        {
            try
            {
                var academicStaffRepo = _unitOfWork.Repository<AcademicStaff>();
                var mappedAcademicStaff = new AcademicStaff 
                {
                      DepartmentId =dto.DepartmentId,
                      Specialization =dto.Specialization,
                       UserId =dto.UserId
                  };
                await academicStaffRepo.AddAsync(mappedAcademicStaff);
                await _unitOfWork.CompleteAsync();
                return new ResponseDTO<object>
                {
                    IsSuccess=true,
                    Message="Add AcademicStaff Succesfull",
                    Data= mappedAcademicStaff,
                };

            }catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accoured ,{ex}",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDTO<object>> DeleteAcademicStaff(int id)
        {
            try
            {
                var spec= new AcademicStaffSpecification(new AcademicStaffParames
                {
                    AcademicStaffId = id
                }); 
                var academicStaff = await _unitOfWork.Repository<AcademicStaff>().GetByIdAsync(spec);
                if(academicStaff==null)
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "AcademicSteff Not Found",
                        ErrorCode = ErrorCodes.NotFound
                    };
                await _unitOfWork.Repository<AcademicStaff>().DeleteAsync(id);
                await _unitOfWork.CompleteAsync();
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Delete AcademicSteff Succesfull",


                };

            }
            catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accoured ,{ex}",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDTO<object>> GetAcademicStaffByDepartment(int departmentId)
        {
            try
            {
                var academicSteffSpec = new AcademicStaffSpecification(new AcademicStaffParames 
                {
                    DepartmentId=departmentId,
                });
                var academicSteff = await _unitOfWork.Repository<AcademicStaff>().GetAllAsync(academicSteffSpec);
                if (academicSteff == null)
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "No academicSteff found in this Department",
                        ErrorCode = ErrorCodes.NotFound
                    };
                var res = academicSteff.Select(ac => new AcademicStaffResponse
                {
                    Id = ac.Id,
                    DepartmentId = ac.DepartmentId,
                    Specialization = ac.Specialization,
                    UserId = ac.UserId,
                    FullName = ac.User.FullName,
                    Email = ac.User.Email,
                    PhoneNumber = ac.User.PhoneNumber,
                    Department = new DepartmentResponseDTO
                    {
                        DepartmentName = ac.Department.DepartmentName,
                        Id = ac.Department.Id,
                    },
                    Subject = ac.Subject.Select(ac => new SubjectResponseDto { SubjectName = ac.SubjectName, Id = ac.Id }).ToList(),
                }).ToList();
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Get All AcademicSteff in Department ",
                    Data = res,

                };
            }
            catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accoured ,{ex}",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDTO<object>> GetAcademicStaffById(int id)
        {
            try
            {
                var academicStaffSpec = new AcademicStaffSpecification(new AcademicStaffParames
                {
                    AcademicStaffId = id
                });
                var academicStaffs = await _unitOfWork.Repository<AcademicStaff>().GetByIdAsync(academicStaffSpec);
              //  Console.WriteLine(academicStaffs.Specialization);
                if(academicStaffs == null)
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "AcadmeicSteff is null",
                        ErrorCode = ErrorCodes.NotFound,
                    };
                var res = new AcademicStaffResponse 
                {
                    Id = academicStaffs.Id,
                    DepartmentId = academicStaffs.DepartmentId,
                    Specialization = academicStaffs.Specialization,
                    UserId = academicStaffs.UserId,
                    FullName = academicStaffs.User.FullName,
                    Email = academicStaffs.User.Email,
                    PhoneNumber = academicStaffs.User.PhoneNumber,
                    Department = new DepartmentResponseDTO
                    {
                        DepartmentName = academicStaffs.Department.DepartmentName,
                        Id = academicStaffs.Department.Id,
                    },
                    Subject = academicStaffs.Subject.Select(ac => new SubjectResponseDto { SubjectName = ac.SubjectName, Id = ac.Id }).ToList(),
                };
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Get AcademicStaff Succesfful",
                    Data = res
                };
            }
            catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accoured ,{ex}",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDTO<object>> GetAcademicStaffByUserId(string userId)
        {
            try
            {
                var academicStaffSpec = new AcademicStaffSpecification(new AcademicStaffParames
                {
                    userId = userId
                });
                var academicStaffs = await _unitOfWork.Repository<AcademicStaff>().GetByIdAsync(academicStaffSpec);
                if (academicStaffs == null)
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "AcadmeicSteff is null",
                        ErrorCode = ErrorCodes.NotFound,
                    };
                var res = new AcademicStaffResponse
                {
                    Id = academicStaffs.Id,
                    DepartmentId = academicStaffs.DepartmentId,
                    Specialization = academicStaffs.Specialization,
                    UserId = academicStaffs.UserId,
                    FullName = academicStaffs.User.FullName,
                    Email = academicStaffs.User.Email,
                    PhoneNumber = academicStaffs.User.PhoneNumber,
                    Department = new DepartmentResponseDTO
                    {
                        DepartmentName = academicStaffs.Department.DepartmentName,
                        Id = academicStaffs.Department.Id,
                    },
                    Subject = academicStaffs.Subject.Select(ac => new SubjectResponseDto { SubjectName = ac.SubjectName, Id = ac.Id }).ToList(),
                };
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Get AcademicStaff Succesfful",
                    Data = res
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accoured ,{ex}",
                    ErrorCode = ErrorCodes.Exception
                };
            }
            }
        public async Task<ResponseDTO<object>> GetAllAcademicStaff()
        {
            try
            {
                var academicStaffSpec = new AcademicStaffSpecification();
                var academicStaffs = await _unitOfWork.Repository<AcademicStaff>().GetAllAsync(academicStaffSpec);
                if (academicStaffs == null)
                    return new ResponseDTO<object> 
                    {
                        IsSuccess=false,
                        Message="AcadmeicSteff is null",
                        ErrorCode=ErrorCodes.NotFound,
                    };
                var res = academicStaffs.Select(ac => new AcademicStaffResponse
                {
                    Id=ac.Id,
                    DepartmentId = ac.DepartmentId,
                    Specialization = ac.Specialization,
                    UserId = ac.UserId,
                    FullName = ac.User.FullName,
                    Email = ac.User.Email,
                    PhoneNumber = ac.User.PhoneNumber,
                    Department = new DepartmentResponseDTO
                    {
                        DepartmentName = ac.Department.DepartmentName,
                        Id = ac.Department.Id,
                    },
                    Subject =ac.Subject.Select(ac=>new SubjectResponseDto { SubjectName=ac.SubjectName,Id=ac.Id}).ToList(),
                }).ToList();
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Get All AcadmeicStaff",
                    Data = res,

                };
            }
            catch(Exception  ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accoured ,{ex}",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDTO<object>> UpdateAcademicStaff(int id, CreateAcademicStaffDTO dto)
        {
            try
            {
                var spec= new AcademicStaffSpecification(new AcademicStaffParames
                {
                    AcademicStaffId = id
                });
                var academicStaff = await _unitOfWork.Repository<AcademicStaff>().GetByIdAsync(spec);
                if (academicStaff == null)
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "AcademicSteff Not Found",
                        ErrorCode = ErrorCodes.NotFound
                    };
                await _unitOfWork.Repository<AcademicStaff>().UpdateAsync(id, entity => 
                {
                    entity.DepartmentId = dto.DepartmentId;
                    entity.Specialization = dto.Specialization;
                    entity.UserId = dto.UserId;             
                });
                await _unitOfWork.CompleteAsync();
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Update AcademicSteff Succesfull",
                };

            }
            catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accoured ,{ex}",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }
    }
}
