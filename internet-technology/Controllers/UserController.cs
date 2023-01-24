using Domain.UseCases;
using Domain.Models;
using IT_Project.Serializers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Domain.Models;
using Domain.UseCases;
using IT_Project.Auth;
using Microsoft.AspNetCore.Authorization;

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
		[Authorize]
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
                Phone = user.Value.Phone,
                Fullname = user.Value.Fullname,
                Role = user.Value.Role,
            });
        }

        [HttpPost("register")]
        public IActionResult RegisterUser(string username, string password, string phone, string fio, Role role)
        {
            User user = new(0, phoner, fio, username, password, role: role);
            var register = _users.Register(user);

            if (register.isFailure)
                return Problem(statusCode: 404, detail: register.Error);
            return Ok(new UserSerializer
            {
                Id = register.Value.Id,
                username = register.Value.UserName,
                Phone = register.Value.Phone,
                Fullname = register.Value.Fullname,
                Role = register.Value.Role,
				Token = TokenManager.GenerateToken(register.Value.UserName),
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
                Phone = res.Value.Phone,
                Fullname = res.Value.Fullname,
                Role = res.Value.Role,
            });
        }
		[HttpGet("login")]
         public IActionResult Login(string username, string password)
         {
             if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                 return BadRequest();
             var result = _users.GetUserByLogin(username);
             if (result.isFailure)
                 return NotFound("Invalid login or password");
             if (result.Value.Password != password)
                 return Unauthorized("Invalid login or password");
             return Ok(new UserSerializer
             {
                 Id = result.Value.Id,
                 username = result.Value.UserName,
                 Phone = result.Value.Phone,
                 Fullname = result.Value.Fullname,
                 Role = result.Value.Role,
                 Token = TokenManager.GenerateToken(username),
             });

         }
    }
}