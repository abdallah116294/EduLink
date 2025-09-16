using EduLink.Utilities.DTO;
using EduLink.Utilities.DTO.NonAcademicStaff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.IServices
{
    public interface INonAcademicStaffService
    {
        Task<ResponseDTO<object>> AddNonAcademicStaffAsync(CreateNonAcademicSteffDTO dto);
        Task<ResponseDTO<object>> GetAllNonAcademicStaff();
        Task<ResponseDTO<object>> GetNonAcademicSteffById(int id);
        Task<ResponseDTO<object>> GetNonAcademicSteffByUserId(string userId);
        Task<ResponseDTO<object>> GetNonAcademicStaffByDepartment(int departmentId);
    }
}
