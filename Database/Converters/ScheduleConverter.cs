using Database.Models;
using Domain.Models;


using ScheduleDB = Database.Models.Schedule;
using ScheduleDomain = Domain.Models.Schedule;

namespace Database.Converters
{
    public static class ScheduleConverter
    {
        public static ScheduleDB ToModel(this ScheduleDomain model)
        {
            return new ScheduleDB
            {
                Id = model.Id,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                DoctorId = model.DoctorId,
            };
        }

        public static ScheduleDomain ToDomain(this ScheduleDB model)
        {
            return new ScheduleDomain
            {
                Id = model.Id,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                DoctorId = model.DoctorId,
            };
        }
    }
}