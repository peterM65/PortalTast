using AutoMapper;
using Portal.Core.Models;
using Portal.Infrastructure.Entities;
using Portal.Infrastructure.Extend;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Core.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile() 
        {
            CreateMap<Department, DepartmentVM>().ReverseMap();
            CreateMap<EmployeeVM, Employee>().ReverseMap()
                .ForPath(dest => dest.DepartmentName, source => source.MapFrom(src => src.Department.Name))
                .ForPath(dest => dest.DepartmentCode, source => source.MapFrom(src => src.Department.Code));
            CreateMap<City, CityVM>().ReverseMap();
            CreateMap<Country, CountryVM>().ReverseMap();
            CreateMap<District, DistrictVM>().ReverseMap();
            CreateMap<RegistrationVM, ApplicationUser>().ReverseMap()
                .ForPath(dest => dest.UserName, source => source.MapFrom(src => src.UserName))
                .ForPath(dest => dest.Email, source => source.MapFrom(src => src.Email))
                .ForPath(dest => dest.IsAgree,source => source.MapFrom(src => src.IsAgree));
        }
    }
}
