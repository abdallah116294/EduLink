using EduLink.Utilities.DTO;
using EduLink.Utilities.DTO.Parent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.IServices
{
    public  interface IParentService
    {
        Task<ResponseDTO<object>> CreateParent(CreateParentDTO dto);
        Task<ResponseDTO<object>> UpdateParent(UpdateParentDTO dto);
        Task<ResponseDTO<object>> DeleteParent(int id);
        Task<ResponseDTO<object>> GetParent(string usrId);
        Task<ResponseDTO<object>> GetAllParent();
    }
}
