using HotelRoomsApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelRoomsApi.Data
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomNumber> RoomNumbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Room>().HasData(
                new Room
                {
                    Id = 1,
                    Name = "Room Name 1",
                    Description = "Room Description 1",
                    Price = 100,
                    Capacity = 1,
                    CreatedDate = DateTime.Now
                },
              new Room
              {
                  Id = 2,
                  Name = "Room Name 2",
                  Description = "Room Description 2",
                  Price = 100,
                  Capacity = 1,
                  CreatedDate = DateTime.Now
              },
              new Room
              {
                  Id = 3,
                  Name = "Room Name 3",
                  Description = "Room Description 3",
                  Price = 70,
                  Capacity = 2,
                  CreatedDate = DateTime.Now
              },
              new Room
              {
                  Id = 4,
                  Name = "Room Name 4",
                  Description = "Room Description 4",
                  Price = 70,
                  Capacity = 2,
                  CreatedDate = DateTime.Now
              },
              new Room
              {
                  Id = 5,
                  Name = "Room Name 5",
                  Description = "Room Description 5",
                  Price = 50,
                  Capacity = 3,
                  CreatedDate = DateTime.Now
              });
        }
    }
}
