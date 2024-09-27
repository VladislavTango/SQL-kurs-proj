
namespace Testovoe.Models
{
    public class DoctorModel
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public int DoctorsRoomId { get; set; }
        public DoctorsRoomModel DoctorsRoom { get; set; }
        public SpecializationModel Specialization { get; set; }
        public RegionModel? DoctorsRegion { get; set; }
    }
}
