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
        Task<ResponseDTO<object>> DeleteParent(int id);
        Task<ResponseDTO<object>> GetParent(string usrId);
        Task<ResponseDTO<object>> GetAllParent();
        Task<ResponseDTO<object>> GetStudentByParentId(string userId);
        Task<ResponseDTO<object>> GetParentById(int id);
        Task<ResponseDTO<object>>UpdateParent(string userID,CreateParentDTO dto);
    }
}
