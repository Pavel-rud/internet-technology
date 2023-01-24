using Database.Models;
using Domain.Models;


using SpecializationDB = Database.Models.Specialization;
using SpecializationDomain = Domain.Models.Specialization;

namespace Database.Converters
{
    public static class DomainModelSpecializationConverter
    {
        public static SpecializationDB ToModel(this SpecializationDomain model)
        {
            return new SpecializationDB
            {
                Id = model.Id,
                Name = model.Name,
            };
        }

        public static SpecializationDomain ToDomain(this SpecializationDB model)
        {
            return new SpecializationDomain
            {
                Id = model.Id,
                Name = model.Name
            };
        }
    }
}