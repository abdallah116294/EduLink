using EduLink.Core.Entities;
using EduLink.Core.IRepositories;
using EduLink.Core.IServices;
using EduLink.Core.Specifications;
using EduLink.Core.Specifications.Parames;
using EduLink.Utilities.DTO;
using EduLink.Utilities.DTO.Classes;
using EduLink.Utilities.DTO.Fee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Service
{
    public class FeeService : IFeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDTO<object>> AddFeeAsync(AddFeeDTO model)
        {
            try
            {
                var fee = new Fee
                {
                    StudentId = model.StudentId,
                    Amount = model.Amount,
                    DueDate = model.DueDate,
                    Status = model.Status
                };
                await _unitOfWork.Repository<Fee>().AddAsync(fee);
                await _unitOfWork.CompleteAsync();
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Fee added successfully",

                };

            }
            catch (Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An error occurred while adding the fee: {ex.Message}",
                    Data = null,
                    ErrorCode = ErrorCodes.Exception

                };
            }

        }

        public async Task<ResponseDTO<object>> DeleteFeeAsync(int id)
        {
            try
            {
                var spec = new FeesSpecifications(new FeeParames 
                {
                    Id=id
                });
                var fee = await _unitOfWork.Repository<Fee>().GetByIdAsync(spec);
                if (fee == null)
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "No Fee Found",
                        ErrorCode = ErrorCodes.NotFound
                    };
                await _unitOfWork.Repository<Fee>().DeleteAsync(id);
                await _unitOfWork.CompleteAsync();
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Delete Fee",
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

        public async Task<ResponseDTO<object>> GetAllFeesAsync()
        {
            try
            {
                var spec = new FeesSpecifications();
                var fees = await _unitOfWork.Repository<Fee>().GetAllAsync(spec);
                if (fees == null || !fees.Any())
                {
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "No fees found",
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var feeList = fees.Select(f => new FeeResponseDTO
                {
                    Amount = f.Amount,
                    DueDate = f.DueDate,
                    Status = f.Status,
                    StudentId = f.StudentId,
                    Student = new StudentResponseDto
                    {
                        FullName= f.Student.User.FullName,
                        Id= f.Student.Id
                    }
                }
                    ).ToList();
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Fees retrieved successfully",
                    Data = feeList
                };

            }
            catch(Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An error occurred while retrieving fees: {ex.Message}",
                    Data = null,
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDTO<object>> GetFeeByIdAsync(int id)
        {
            try
            {
                var spec = new FeesSpecifications(new FeeParames { Id = id });
                var fees = await _unitOfWork.Repository<Fee>().GetByIdAsync(spec);
                if (fees == null)
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "No Feed Found",
                        ErrorCode = ErrorCodes.NotFound
                    };
                var feesResponse = new FeeResponseDTO
                {
                    Id=fees.Id,
                    Amount=fees.Amount,
                    DueDate=fees.DueDate,
                    Status=fees.Status,
                    Student= new StudentResponseDto
                    {
                        Id=fees.Student.Id,
                        FullName=fees.Student.User.FullName,
                    },
                    StudentId = fees.StudentId
                };
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Get Fees",
                    Data = feesResponse,

                };
            }catch (Exception ex)
            {
                return new ResponseDTO<object>
                {
                    IsSuccess = false,
                    Message = $"An Error Accured{ex.Message}",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDTO<object>> MarkFeeAsPaidAsync(int id)
        {
            try
            {
                var spec = new FeesSpecifications(new FeeParames 
                {
                    Id=id
                });
                var fee = await _unitOfWork.Repository<Fee>().GetByIdAsync(spec);
                if (fee == null)
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "No Fee Found",
                        ErrorCode = ErrorCodes.NotFound
                    };
                await _unitOfWork.Repository<Fee>().UpdateAsync(id, entity =>
                {
                    entity.Status = "Paid";
                });
                await _unitOfWork.CompleteAsync();
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Marked Paid",

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

        public async Task<ResponseDTO<object>> UpdateFeeAsync(int id, AddFeeDTO model)
        {
            try
            {
                var spec = new FeesSpecifications(new FeeParames { Id = id });
                var fee = await _unitOfWork.Repository<Fee>().GetByIdAsync(spec);
                if (fee == null)
                    return new ResponseDTO<object>
                    {
                        IsSuccess = false,
                        Message = "No Fee Found",
                        ErrorCode = ErrorCodes.NotFound
                    };
                await _unitOfWork.Repository<Fee>().UpdateAsync(id, entity => 
                {
                    entity.DueDate = model.DueDate;
                    entity.Status=model.Status;
                    entity.StudentId = model.StudentId;
                    entity.Amount = model.Amount;
                });
                await _unitOfWork.CompleteAsync();
                return new ResponseDTO<object>
                {
                    IsSuccess = true,
                    Message = "Update Fee"
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
    }
}
