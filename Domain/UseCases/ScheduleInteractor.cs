using Domain.Logic;
using Domain.Models;
using Domain.Logic.Interfaces;

namespace Domain.UseCases
{
    public class ScheduleInteractor
    {
        private readonly IScheduleRepository _db;
        
        public ScheduleInteractor(IScheduleRepository db)
        {
            _db = db;
        }

        public Result<IEnumerable<Schedule>> getSchedule(Doctor doctor)
        {
            var result = doctor.IsValid();
            if (result.isFailure)
                return Result.Fail<IEnumerable<Schedule>>("Cannot delete schedule");
            return Result.Ok(_db.getSchedule(doctor));
        }
        
        public Result<Schedule> CreateSchedule(Doctor doctor, Schedule schedule)
        {
            var result = doctor.IsValid() & schedule.IsValid();
            if (!result)
                return Result.Fail<Schedule>("Cannot create schedule");
            return _db.CreateSchedule(doctor, schedule) ? Result.Ok(schedule) : Result.Fail<Schedule>("Unable to add schedule");
        }

    }
}
