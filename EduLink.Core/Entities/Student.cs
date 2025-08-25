using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.Entities
{
    public class Student:BaseEntity
    {
        public string AdmissionNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime EnrollmentDate { get; set; }
        //Fk for RelationShip 
        public int ParentId { get; set; }
        public Parent Parent { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }
        public ICollection<Attendance> Attendance { get; set; }
        public ICollection<Grade> Grade { get; set; }
        public ICollection<Fee> Fee { get; set; }
    }
}
