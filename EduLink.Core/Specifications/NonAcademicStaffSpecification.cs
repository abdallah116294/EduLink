using EduLink.Core.Entities;
using EduLink.Core.Specifications.Parames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.Specifications
{
    public class NonAcademicStaffSpecification:BaseSpecifications<NonAcademicStaff>
    {
        public NonAcademicStaffSpecification(NonAcademicSteffParames parames):
            base(non=>(
            (!parames.Id.HasValue||non.Id==parames.Id))
            &&(string.IsNullOrEmpty(parames.UserId)||non.UserId==parames.UserId)
            &&(!parames.DepartmentId.HasValue||non.DepartmentId==parames.DepartmentId))
        {
            AddInclude(non => non.User);
            AddInclude(non => non.Department);
        }
        public NonAcademicStaffSpecification()
        {
            AddInclude(non => non.User);
            AddInclude(non => non.Department);
        }
    }
}
