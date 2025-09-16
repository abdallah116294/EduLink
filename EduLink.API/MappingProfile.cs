using AutoMapper;
using EduLink.Core.Entities;
using EduLink.Utilities.DTO.AcademicYear;
using EduLink.Utilities.DTO.Classes;
using EduLink.Utilities.DTO.Department;
using EduLink.Utilities.DTO.Student;

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
           //Class
           CreateMap<Class,CreateClassDTO>().ReverseMap();
           CreateMap<Class,ClassResponsDTO>().ReverseMap();
           CreateMap<UpdateClassDTO,Class>().ReverseMap();
            //Student 
            CreateMap<CreateStudentDTO, Student>().ReverseMap();
            CreateMap<Student, StudentResponseDto>().ReverseMap();

        }
    }
}
