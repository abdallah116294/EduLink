using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Utilities.DTO.AcademicYear
{
    public class AcademicYearResponseDTO
    {
        public int Id { get; set; }
        public string YearName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Return classes as nested DTOs
        public ICollection<ClassResponseDTO> Classes { get; set; }
    }
}
