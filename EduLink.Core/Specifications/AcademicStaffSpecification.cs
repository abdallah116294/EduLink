using EduLink.Core.Entities;
using EduLink.Core.Specifications.Parames;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.Specifications
{
    public class AcademicStaffSpecification:BaseSpecifications<AcademicStaff>
    {
        public AcademicStaffSpecification(AcademicStaffParames parames):base(ac=>(!parames.AcademicStaffId.HasValue || ac.Id==parames.AcademicStaffId) &&
        (string.IsNullOrEmpty(parames.userId) || ac.UserId==parames.userId)
        &&(!parames.DepartmentId.HasValue||ac.DepartmentId==parames.DepartmentId)
        )
        {
            AddInclude(ac => ac.User);
            AddInclude(ac => ac.Department);
            AddInclude(ac => ac.Subject);
        }
        public AcademicStaffSpecification()
        {
            AddInclude(ac => ac.User);
            AddInclude(ac => ac.Department);
            AddInclude(ac => ac.Subject);
        }
    }
}
