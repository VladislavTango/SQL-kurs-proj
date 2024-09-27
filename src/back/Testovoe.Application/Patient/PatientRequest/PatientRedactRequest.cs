using MediatR;
using Testovoe.Enums;


namespace Testovoe.Application.Patient.PatientRequest
{
    public class PatientRedactRequest : IRequest<Unit>
    {
        public int RedactId { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Address { get; set; }
        public DateTime BornTime { get; set; }
        public Sex Sex { get; set; }
        public int PatientRegionNumber { get; set; }
    }
}
