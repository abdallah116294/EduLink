using EduLink.Core.Specifications;
using EduLink.Utilities.DTO;
using EduLink.Utilities.DTO.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.IServices
{
    public interface IStudentService
    {
        Task<ResponseDTO<Object>> GetAllStudents();
        Task<ResponseDTO<object>> CreateStudent(CreateStudentDTO studentDTO);
        Task<ResponseDTO<object>> GetStudentById(StudentSpecParms studentSpecParms);
        Task<ResponseDTO<object>> GetStudentAttendances(int studentId);
        Task<ResponseDTO<object>> GetStudentProfile(string studentId);
        Task<ResponseDTO<object>> GetStudentGrades(int studentId);
        Task<ResponseDTO<object>>GetStudentFees(int studentId);
        // Task<ResponseDTO<Object>> GetStudentById(string studentId);
    }
}
