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
    public class GradeSpecification:BaseSpecifications<Grade>
    {
        public GradeSpecification(GradeParames  gradeParames):base(g => (!gradeParames.Id.HasValue||g.Id==gradeParames.Id)&&(!gradeParames.StudentId.HasValue||g.StudentId==gradeParames.StudentId))
        {
           AddInclude(g => g.Include(g=>g.Student).ThenInclude(s=>s.User));
          // AddInclude(g => g.Student.User);
        }
        public GradeSpecification()
        {
            AddInclude(g => g.Include(g => g.Student).ThenInclude(s => s.User));
            // AddInclude(g => g.Student.User);
        }
    }
}
