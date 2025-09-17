using EduLink.Utilities.DTO.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Utilities.DTO.Exam
{
    public class ExamResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ClassId { get; set; }
        public ClassResponsDTO Class { get; set; }
        public DateTime Date { get; set; }
    }

}
