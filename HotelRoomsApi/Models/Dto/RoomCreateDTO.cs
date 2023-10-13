using System.ComponentModel.DataAnnotations;

namespace HotelRoomsApi.Models.Dto
{
    public class RoomCreateDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        public int Capacity { get; set; }
    }
}
