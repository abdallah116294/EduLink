using EduLink.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.Specifications
{
    public class AttendanceSpecification:BaseSpecifications<Attendance>
    {
        public AttendanceSpecification(int studentId):base(a=>a.StudentId==studentId)
        {
            Includes.Add(a => a.Student);
            Includes.Add(a => a.Student.User);
        }
    }
}
