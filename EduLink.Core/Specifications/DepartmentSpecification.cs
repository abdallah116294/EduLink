using EduLink.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.Specifications
{
    public class DepartmentSpecification:BaseSpecifications<Department>
    {
        public DepartmentSpecification(int id ):base(d=>d.Id==id)
        {
            //InstedInclude
            AddInclude(d=>d.Include(d=>d.AcademicStaff).ThenInclude(d=>d.User));
            AddInclude(d => d.Include(d => d.NonAcademicStaff).ThenInclude(d => d.User));
            //AddInclude(d => d.AcademicStaff);
            //AddInclude(d => d.NonAcademicStaff);
        }
        public DepartmentSpecification() 
        {
            AddInclude(d => d.Include(d => d.AcademicStaff).ThenInclude(d => d.User));
            AddInclude(d => d.Include(d => d.NonAcademicStaff).ThenInclude(d => d.User));
            //  AddInclude(d => d.AcademicStaff).ThenInclude();
            //AddInclude(d => d.NonAcademicStaff);
        }
    }
}
