

using MediatR;

namespace Testovoe.Application.Patient.PatientRequest
{
    public class PatientDeleteRequest : IRequest<int>
    {
        public int Id { get; set; }
    }
}
