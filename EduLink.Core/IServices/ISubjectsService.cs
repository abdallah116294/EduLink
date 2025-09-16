using EduLink.Utilities.DTO;
using EduLink.Utilities.DTO.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.IServices
{
    public interface ISubjectsService
    {
        Task<ResponseDTO<object>> GetAllSubject();
        Task<ResponseDTO<object>> GetSubjectById(int id);
        Task<ResponseDTO<object>> CreateSubject(CreateSubjectDTO dto);
      //  Task<ResponseDTO<object>> UpdateSubject(int id, UpdateSubjectDTO dto);
        Task<ResponseDTO<object>> DeleteSubject(int id);
        Task<ResponseDTO<object>>AssignTeacherToSubject(int subjectId, int teacherId);


    }
}
