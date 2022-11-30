using JwtWebApi.Data.Models;
using JwtWebApi.JwtCode;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new();
        public PassWordHash PasswordHashClass = new();


        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            PasswordHashClass.CreatePasswordHash(request.Password,out byte[] passwordHash,out byte[] passwordSalt);
            user.Username= request.Username;
            user.PasswordHash= passwordHash;
            user.PasswordSalt= passwordSalt;
            //PasswordHash.inituser(user);
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(UserDto request)
        {
            if (request.Username != user.Username)
            {
                return BadRequest("User Not Found.");
            }

            var result = PasswordHashClass.VerifyPasswordHash(request.Password,user.PasswordHash,user.PasswordSalt);
            if(!result)
            {
                return BadRequest("Wrong Password.");
            }
            string token = PasswordHashClass.CreateToken(user);
            return Ok(token);
        }
    }
}
