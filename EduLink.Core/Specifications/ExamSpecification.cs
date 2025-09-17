using EduLink.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.Specifications
{
    public class ExamSpecification:BaseSpecifications<Exam>
    {
        public ExamSpecification(int id):base(ex => ex.Id == id)
        {
            AddInclude(ex => ex.Include(ex => ex.Class)
            .ThenInclude(c => c.Students)
            .ThenInclude(s => s.User)
            );
            AddInclude(ex => ex.Include(ex => ex.Class).ThenInclude(c => c.AcadmicYear));


        }
        public ExamSpecification()
        {
            AddInclude(ex => ex.Include(ex=>ex.Class)
            .ThenInclude(c=>c.Students)
            .ThenInclude(s=>s.User)
            );
            AddInclude(ex => ex.Include(ex => ex.Class).ThenInclude(c => c.AcadmicYear));


        }
    }
}
