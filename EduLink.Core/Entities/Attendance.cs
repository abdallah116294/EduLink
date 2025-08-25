using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.Entities
{
    public class Attendance:BaseEntity
    {
        //Student 
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
}
