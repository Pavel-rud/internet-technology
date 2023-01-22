using Database.Converters;
using Domain.Logic.Interfaces;
using Domain.Models;

namespace Database.Repository
{
    public class AppointmentsRepository : IAppointmentRepository
    {
        private readonly ApplicationContext context;

        public AppointmentsRepository(ApplicationContext context) {
            this.context = context;
        }

        public Appointment Create(Appointment item) {
            return context.Appointments.Add(item.ToModel()).Entity.ToDomain();
        }

        public Appointment? Delete(int id) {
            var item = GetItem(id);
            if (item != default)
                return context.Appointments.Remove(item.ToModel()).Entity.ToDomain();
            return null;

        }

        public IEnumerable<Appointment> GetAll() {
            return context.Appointments.Select(item => item.ToDomain());
        }

        public IEnumerable<Appointment> GetAppointments(int DoctorId) {
            return context.Appointments
                .Where(item => item.DoctorId == DoctorId)
                .Select(item => item.ToDomain());
        }

        public IEnumerable<Appointment> GetAppointments(Specialization specialization) {
            var doctors = context.Doctors
                .Where(doctor => doctor.SpecializationId == specialization.Id)
                .Select(doctor => doctor.Id);
            return context.Appointments
                .Where(item => doctors.Contains(item.DoctorId))
                .Select(item => item.ToDomain());
        }

        public Appointment? GetItem(int id) {
            return context.Appointments.FirstOrDefault(appointment => appointment.Id == id)?.ToDomain();
        }

        public void Save() {
            context.SaveChangesAsync();
        }

        public Appointment Update(Appointment item) {
            return context.Appointments.Update(item.ToModel()).Entity.ToDomain();
        }
    }
}
