using JwtWebApi.Data.Models;
using JwtWebApi.JwtCode;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using JwtWebApi.Services.UserServices;
namespace JwtWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new();

        private readonly IUserServices _userServices;
       
        public AuthController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            PassWordHash.CreatePasswordHash(request.Password,out byte[] passwordHash,out byte[] passwordSalt);
            user.Username= request.Username;
            user.PasswordHash= passwordHash;
            user.PasswordSalt= passwordSalt;
            //PasswordHash.inituser(user);
            return Ok(user);
        }

        [HttpGet,Authorize]
        public ActionResult<string> GetMe()
        {
            var userName = _userServices.GatMyName();
            return Ok(userName);
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(UserDto request)
        {
            if (request.Username != user.Username)
            {
                return BadRequest("User Not Found.");
            }

            var result = PassWordHash.VerifyPasswordHash(request.Password,user.PasswordHash,user.PasswordSalt);
            if(!result)
            {
                return BadRequest("Wrong Password.");
            }
            string token = PassWordHash.CreateToken(user);
            return Ok(token);
        }
    }
}
