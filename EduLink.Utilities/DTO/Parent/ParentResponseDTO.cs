using EduLink.Utilities.DTO.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Utilities.DTO.Parent
{
    public class ParentResponseDTO
    {
        public int Id { get; set; } // BaseEntity uses int now
        public string Occupation { get; set; }
        public string Address { get; set; }

        // Linked User info
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<StudentResponseDTO> Students { get; set; }
    }
}
