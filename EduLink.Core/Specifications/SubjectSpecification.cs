using EduLink.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.Specifications
{
    public class SubjectSpecification:BaseSpecifications<Subject>
    {
        public SubjectSpecification(int id):base(s=>s.Id==id)
        {
            AddInclude(s => s.Include(s => s.Class).ThenInclude(c => c.AcadmicYear));
            AddInclude(s => s.Include(s => s.AcademicStaff)
            .ThenInclude(ac => ac.User)

            );
        }
        public SubjectSpecification()
        {
            AddInclude(s => s.Include(s=>s.Class).ThenInclude(c=>c.AcadmicYear));
            AddInclude(s => s.Include(s=>s.AcademicStaff)
            .ThenInclude(ac=>ac.User)
       
            );
        }
    }
}
