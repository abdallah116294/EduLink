using EduLink.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.Specifications
{
    public class GradeSpecification:BaseSpecifications<Grade>
    {
        public GradeSpecification(int studentId):base(g => g.StudentId == studentId)
        {
           Includes.Add(g => g.Student);
           Includes.Add(g => g.Student.User);
        }
      
    }
}
