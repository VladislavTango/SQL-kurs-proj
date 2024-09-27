
namespace Testovoe.Models
{
    public class DoctorsRoomModel
    {
        public int Id { get; set; }
        public int RoomNumber {  get; set; }
        public List<DoctorModel> Doctor { get; set; } = new List<DoctorModel>();
        public int DoctorId { get; set; }
    }
}
