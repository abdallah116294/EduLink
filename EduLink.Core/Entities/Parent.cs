using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.Entities
{ 
    public class Parent:BaseEntity
    {
        public string Occupation { get; set; }
        public String Address { get; set; }
        //RelationShip if can multiple student 
        public  string UserId { get; set; }
        public User  User { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
