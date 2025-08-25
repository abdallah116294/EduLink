using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.Entities
{
    public class Grade:BaseEntity
    {
        //Student Id 
        public int  StudentId { get; set; }
        public Student Student { get; set; }
        public decimal Score { get; set; }
        public string ExamType { get; set; }
        public DateTime Date { get; set; }
    }
}
