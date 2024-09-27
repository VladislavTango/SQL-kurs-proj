using MediatR;


namespace Testovoe.Application.Doctor.DoctorRequest
{
    public class DeleteDoctorRequest : IRequest<int>
    {
        public int Id { get; set; }
    }
}
