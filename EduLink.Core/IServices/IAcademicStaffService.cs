using EduLink.Utilities.DTO;
using EduLink.Utilities.DTO.AcademicStaff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.IServices
{
    public interface  IAcademicStaffService
    {
        Task<ResponseDTO<object>> GetAllAcademicStaff();
        Task<ResponseDTO<object>> GetAcademicStaffById(int id);
        Task<ResponseDTO<object>> GetAcademicStaffByUserId(string userId);
        Task<ResponseDTO<object>> CreateAcademicStaff(CreateAcademicStaffDTO dto);
        Task<ResponseDTO<object>> GetAcademicStaffByDepartment(int departmentId);
    }
}
