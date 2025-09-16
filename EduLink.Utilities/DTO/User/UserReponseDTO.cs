using EduLink.Utilities.DTO.Parent;
using EduLink.Utilities.DTO.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Utilities.DTO.User
{
    public class UserReponseDTO
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        //Data for time Of create and when  Update 
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdateAt { get; set; }
        public StudentResponseDTO Student { get; set; }
        public ParentResponseDTO Parent { get; set; }
    }
}
