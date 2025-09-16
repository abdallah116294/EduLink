using EduLink.Core.Entities;
using EduLink.Utilities.DTO.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edulink.Tests.TestData
{
    public static class DepartmentTestData
    {
        public static Department GetValidDepartment()
        {
            return new Department
            {
                Id = 1,
                DepartmentName = "Computer Science",
                HeadOfDepartmentId = 1,
                AcademicStaff = new List<AcademicStaff>(),
                NonAcademicStaff = new List<NonAcademicStaff>()
            };

        }
        public static CreateDepartmentDTO GetValidCreateDepartmentDTO()
        {
            return new CreateDepartmentDTO
            {
                DepartmentName = "Computer Science",
                AcademicStaffIds = new List<int> { 1, 2 },
                NonAcademicStaffIds = new List<int> { 3, 4 },
                HeadOfDepartmentId = 1
            };
        }
        public static UpdateDepartmentDTO GetValidUpdateDepartmentDTO()
        {
            return new UpdateDepartmentDTO
            {
                DepartmentName = "Updated Computer Science",
                AcademicStaffIds = new List<int> { 1, 2 },
                NonAcademicStaffIds = new List<int> { 3, 4 },
                HeadOfDepartmentId = 1
            };
        }
        public static List<Department> GetDepartmentsList()
        {
            return new List<Department>
        {
            new Department { 
                Id = 1,
                DepartmentName = "Computer Science",
                HeadOfDepartmentId = 1,
                AcademicStaff = new List<AcademicStaff>(),
                NonAcademicStaff = new List<NonAcademicStaff>()
            },
            new Department {
                Id = 2, 
                DepartmentName = "Mathematics", 
                HeadOfDepartmentId = 2,
                AcademicStaff = new List<AcademicStaff>(),
                NonAcademicStaff = new List<NonAcademicStaff>()
            },
            new Department {
                Id = 3,
                DepartmentName = "Physics", 
                HeadOfDepartmentId = 3,
                AcademicStaff = new List<AcademicStaff>(),
                NonAcademicStaff = new List<NonAcademicStaff>()
            }
        };
        }
    }
}
