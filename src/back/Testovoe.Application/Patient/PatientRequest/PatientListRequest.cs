

using MediatR;
using Testovoe.Application.Patient.PatientResponse;

namespace Testovoe.Application.Patient.PatientRequest
{
    public class PatientListRequest : IRequest<List<PatientListResponse>>
    {
        public string SortBy { get; set; } = "surname";
        public int Page { get; set; }
    }
}
