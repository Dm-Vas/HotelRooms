using HotelRoomsApi.Models;

namespace HotelRoomsApi.Repository.IRepository
{
    public interface IRoomNumberRepository : IRepository<RoomNumber>
    {
        Task<RoomNumber> UpdateAsync(RoomNumber room);
    }
}
