using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Utilities.DTO.Parent
{
    public class CreateParentDTO
    {
        public string Occupation { get; set; }
        public string Address { get; set; }

        // Link to existing User (Identity UserId)
        public string UserId { get; set; }
        public ICollection<int> StudentIds { get; set; }
    }
}
