using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.Entities
{
    public class Exam:BaseEntity
    {
        public string Name { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }
        public DateTime Date { get; set; }
    }
}
