using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelRoomsApi.Models
{
    public class RoomNumber
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RoomNo { get; set; }

        [ForeignKey("Room")]
        public int RoomID { get; set; }

        public Room Room { get; set; }

        public string Details { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
