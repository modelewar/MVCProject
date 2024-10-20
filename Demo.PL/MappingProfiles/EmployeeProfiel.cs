using AutoMapper;
using Demo.DAL.Models;
using Demo.PL.viewModels;

namespace Demo.PL.MappingProfiles
{
    public class EmployeeProfiel:Profile
    {
        public EmployeeProfiel()
        {
               CreateMap<EmployeeViewModel , Employee>().ReverseMap(); 
        }
    }
}
