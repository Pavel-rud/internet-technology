using Domain.Models;

namespace Domain.Logic.Interfaces
{
    public interface IScheduleRepository : IRepository<Schedule>
    {
        IEnumerable<Schedule> getSchedule(Doctor doctor);
        bool CreateSchedule(Doctor doctor, Schedule schedule);
        bool UpdateSchedule(Doctor? doctor, Schedule? schedule);
    }
}
