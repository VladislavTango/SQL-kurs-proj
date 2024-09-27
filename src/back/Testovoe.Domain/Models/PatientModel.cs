using System.ComponentModel.DataAnnotations;
using Testovoe.Enums;

namespace Testovoe.Models
{
    
    public class PatientModel
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Address { get; set; }
        public DateTime BornTime { get; set; }
        public Sex Sex { get; set; }
        public RegionModel PatientRegion { get; set; }
    }
}
