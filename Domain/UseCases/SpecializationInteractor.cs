using Domain.Logic;
using Domain.Models;
using Domain.Logic.Interfaces;

namespace Domain.UseCases
{
    public class SpecializationInteractor
    {
        private readonly ISpecializationRepository _db;

        public SpecializationInteractor(ISpecializationRepository spec_rep)
        {
            _db = spec_rep;
        }

        public Result<Specialization> CreateSpeciailization(Specialization specialization)
        {
            if (specialization.IsValid().isFailure)
                return Result.Fail<Specialization>("Incorrect specialization: " + specialization.IsValid().Error);
            if (_db.Create(specialization)!.IsValid().Success)
            {
                _db.Save();
                return Result.Ok(specialization);
            }
            return Result.Fail<Specialization>("Cannot create specialization");
        }

        public Result<Specialization> GetSpecialization(int id)
        {
            if (id < 0)
                return Result.Fail<Specialization>("Incorrect specialization id");

            var specialization = _db.GetItem(id);

            return specialization != null ? Result.Ok(specialization) : Result.Fail<Specialization>("Specialization not found");
        }

        public Result<Specialization> DeleteSpecialization(int id)
        {
            var result = GetSpecialization(id);
            if (result.isFailure)
                return Result.Fail<Specialization>(result.Error);
            if (_db.Delete(id)!.IsValid().Success)
            {
                _db.Save();
                return result;
            }
            return Result.Fail<Specialization>("Cannot delete the specialization");
        }

        public Result<IEnumerable<Specialization>> GetAllSpecializations()
        {
            return Result.Ok(_db.GetAll());
        }
        public Result<Specialization> GetByName(string name)
        {
            var result = _db.Get(name);
            if (result == null)
                return Result.Fail<Specialization>("There is no spec with this name");
            return Result.Ok(result);
        }
    }
}
