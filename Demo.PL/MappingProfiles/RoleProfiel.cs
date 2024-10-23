using AutoMapper;
using Demo.PL.viewModels;
using Microsoft.AspNetCore.Identity;

namespace Demo.PL.MappingProfiles
{
    public class RoleProfiel:Profile
    {
        public RoleProfiel()
        {
                CreateMap<IdentityRole,RoleViewModel>().ForMember(d=>d.RoleName, O=>O.MapFrom(S=>S.Name)).ReverseMap();
        }
    }
}
