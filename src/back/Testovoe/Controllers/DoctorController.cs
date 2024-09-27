using MediatR;
using Microsoft.AspNetCore.Mvc;
using Testovoe.Application.Doctor.DoctorRequest;

namespace Testovoe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DoctorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddDoctor([FromBody] AddDoctorRequest addDoctorRequest)
        {
            var response = await _mediator.Send(addDoctorRequest);
            return Ok(response);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteDoctor([FromBody] DeleteDoctorRequest deleteDoctor) {
            var response = await _mediator.Send(deleteDoctor);
            return Ok(response);
        }
        [HttpGet("getList")]
        public async Task<IActionResult> DoctorsList( [FromQuery] DoctorsListRequest doctorsList)
        {
            var response = await _mediator.Send(doctorsList);
            return Ok(response);
        }

        [HttpPut("redact")]
        public async Task<IActionResult> DoctorRedact([FromBody] DoctorRedactRequest doctorRedact) {
            var response = await _mediator.Send(doctorRedact);
            return Ok(response);
        }
        [HttpGet("getRow")]
        public async Task<IActionResult> GetDoctorForEdit([FromQuery] TakeReadactRowRequest takeReadact)
        {
            var response = await _mediator.Send(takeReadact);
            return Ok(response);
        }
    }
}
