using MediatR;

namespace Testovoe.Application.Doctor.DoctorRequest
{
    public class DoctorRedactRequest : IRequest<Unit>
    {
        public int RedactId { get; set; }
        public string FIO { get; set; }
        public int RoomNumber { get; set; }
        public string Specialization { get; set; }
        public int Region { get; set; }
    }
}
