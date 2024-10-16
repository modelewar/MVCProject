using AutoMapper;
using Demo.DAL.Models;
using Demo.PL.viewModels;

namespace Demo.PL.MappingProfiles
{
    public class DepartmentProfil:Profile
    {
        public DepartmentProfil()
        {
            CreateMap<DepartmentViewModel, Department>().ReverseMap();
        }
    }
}
