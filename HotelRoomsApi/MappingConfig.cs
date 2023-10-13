using AutoMapper;
using HotelRoomsApi.Models;
using HotelRoomsApi.Models.Dto;

namespace MagicVilla_VillaAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            CreateMap<Room, RoomDTO>();
            CreateMap<RoomDTO, Room>();

            CreateMap<Room, RoomCreateDTO>().ReverseMap();
            CreateMap<Room, RoomUpdateDTO>().ReverseMap();

            CreateMap<RoomNumber, RoomNumberDTO>().ReverseMap();
            CreateMap<RoomNumber, RoomNumberCreateDTO>().ReverseMap();
            CreateMap<RoomNumber, RoomNumberUpdateDTO>().ReverseMap();
            CreateMap<ApplicationUser, UserDTO>().ReverseMap();
        }
    }
}
