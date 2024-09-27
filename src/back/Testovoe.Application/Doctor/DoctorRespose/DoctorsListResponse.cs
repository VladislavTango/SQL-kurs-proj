namespace Testovoe.Application.Doctor.DoctorRespose
{
    public class DoctorsListResponse
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public int RoomNumber { get; set; }
        public string SpecializationName { get; set; }
        public int Region { get; set; }
    }
}
