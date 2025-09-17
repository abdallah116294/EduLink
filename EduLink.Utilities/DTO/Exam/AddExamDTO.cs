using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Utilities.DTO.Exam
{
    public class AddExamDTO
    {
        public string Name { get; set; }
        public int ClassId { get; set; }
        public DateTime Date { get; set; }
    }
}
