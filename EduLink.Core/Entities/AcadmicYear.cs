using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.Entities
{
    public class AcadmicYear:BaseEntity
    {
        public string YearName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        //Classes
        public ICollection<Class> Class { get; set; }
    }
}
