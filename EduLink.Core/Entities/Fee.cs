using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.Entities
{
    public class Fee:BaseEntity
    {
        //Student relationShip
        public int  StudentId { get; set; }
        public Student Student { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
    }
}
