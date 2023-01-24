using Database.Converters;
using Domain.Logic.Interfaces;
using Domain.Models;

namespace Database.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ApplicationContext context;

        public DoctorRepository(ApplicationContext context) {
            this.context = context;
        }

        public Doctor Create(Doctor item) {
            return context.Add(item.ToModel()).Entity.ToDomain();
        }

        public Doctor? Delete(int id) {
            Doctor? item = GetItem(id);
            if (item == default)
                return null;
            return context.Remove(item.ToModel()).Entity.ToDomain();
        }

        public IEnumerable<Doctor> GetAll() {
            return context.Doctors.Select(item => item.ToDomain());
        }

        public IEnumerable<Doctor>? getDoctor(Specialization specialization) {
            return context.Doctors
                .Where(item => item.SpecializationId == specialization.Id)
                .Select(item => item.ToDomain());
        }

        public Doctor? GetItem(int id) {
            return context.Doctors.FirstOrDefault(item => item.Id == id)?.ToDomain();
        }

        public void Save() {
            context.SaveChangesAsync();
        }

        public Doctor Update(Doctor item) {
            return context.Doctors.Update(item.ToModel()).Entity.ToDomain();
        }
    }
}
