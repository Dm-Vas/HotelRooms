using System.ComponentModel.DataAnnotations;

namespace HotelRoomsApi.Models.Dto
{
    public class RoomNumberDTO
    {
        [Required]
        public int RoomNo { get; set; }
        [Required]
        public int RoomID { get; set; }
        public string Details { get; set; }
        public RoomDTO Room { get; set; }
    }
}
