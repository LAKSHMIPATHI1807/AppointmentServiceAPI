using Microsoft.AspNetCore.Mvc;
using AppointmentServiceAPI.Services;
using AppointmentServiceAPI.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace AppointmentServiceAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        [HttpGet("GetAllAppointments")]
        [Authorize(Roles = "Admin,Doctor,Patient")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var appointments = await _appointmentService.GetAllAppointmentsAsync();
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetAppointmentById/{id}")]
        [Authorize(Roles = "Admin,Doctor,Patient")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
                if (appointment == null)
                    return NotFound();

                return Ok(appointment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("AddAppointment")]
        [Authorize(Roles = "Admin,Doctor,Patient")]
        public async Task<IActionResult> Add(CreateAppointmentDto createAppointmentDto)
        {
            try
            {
                await _appointmentService.BookAppointmentAsync(createAppointmentDto);
                return Ok(createAppointmentDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("UpdateAppointment/{id}")]
        [Authorize(Roles = "Admin,Doctor,Patient")]
        public async Task<IActionResult> Update(int id, UpdateAppointmentDto updateAppointmentDto)
        {
            try
            {
                await _appointmentService.UpdateAppointmentAsync(id, updateAppointmentDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("CancelAppointment/{id}")]
        [Authorize(Roles = "Admin,Doctor,Patient")]
        public async Task<IActionResult> Cancel(int id)
        {
            try
            {
                await _appointmentService.CancelAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("CompleteAppointment/{id}")]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> Complete(int id)
        {
            try
            {
                await _appointmentService.CompleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetAppointmentsByPatientId/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByPatientId(int id)
        {
            try
            {
                var appointments = await _appointmentService.GetAppointmentsByPatientIdAsync(id);
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetAppointmentsByDoctorId/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByDoctorId(int id)
        {
            try
            {
                var appointments = await _appointmentService.GetAppointmentsByDoctorIdAsync(id);
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
