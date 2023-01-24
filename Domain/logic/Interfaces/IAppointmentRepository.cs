using Domain.Models;

namespace Domain.Logic.Interfaces
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        bool CreateAppointment(Appointment appointment);
        IEnumerable<Appointment> GetAppointments(int DoctorId);
        IEnumerable<Appointment> GetAppointments(Specialization specialization);
    }
}
