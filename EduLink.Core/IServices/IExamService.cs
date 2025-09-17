using EduLink.Utilities.DTO;
using EduLink.Utilities.DTO.Exam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.IServices
{
    public interface IExamService
    {
        Task<ResponseDTO<object>> GetAllExam();
        Task<ResponseDTO<object>>GetExamById(int examId);
        Task<ResponseDTO<object>> CreateExam(AddExamDTO examDto);
        Task<ResponseDTO<object>> UpdateExam(int examId, AddExamDTO examDto);
        Task<ResponseDTO<object>>DeleteExame(int examId);
        

    }
}
