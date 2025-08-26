using EduLink.Utilities.DTO;
using EduLink.Utilities.DTO.AcademicYear;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.IServices
{
    public interface IAcademicYearService
    {
        Task<ResponseDTO<object>> CreateAcademicYear(CreateAcademicYearDTO dto);
        Task<ResponseDTO<object>> UpdateAcademicYear(UpdateAcademicYearDTO dto);
        Task<ResponseDTO<object>> UpdateAcademicYear(int id, UpdateAcademicYearDTO dto);
        Task<ResponseDTO<object>> DeleteAcademicYear(int id);
        Task<ResponseDTO<object>> GetAcademicYear(int id);
        Task<ResponseDTO<object>> GetAllAcademicYears();
    }
}
