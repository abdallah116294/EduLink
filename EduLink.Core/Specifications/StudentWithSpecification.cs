using EduLink.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.Specifications
{
    public class StudentWithSpecification:BaseSpecifications<Student>
    {
        public StudentWithSpecification(StudentSpecParms studentSpecParms)
            :base(s=>(!studentSpecParms.StudentId.HasValue||s.Id==studentSpecParms.StudentId)&&(!studentSpecParms.ClassId.HasValue||s.ClassId==studentSpecParms.ClassId))
        {

            Includes.Add(s => s.Parent);
            Includes.Add(s => s.Parent.User);
            Includes.Add(s => s.User);
            Includes.Add(s => s.Class);
            Includes.Add(s => s.Attendance);
            Includes.Add(s => s.Grade);
            Includes.Add(s => s.Fee);
        }
        public StudentWithSpecification()
        {

            Includes.Add(s => s.Parent);
            Includes.Add(s => s.Parent.User);
            Includes.Add(s => s.User);
            Includes.Add(s => s.Class);
            Includes.Add(s => s.Attendance);
            Includes.Add(s => s.Grade);
            Includes.Add(s => s.Fee);
        }
    }
}
