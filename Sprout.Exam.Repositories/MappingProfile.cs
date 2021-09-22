using AutoMapper;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Repositories
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<EmployeeDto, Employee>().ForMember(dest => dest.EmployeeTypeId, opt => opt.MapFrom(src => src.TypeId)).ReverseMap();
        }
    }
}
