using EduLink.Utilities.DTO;
using EduLink.Utilities.DTO.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.IServices
{
    public interface IDepartmentService
    {
        Task<ResponseDTO<object>> CreateDepartment(CreateDepartmentDTO dto);
        Task<ResponseDTO<object>> UpdateDepartment(int id, UpdateDepartmentDTO dto);
        Task<ResponseDTO<object>> DeleteDepartment(int id);
        Task<ResponseDTO<object>> GetDepartment(int id);
        Task<ResponseDTO<object>> GetAllDepartments();
    }
}
