using EduLink.Core.Specifications;
using EduLink.Utilities.DTO;
using EduLink.Utilities.DTO.Attendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.IServices
{
    public interface IAttendanceService
    {
        Task<ResponseDTO<object>> MarkAttendanceAsync(AddAttendanceDTO dto );
        Task<ResponseDTO<object>> GetStudentsAttanceByClass(StudentSpecParms studentSpecParms);
    }
}
