using AutoMapper;
using DataAccess.Identity;
using Domain.Entities;
using Presentation.Models;

namespace Presentation.AutoMapProfile
{
    public class PresentationProfile : Profile
    {
        public PresentationProfile()
        {
            CreateMap<UserUpdateModel, ApplicationUser>().ReverseMap();
            CreateMap<MeetingRoomCreateModel, MeetingRoom>().ReverseMap();
            CreateMap<MeetingRoomUpdateModel, MeetingRoom>().ReverseMap();
        }
    }
}
