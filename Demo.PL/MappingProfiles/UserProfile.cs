using AutoMapper;
using Demo.DAL.Models;
using Demo.PL.viewModels;

namespace Demo.PL.MappingProfiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
        }
    }
}
