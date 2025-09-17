using EduLink.Core.Entities;
using EduLink.Core.IRepositories;
using EduLink.Core.IServices;
using EduLink.Core.Specifications;
using EduLink.Core.Specifications.Parames;
using EduLink.Utilities.DTO;
using EduLink.Utilities.DTO.Parent;
using EduLink.Utilities.DTO.Student;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Service
{
    public class ParentService : IParentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public ParentService(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<ResponseDTO<object>> CreateParent(CreateParentDTO dto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(dto.UserId);

                //var userRepo=_unitOfWork.Repository<User>();
                //var users = await userRepo.GetAllAsync();
                //var user =  users.Where(u=>u.Id==dto.UserId);
                var parentRepo = _unitOfWork.Repository<Parent>();
                var studentRepo= _unitOfWork.Repository<Student>();
                var students = new List<Student>();
                if (dto.StudentIds != null && dto.StudentIds.Any())
                {
                    var studenList = await studentRepo.GetAllAsync();
                    students=studenList.Where(s=>dto.StudentIds.Contains(s.Id)).ToList();
                }

                var parent = new Parent
                {
                    Occupation = dto.Occupation,
                    Address = dto.Occupation,
                    UserId = dto.UserId,
                    Students = students
                };
                await parentRepo.AddAsync(parent);
                await _unitOfWork.CompleteAsync();
                var parentReponseDto = new ParentResponseDTO
                {
                    Id = parent.Id,
                    Occupation = parent.Occupation,
                    Address = parent.Address,
                    UserId = parent.UserId,
                    FullName = user.FullName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    //Students = students.Select(s => new StudentResponseDto
                    //{
                    //    Id = s.Id,
                    //    FullName = s.User.FullName,
                    //    Grade = s.Grade.Select(g=>g.ExamType).ToList().ToString(),
                    //}).ToList()
                };

                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Add Parent Succesful",
                    Data = parentReponseDto,
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

        public async Task<ResponseDTO<object>> DeleteParent(int id)
        {
            try
            {
                var parent=await _unitOfWork.Repository<Parent>().GetByIdAsync(id);
                if(parent==null)
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "No Parent Found",
                        ErrorCode = ErrorCodes.NotFound,
                    };
              await  _unitOfWork.Repository<Parent>().DeleteAsync(id);
               await _unitOfWork.CompleteAsync();
                    return new ResponseDTO<object>
                    {
                        IsSuccess = true,
                        Message = "Delete Parent Succesful",
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

        public async Task<ResponseDTO<object>> GetAllParent()
        {
            try
            {
                var parentSpec = new ParentSpecification();
                var parentRepo = _unitOfWork.Repository<Parent>();
                var parents = await parentRepo.GetAllAsync(parentSpec);
                if (parents == null)
                    return new ResponseDTO<object>
                    {
                        IsSuccess = true,
                        Message = "No Parents Found",
                        ErrorCode = ErrorCodes.NotFound,
                    };
                var Student = parents.FirstOrDefault().Students.FirstOrDefault().ClassId;
                Console.WriteLine($"Clsss ID of Stuednt:{Student}");
                var parentsDTo = parents.Select(parent => new ParentResponseDTO
                {
                    Id = parent.Id,
                    Occupation = parent.Occupation,
                    Address = parent.Address,
                    UserId = parent.UserId,
                    FullName = parent.User.FullName,
                    Email = parent.User.Email,
                    PhoneNumber = parent.User.PhoneNumber,
                    Students = parent.Students.Select(p => new StudentResponseDTO
                    {
                        UserId = p.UserId,
                        AdmissionNumber = p.AdmissionNumber,
                        ClassId = p.ClassId,
                        ClassName = p.Class.ClassName,
                        Email = p.User.Email,
                        DateOfBirth = p.DateOfBirth,
                        EnrollmentDate = p.EnrollmentDate,
                        FullName = p.User.FullName,
                        Id = p.Id,
                        ParentId = p.ParentId,
                        ParentName = parent.User.FullName
                    }).ToList()


                });
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Get All Parents",
                    Data = parentsDTo
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

        public async Task<ResponseDTO<object>> GetParent(string usrId)
        {
            try
            {
                var parentSpec = new ParentSpecification(new ParentParames
                {
                    UserId=usrId
                });
                var parent = await _unitOfWork.Repository<Parent>().GetByIdAsync(parentSpec);
                if (parent == null)
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "Not Parent Found",
                        ErrorCode = ErrorCodes.NotFound,
                    };
                var parentDTO = new ParentResponseDTO
                {
                    Id = parent.Id,
                    Occupation = parent.Occupation,
                    Address = parent.Address,
                    UserId = usrId,
                    FullName = parent.User.FullName,
                    Email = parent.User.Email,
                    PhoneNumber = parent.User.PhoneNumber,
                    Students = parent.Students.Select(p => new StudentResponseDTO
                    {
                        UserId = p.UserId,
                        AdmissionNumber = p.AdmissionNumber,
                        ClassId = p.ClassId,
                        ClassName = p.Class.ClassName,
                        Email = p.User.Email,
                        DateOfBirth = p.DateOfBirth,
                        EnrollmentDate = p.EnrollmentDate,
                        FullName = p.User.FullName,
                        Id = p.Id,
                        ParentId = p.ParentId,
                        ParentName = parent.User.FullName
                    }).ToList()
                };
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Get Parent Succesful",
                    Data = parentDTO,
                };
            }
            catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accured{ex.Message}",
                    ErrorCode = ErrorCodes.Exception,
                };
            }
        }

        public async Task<ResponseDTO<object>> GetParentById(int id)
        {
            try
            {
                var spec = new ParentSpecification(new ParentParames
                {
                    Id= id
                });
                var parent = await _unitOfWork.Repository<Parent>().GetByIdAsync(spec);
                if (parent == null)
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "No Parent Found",
                        ErrorCode = ErrorCodes.NotFound,
                    };
                var parentDTO = new ParentResponseDTO
                {
                    Id = parent.Id,
                    Occupation = parent.Occupation,
                    Address = parent.Address,
                    UserId = parent.UserId,
                    FullName = parent.User.FullName,
                    Email = parent.User.Email,
                    PhoneNumber = parent.User.PhoneNumber,
                    Students = parent.Students.Select(p => new StudentResponseDTO
                    {
                        UserId = p.UserId,
                        AdmissionNumber = p.AdmissionNumber,
                        ClassId = p.ClassId,
                        ClassName = p.Class.ClassName,
                        Email = p.User.Email,
                        DateOfBirth = p.DateOfBirth,
                        EnrollmentDate = p.EnrollmentDate,
                        FullName = p.User.FullName,
                        Id = p.Id,
                        ParentId = p.ParentId,
                        ParentName = parent.User.FullName
                    }).ToList()
                };
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Get Parent By Id",
                    Data = parentDTO,

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

        public async Task<ResponseDTO<object>> GetStudentByParentId(string userId)
        {
            try
            {
                var parentSpec = new ParentSpecification(new ParentParames
                {
                    UserId=userId
                });
                var parent = await _unitOfWork.Repository<Parent>().GetByIdAsync(parentSpec);
                if (parent == null)
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "No Parent Found",
                        ErrorCode=ErrorCodes.NotFound,
                    };
                var students = parent.Students.Select(p => new StudentResponseDTO {
                    UserId = p.UserId,
                    AdmissionNumber = p.AdmissionNumber,
                    ClassId = p.ClassId,
                    ClassName = p.Class.ClassName,
                    Email = p.User.Email,
                    DateOfBirth = p.DateOfBirth,
                    EnrollmentDate = p.EnrollmentDate,
                    FullName = p.User.FullName,
                    Id = p.Id,
                    ParentId = p.ParentId,
                    ParentName = parent.User.FullName

                }).ToList();
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Get Students of Parent Successful",
                    Data = students,
                };
            }catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accured{ex.Message}",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }
        public async Task<ResponseDTO<object>> UpdateParent(string userID, CreateParentDTO dto)
        {
            try
            {
                var spec = new ParentSpecification(new ParentParames
                {
                    UserId=userID
                });
                var parent = await _unitOfWork.Repository<Parent>().GetByIdAsync(spec);
                if (parent == null)
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "No Parent Found",
                        ErrorCode = ErrorCodes.NotFound,
                    };
                await _unitOfWork.Repository<Parent>().UpdateAsync(parent.Id, entity => 
                {
                    entity.Occupation = dto.Occupation;
                    entity.Address = dto.Address;
                });
                await _unitOfWork.CompleteAsync();
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Update Parent",
                };
            }
            catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accured{ex.Message}",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }
    }
}
