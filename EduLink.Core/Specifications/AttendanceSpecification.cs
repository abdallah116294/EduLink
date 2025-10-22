using EduLink.Core.Entities;
using EduLink.Core.Specifications.Parames;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.Specifications
{
    public class AttendanceSpecification:BaseSpecifications<Attendance>
    {
        public AttendanceSpecification(AttendanceParames attendance):base(a=>(!attendance.Id.HasValue||a.Id==attendance.Id)&&(!attendance.StudentId.HasValue||a.StudentId==attendance.StudentId))
        {
            AddInclude(a=>a.Include(a=>a.Student).ThenInclude(s=>s.User));
            AddInclude(a => a.Include(a => a.Student).ThenInclude(s => s.Parent).ThenInclude(p=>p.User));
            //Includes.Add(a => a.Student);
            //Includes.Add(a => a.Student.User);
        }
        public AttendanceSpecification()
        {
            AddInclude(a => a.Include(a => a.Student).ThenInclude(s => s.User));
        }
    }
}
