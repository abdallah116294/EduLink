using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.Entities
{
    public class Department:BaseEntity
    {
        public string DepartmentName { get; set; }
        public int HeadOfDepartmentId { get; set; }
        public ICollection<AcademicStaff> AcademicStaff { get; set; }
        public ICollection<NonAcademicStaff> NonAcademicStaff { get; set; }
    }
}
