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
    public class FeesSpecifications:BaseSpecifications<Fee>
    {
        public FeesSpecifications(FeeParames feeParames) :base(f => (!feeParames.StudentId.HasValue||f.StudentId == feeParames.StudentId)&&(!feeParames.Id.HasValue||f.Id==feeParames.Id))
        {
            //Includes.Add(f => f.Student);
            //Includes.Add(f => f.Student.User);
            AddInclude(f=>f.Include(s=>s.Student).ThenInclude(u=>u.User));

        }
        public FeesSpecifications()
        {
            AddInclude(f => f.Include(s => s.Student).ThenInclude(u => u.User));
        }
    }
}
