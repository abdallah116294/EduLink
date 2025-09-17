using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Utilities.DTO.Grade
{
    public  class CreateGradeDTO
    {
        public int StudentId { get; set; }
        public decimal Score { get; set; }
        public string ExamType { get; set; }
        public DateTime Date { get; set; }
    }
}
