using HotelRoomsApi.Models;

namespace HotelRoomsApi.Repository.IRepository
{
    public interface IRoomRepository : IRepository<Room>
    {
        Task<Room> UpdateAsync(Room room);
    }
}
