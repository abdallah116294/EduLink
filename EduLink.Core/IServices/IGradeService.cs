using EduLink.Utilities.DTO;
using EduLink.Utilities.DTO.Grade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.IServices
{
    public interface IGradeService
    {
        Task<ResponseDTO<object>> AddGrade(CreateGradeDTO dto);
        Task<ResponseDTO<object>> GetAllGrades();
        Task<ResponseDTO<object>> GetGradeById(int id);
        Task<ResponseDTO<object>> UpdateGrade(int id, CreateGradeDTO dto);
        Task<ResponseDTO<object>> DeleteGrade(int id);

    }
}
