using AutoMapper;
using EduLink.Core.Entities;
using EduLink.Utilities.DTO;
using EduLink.Utilities.DTO.Department;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edulink.Tests.Helpers
{
    public static class TestHelpers
    {
        public static ResponseDTO<object> CreateSuccessResponse(object data = null, string message = "Success")
        {
            return new ResponseDTO<object>
            {
                IsSuccess = true,
                Message = message,
                Data = data
            };
        }
        public static ResponseDTO<object> CreateErrorResponse(string message = "Error occurred")
        {
            return new ResponseDTO<object>
            {
                IsSuccess = false,
                Message = message,
                Data = null
            };
        }
        public static Mock<IMapper> CreateMapperMock()
        {
            var mockMapper = new Mock<IMapper>();

            // Setup common mappings
            mockMapper.Setup(m => m.Map<Department>(It.IsAny<CreateDepartmentDTO>()))
                      .Returns((CreateDepartmentDTO dto) => new Department
                      {
                          DepartmentName = dto.DepartmentName,
                            HeadOfDepartmentId = dto.HeadOfDepartmentId,
                            AcademicStaff = new List<AcademicStaff>(), // In real scenario, you'd fetch these based on IDs
                            NonAcademicStaff = new List<NonAcademicStaff>() // In real scenario, you'd fetch these based on IDs

                      });

            mockMapper.Setup(m => m.Map(It.IsAny<UpdateDepartmentDTO>(), It.IsAny<Department>()))
                      .Returns((UpdateDepartmentDTO dto, Department dept) =>
                      {
                          dept.DepartmentName = dto.DepartmentName;
                            dept.HeadOfDepartmentId = dto.HeadOfDepartmentId;
                          // In real scenario, you'd update AcademicStaff and NonAcademicStaff based on IDs
                          dept.AcademicStaff = new List<AcademicStaff>();
                         dept.NonAcademicStaff = new List<NonAcademicStaff>();
                          return dept;
                      });

            return mockMapper;
        }
    }
}
