using MediatR;
using Testovoe.Application.Doctor.DoctorRespose;

namespace Testovoe.Application.Doctor.DoctorRequest
{
    public class DoctorsListRequest : IRequest<List<DoctorsListResponse>>
    {
        public string SortBy { get; set; } = "fio";
        public int Page { get; set; }

    }
}
