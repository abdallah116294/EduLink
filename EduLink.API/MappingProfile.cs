using AutoMapper;
using EduLink.Core.Entities;
using EduLink.Utilities.DTO.AcademicYear;
using EduLink.Utilities.DTO.Department;

namespace EduLink.API
{
    public class MappingProfile:Profile
    {
        public MappingProfile() 
        {
            //Acadmic Year Mappings
            CreateMap<AcadmicYear, AcademicYearResponseDTO>().ReverseMap();
            CreateMap<AcademicYearResponseDTO, AcadmicYear>().ReverseMap();
            CreateMap<CreateAcademicYearDTO, AcadmicYear>().ReverseMap();
            CreateMap<UpdateAcademicYearDTO, AcadmicYear>().ReverseMap();
            //Departement Mappings
            CreateMap<Department, DepartmentResponseDTO>().ReverseMap();
            CreateMap<DepartmentResponseDTO, Department>().ReverseMap();
            CreateMap<CreateDepartmentDTO, Department>().ReverseMap();
            CreateMap<UpdateDepartmentDTO, Department>().ReverseMap();

        }
    }
}
