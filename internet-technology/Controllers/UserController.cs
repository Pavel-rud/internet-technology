using Domain.UseCases;
using Domain.Models;
using IT_Project.Serializers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace HospitalProjectIT.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly UserInteractor _users;
        public UserController(UserInteractor users)
        {
            _users = users;
        }

        [HttpGet("getUserByLogin")]
        public ActionResult<UserSerializer> GetUserByLogin(string login)
        {
            if (login == string.Empty)
                return Problem(statusCode: 404, detail: "Не указан логин");

            var user = _users.GetUserByLogin(login);
            if (user.isFailure)
                return Problem(statusCode: 404, detail: user.Error);

            return Ok(new UserSerializer
            {
                Id = user.Value.Id,
                username = user.Value.UserName,
                PhoneNumber = user.Value.PhoneNumber,
                Fullname = user.Value.Fullname,
                Role = user.Value.Role,
            });
        }

        [HttpPost("register")]
        public IActionResult RegisterUser(string username, string password, string phone_number, string fio, Role role)
        {
            User user = new(0, phone_number, fio, username, password, role: role);
            var register = _users.Register(user);

            if (register.isFailure)
                return Problem(statusCode: 404, detail: register.Error);
            return Ok(new UserSerializer
            {
                Id = register.Value.Id,
                username = register.Value.UserName,
                PhoneNumber = register.Value.PhoneNumber,
                Fullname = register.Value.Fullname,
                Role = register.Value.Role,
            });
        }

        [HttpGet("isUserExists")]
        public IActionResult IsUserExists(string login)
        {
            var res = _users.IsUserExists(login);

            if (res.isFailure)
                return Problem(statusCode: 404, detail: res.Error);

            return Ok(new UserSerializer
            {
                Id = res.Value.Id,
                username = res.Value.UserName,
                PhoneNumber = res.Value.PhoneNumber,
                Fullname = res.Value.Fullname,
                Role = res.Value.Role,
            });
        }
    }
}