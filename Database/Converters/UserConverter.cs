using Database.Models;
using Domain.Models;


using UserDB = Database.Models.User;
using UserDomain = Domain.Models.User;

namespace Database.Converters
{
    public static class UserConverter
    {
        public static UserDB ToModel(this UserDomain model)
        {
            return new UserDB
            {
                Id = model.Id,
                Fullname = model.Fullname,
                Phone = model.Phone,
                UserName = model.UserName,
                Password = model.Password,
                Role = model.Role,
            };
        }

        public static UserDomain ToDomain(this UserDB model)
        {
            return new UserDomain
            {
                Id = model.Id,
                Fullname = model.Fullname,
                Phone = model.Phone,
                UserName = model.UserName,
                Password = model.Password,
                Role = model.Role,
            };
        }
    }
}