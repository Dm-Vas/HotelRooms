using Microsoft.AspNetCore.Identity;

namespace HotelRoomsApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
