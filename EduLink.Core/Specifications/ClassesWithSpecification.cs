using EduLink.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.Specifications
{
    public class ClassesWithSpecification: BaseSpecifications<Class>
    {
        public ClassesWithSpecification(int id):base(
            c => c.Id == id
            )
        {
            Includes.Add(c => c.AcadmicYear);
            Includes.Add(c => c.Students);
            Includes.Add(c => c.Exam);
            Includes.Add(c => c.Subject);
        }

        public ClassesWithSpecification()
        {
                Includes.Add(c => c.AcadmicYear);
                Includes.Add(c => c.Students);
                Includes.Add(c => c.Exam);
                Includes.Add(c => c.Subject);
        }
    }
}
