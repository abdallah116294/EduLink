using EduLink.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.Specifications
{
    public class ParentSpecification:BaseSpecifications<Parent>
    {
        public ParentSpecification()
        {
         //Simple Includes 
         AddInclude(p => p.User);
         AddInclude(p=>p.Students);
            //Nasted Includes 
            AddInclude(p=>p.Include(p=>p.Students).ThenInclude(s=>s.User));
            AddInclude(q => q.Include(p => p.Students)
                               .ThenInclude(s => s.Class));  // Nested include

        }
        public ParentSpecification(string userId):base(p=>p.UserId== userId)
        {
            AddInclude(p => p.User);
            AddInclude(p => p.Students);
            //Nasted Includes
            AddInclude(q => q.Include(p => p.Students)
                             .ThenInclude(s => s.User));
            AddInclude(q => q.Include(p => p.Students)
                             .ThenInclude(s => s.Class));
        }
    }
}
