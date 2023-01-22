using Database.Models;
using Domain.Models;


using DoctorDB = Database.Models.Doctor;
using DoctorDomain = Domain.Models.Doctor;

namespace Database.Converters
{
    public static class DoctorConverter
    {
        public static DoctorDB ToModel(this DoctorDomain model)
        {
            return new DoctorDB
            {
                Id = model.Id,
                Fullname = model.Fullname,
                SpecializationId = model.SpecializationId,
            };
        }

        public static DoctorDomain ToDomain(this DoctorDB model)
        {
            return new DoctorDomain
            {
                Id = model.Id,
                Fullname = model.Fullname,
                SpecializationId = model.SpecializationId,
            };
        }
    }
}