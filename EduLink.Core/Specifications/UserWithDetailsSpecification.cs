using EduLink.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.Specifications
{
    public class UserWithDetailsSpecification:BaseSpecifications<User>
    {
        public UserWithDetailsSpecification(string id):base(u=>u.Id==id)
        {
            Includes.Add(u => u.Student);
            Includes.Add(u => u.Student.Parent);
            Includes.Add(u => u.Student.Class);
            //Includes.Add(u => u.Student.User);
           // Includes.Add(u => u.Parent);
            //Includes.Add(u => u.Parent.User);

        }
    }
}
