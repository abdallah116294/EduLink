using EduLink.Core.Entities;
using Microsoft.EntityFrameworkCore;
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
            AddInclude(c => c.AcadmicYear);
           
            AddInclude(c=>c.Include(c=>c.Students)
                             .ThenInclude(s=>s.User));
            AddInclude(c=>c.Include(c=>c.Students)
                             .ThenInclude(s=>s.Parent)
                                .ThenInclude(p=>p.User)
                             );
            AddInclude(c => c.Exam);
           AddInclude(c => c.Subject);
        }

        public ClassesWithSpecification()
        {
            AddInclude(c => c.AcadmicYear);

            AddInclude(c => c.Include(c => c.Students)
                             .ThenInclude(s => s.User));
            AddInclude(c => c.Include(c => c.Students)
                             .ThenInclude(s => s.Parent)
                                .ThenInclude(p => p.User)
                             );
            AddInclude(c => c.Exam);
            AddInclude(c => c.Subject);
        }
    }
}
