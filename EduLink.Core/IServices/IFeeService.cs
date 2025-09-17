using EduLink.Utilities.DTO;
using EduLink.Utilities.DTO.Fee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.IServices
{
    public interface IFeeService
    {
        Task<ResponseDTO<object>> AddFeeAsync(AddFeeDTO model);
        Task<ResponseDTO<object>> GetFeeByIdAsync(int id);
        Task<ResponseDTO<object>> GetAllFeesAsync();
        Task<ResponseDTO<object>> UpdateFeeAsync(int id, AddFeeDTO model);
        Task<ResponseDTO<object>> DeleteFeeAsync(int id);
        Task<ResponseDTO<object>> MarkFeeAsPaidAsync(int id);


    }
}
