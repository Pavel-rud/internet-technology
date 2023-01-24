using Domain.Models;
using Domain.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace IT_Project.Controllers
{
    [Route("api/schedule")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly ScheduleInteractor _schedules;
        private readonly DoctorInteractor _doctors;

        public ScheduleController(ScheduleInteractor schedules, DoctorInteractor doctors)
        {
            _schedules = schedules;
            _doctors = doctors;
        }

        [HttpPost("create")]
        public IActionResult AddSchedule(int doctor_id, DateTime start_time, DateTime end_time)
        {
            Schedule schedule = new Schedule(doctor_id, start_time, end_time);

            var res = _schedules.CreateSchedule(doctor_id, schedule);
            if (res.isFailure)
                return Problem(statusCode: 404, detail: res.Error);
            return Ok(res.Value);
        }

        [HttpGet("update")]
        public IActionResult UpdateSchedule(int schedule_id, int? doctor_id, DateTime? start_time, DateTime? end_time)
        {
            var res = _schedules.GetSchedule(schedule_id);
            if (res.isFailure)
                return Problem(statusCode: 404, detail: res.Error);

            var schedule = res.Value;

            if (doctor_id != null)
                schedule.DoctorId = (int)doctor_id;
            if (start_time != null && end_time != null)
            {
                schedule.StartTime = (DateTime)start_time;
                schedule.EndTime = (DateTime)end_time;
            }

            var updateResult = _schedules.UpdateSchedule(schedule);

            if (updateResult.isFailure)
                return Problem(statusCode: 404, detail: updateResult.Error);

            return Ok();
        }
        [HttpGet("getById")]
        public IActionResult GetById(int schedule_id)
        {
            var res = _schedules.GetSchedule(schedule_id);
            if (res.isFailure)
                return Problem(statusCode: 404, detail: res.Error);
            return Ok(res.Value);
        }
        [HttpGet("getByDoctorId")]
        public IActionResult GetByDoctorId(int doctor_id)
        {
            var doctor = _doctors.GetDoctor(doctor_id);
            if (doctor.isFailure)
                return Problem(statusCode: 404, detail: doctor.Error);
            var res = _schedules.GetSchedule(doctor.Value);
            if (res.isFailure)
                return Problem(statusCode: 404, detail: res.Error);
            return Ok(res.Value);
        }
        [HttpDelete("delete")]
        public IActionResult DeleteSchedule(int id)
        {
            var res = _schedules.DeleteSchedule(id);
            if (res.isFailure)
                return Problem(statusCode: 400, detail: res.Error);
            return Ok(res.Value);
        }
    }
}
