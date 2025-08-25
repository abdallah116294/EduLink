using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.Entities
{
    public class AcademicStaff:BaseEntity
    {
        
        public int DepartmentId { get; set; }
        public string Specialization { get; set; }
        public string UserId { get; set; }
        public  User User { get; set; }
        //Department
        public Department Department { get; set; }
        public ICollection<Subject> Subject { get; set; }

    }
}
