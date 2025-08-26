using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Utilities.DTO.AcademicYear
{
    public class CreateAcademicYearDTO
    {
        public string YearName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Optionally link existing classes by Id
        public ICollection<int> ClassIds { get; set; }
    }
}
