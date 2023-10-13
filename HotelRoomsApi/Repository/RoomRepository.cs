using HotelRoomsApi.Data;
using HotelRoomsApi.Models;
using HotelRoomsApi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace HotelRoomsApi.Repository
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        private readonly ApplicationDbContext _db;
        public RoomRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public async Task<Room> UpdateAsync(Room room)
        {
            room.UpdatedDate = DateTime.Now;

            _db.Rooms.Update(room);
            await _db.SaveChangesAsync();

            return room;
        }
    }
}
