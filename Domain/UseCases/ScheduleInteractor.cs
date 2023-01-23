using Domain.Logic;
using Domain.Models;
using Domain.Logic.Interfaces;

namespace Domain.UseCases
{
    public class ScheduleInteractor
    {
        private readonly IDoctorRepository _doctor_db;
        
        public ScheduleInteractor(IScheduleRepository db, IDoctorRepository doctor_db)
        {
            _db = db;
            _doctor_db = doctor_db;
        }

        public Result<Schedule> GetSchedule(Doctor doctor)
        {
            var result = doctor.IsValid();
            if (result.isFailure)
                return Result.Fail<IEnumerable<Schedule>>("Cannot delete schedule");
            return Result.Ok(_db.getSchedule(doctor));
        }
        
        public Result<Schedule> GetSchedule(int scheduleId)
        {
            var res = _db.GetItem(scheduleId);
            return res != null ? Result.Ok(res) : Result.Fail<Schedule>("Cannot find schedule with this ID");
        }

        
        public Result<Schedule> CreateSchedule(int doctor_id, Schedule schedule)
        {
            var doctor = _doctor_db.GetItem(doctor_id);
            if (doctor == default)
                return Result.Fail<Schedule>("There is no doctor with this ID");
            var result = doctor.IsValid() & schedule.IsValid();
            if (!result)
                return Result.Fail<Schedule>("Can't create schedule");
            return Result.Fail<Schedule>("Unable to add schedule");
        }
        public Result<Schedule> UpdateSchedule(Schedule schedule)
        {
            var result = schedule.IsValid();
            if (result.isFailure)
                return Result.Fail<Schedule>("Invalid schedule: " + result.Error);
            var res = _db.Update(schedule);
            if (res != null)
            {
                _db.Save();
                return Result.Ok(res);
            }
            return Result.Fail<Schedule>("Unable to update schedule");
        }

        public Result<Schedule> DeleteSchedule(int id)
        {
            var result = GetSchedule(id);
            if (result.isFailure)
                return Result.Fail<Schedule>(result.Error);
            if (_db.Delete(id)!.IsValid().Success)
            {
                _db.Save();
                return result;
            }
            return Result.Fail<Schedule>("Cannot delete the schedule");
        }
    }
}
