namespace Testovoe.Application.Doctor.DoctorRespose
{
    public class TakeRedactRowResponse
    {
        public int DoctorID { get; set; }
        public string FIO { get; set; }
        public int DoctorsRoomId { get; set; }
        public int SpecializationID { get; set; }
        public int? DoctorsRegionID { get; set; }
    }
}
