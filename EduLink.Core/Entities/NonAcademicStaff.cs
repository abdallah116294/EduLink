using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.Entities
{
    public class NonAcademicStaff:BaseEntity
    {
        //Department
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public string  JobTitle { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
