using EduLink.Core.Entities;
using EduLink.Core.Specifications.Parames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.Specifications
{
    public class AdminSpecification:BaseSpecifications<User>
    {
        public AdminSpecification(AdminParames adminParames)
            :base(ad=>(ad.Role==adminParames.Role)&&(string.IsNullOrEmpty(adminParames.Id) || ad.Id == adminParames.Id))
        {
            
        }
    }
}
