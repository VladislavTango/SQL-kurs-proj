using MediatR;
using Testovoe.Application.Doctor.DoctorRespose;

namespace Testovoe.Application.Doctor.DoctorRequest
{
    public class TakeReadactRowRequest : IRequest<TakeRedactRowResponse>
    {
        public int Id { get; set; }
    }
}
