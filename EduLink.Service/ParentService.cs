using EduLink.Core.Entities;
using EduLink.Core.IRepositories;
using EduLink.Core.IServices;
using EduLink.Utilities.DTO;
using EduLink.Utilities.DTO.Parent;
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
                    Students = students.Select(s => new StudentResponseDto
                    {
                        Id = s.Id,
                        FullName = s.User.FullName,
                        Grade = s.Grade.Select(g=>g.ExamType).ToList().ToString(),
                    }).ToList()
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

        public Task<ResponseDTO<object>> DeleteParent(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDTO<object>> GetAllParent()
        {
            try
            {
                var parentRepo = _unitOfWork.Repository<Parent>();
                var parents = await parentRepo.GetAllAsync();
                if (parents == null)
                    return new ResponseDTO<object>
                    {
                        IsSuccess = true,
                        Message = "No Parents Found",
                        ErrorCode = ErrorCodes.NotFound,
                    };
               
                //var parentsDTo = parents.Select(parent => new ParentResponseDTO 
                //{
                //    Id = parent.Id,
                //    Occupation = parent.Occupation,
                //    Address = parent.Address,
                //    UserId = parent.UserId,
                //    FullName = parent.User.FullName,
                //    Email = parent.User.Email,
                //    PhoneNumber =parent.User.PhoneNumber ,

                //});
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Get All Parents",
                    Data = parents
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

        public Task<ResponseDTO<object>> GetParent(string usrId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO<object>> UpdateParent(UpdateParentDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
