using EduLink.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.Specifications
{
    public class FeesSpecifications:BaseSpecifications<Fee>
    {
        public FeesSpecifications(int studentId):base(f => f.StudentId == studentId)
        {
            Includes.Add(f => f.Student);
            Includes.Add(f => f.Student.User);

        }
    }
}
