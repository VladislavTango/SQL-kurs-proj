using MediatR;
using Microsoft.AspNetCore.Mvc;
using Testovoe.Application.Patient.PatientRequest;

namespace Testovoe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PatientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddPatient([FromBody] PatientAddRequest patientAddRequest)
        {
            var response = await _mediator.Send(patientAddRequest);
            return Ok(response);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteDoctor([FromBody] PatientDeleteRequest patientDelete)
        {
            var response = await _mediator.Send(patientDelete);
            return Ok(response);
        }
        [HttpGet("getList")]
        public async Task<IActionResult> DoctorsList([FromQuery] PatientListRequest patientList)
        {
            var response = await _mediator.Send(patientList);
            return Ok(response);
        }

        [HttpPut("redact")]
        public async Task<IActionResult> DoctorRedact([FromBody] PatientRedactRequest patientRedact)
        {
            var response = await _mediator.Send(patientRedact);
            return Ok(response);
        }
        [HttpGet("getRow")]
        public async Task<IActionResult> GetDoctorForEdit([FromQuery] TakePatientRowRequest takePatient)
        {
            var response = await _mediator.Send(takePatient);
            return Ok(response);
        }

    }
}
