using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Utilities.DTO.User
{
    public class AdminResponsDTO
    {
        public string Id { get; set; }
        public string FullName  { get; set; }
        public string  PhoneNumber { get; set; }
        public string  Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
