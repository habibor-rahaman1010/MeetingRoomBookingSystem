using AutoMapper;
using DataAccess.Identity;
using Presentation.Models;

namespace Presentation.AutoMapProfile
{
    public class PresentationProfile : Profile
    {
        public PresentationProfile()
        {
            CreateMap<UserUpdateModel, ApplicationUser>().ReverseMap();
        }
    }
}
