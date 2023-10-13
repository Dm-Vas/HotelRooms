using HotelRoomsApi.Data;
using HotelRoomsApi.Models;
using HotelRoomsApi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace HotelRoomsApi.Repository
{
    public class RoomNumberRepository : Repository<RoomNumber>, IRoomNumberRepository
    {
        private readonly ApplicationDbContext _db;
        public RoomNumberRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public async Task<RoomNumber> UpdateAsync(RoomNumber room)
        {
            room.UpdatedDate = DateTime.Now;

            _db.RoomNumbers.Update(room);
            await _db.SaveChangesAsync();

            return room;
        }
    }
}
