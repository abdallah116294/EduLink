using EduLink.Utilities.DTO;
using EduLink.Utilities.DTO.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.IServices
{
    public interface IClassService
    {
        Task<ResponseDTO<object>> CreateClassAsync(CreateClassDTO createClassDTO);
        Task<ResponseDTO<object>> GetClassByIdAsync(int id);
        Task<ResponseDTO<object>> GetAllClassesAsync();
        Task<ResponseDTO<object>> UpdateClassAsync(int id, UpdateClassDTO updateClassDTO);
        Task<ResponseDTO<object>> DeleteClassAsync(int id);
        Task<ResponseDTO<object>>GetStudentInClass(int classId);
        Task<ResponseDTO<object>>GetSubjectsInClass(int classId);

    }
}
