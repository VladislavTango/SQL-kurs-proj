using Testovoe.Enums;
using Testovoe.Models;

namespace Testovoe.Application.Patient.PatientResponse
{
    public class PatientListResponse
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Address { get; set; }
        public DateTime BornTime { get; set; }
        public Sex Sex { get; set; }
        public int PatientRegionNumber { get; set; }
    }
}
