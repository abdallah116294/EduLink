using EduLink.Core.Entities;
using EduLink.Core.Specifications.Parames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.Specifications
{
    public class AcademicYearSpecification:BaseSpecifications<AcadmicYear>
    {
        public AcademicYearSpecification(AcademicYearParames parames):base(ac =>
            (!parames.Id.HasValue || ac.Id == parames.Id) &&
            (!parames.today.HasValue || (ac.StartDate <= parames.today.Value && ac.EndDate >= parames.today.Value)))
        {
            AddInclude(ac => ac.Class);
        }
        public AcademicYearSpecification()
        {
            AddInclude(ac=>ac.Class);
        }
    }
}
