using MediatR;
using Testovoe.Application.Patient.PatientResponse;

namespace Testovoe.Application.Patient.PatientRequest
{
    public class TakePatientRowRequest : IRequest<TakePatientRowResponse>
    {
        public int id { get; set; }
    }
}
