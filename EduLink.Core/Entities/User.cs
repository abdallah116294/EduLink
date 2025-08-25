using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.Entities
{
    public class User:IdentityUser
    {
        // Custom Properties 
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public UserRole Role { get; set; }
        //Data for time Of create and when  Update 
        public DateTime CreateAt { get; set; }= DateTime.UtcNow;
        public DateTime? UpdateAt { get; set; }
        //Navigation Property base on Role 
        //Student
        public Student Student { get; set; }
        //Academic steff 
        public AcademicStaff AcademicStaff { get; set; }
        // Non-Academic Steff
        public  NonAcademicStaff NonAcademicStaff { get; set; }
        // for Parents - can have multiple childern
        public Parent Parent { get; set; }
  

    }
}
