using Microsoft.AspNetCore.Mvc;
using Domain.UseCases;
using Domain.Models;

namespace IT_Project.Controllers
{
    [ApiController]
    [Route("api/appointment")]
    public class AppointmentController : Controller
    {
        private readonly AppointmentInteractor _appointments;
        private readonly ScheduleInteractor _schedules;
        public AppointmentController(AppointmentInteractor appointment, ScheduleInteractor schedule)
        {
            _schedules = schedule;
            _appointments = appointment;
        }
        [HttpPost("save")]
        public IActionResult SaveAppointment(int patientId,
            int doctorId,
            DateTime start_time,
            DateTime end_time,
            int schedule_id)
        {
            Appointment appointment = new Appointment(start_time, end_time, patientId, doctorId);
            var schedule = _schedules.GetSchedule(schedule_id);
            if (schedule.isFailure)
            {
                return Problem(statusCode: 404, detail: schedule.Error);
            }

            var res = _appointments.SaveAppointment(appointment, schedule.Value);
            if (res.isFailure)
                return Problem(statusCode: 404, detail: res.Error);
            return Ok(res.Value);
        }

        [HttpGet("get")]
        public IActionResult GetAppointments(int specialization_id)
        {
            Specialization spec = new Specialization(specialization_id, "tmp");
            var res = _appointments.GetAppointments(spec);

            if (res.isFailure)
                return Problem(statusCode: 404, detail: res.Error);
            return Ok(res.Value);
        }
    }
}
