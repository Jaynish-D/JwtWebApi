using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace JwtWebApi.Services.UserServices
{
    public class UserServices : IUserServices
    {
        private readonly IHttpContextAccessor _httpContextAccesser;
        public UserServices(IHttpContextAccessor httpContextAccesser)
        {
            _httpContextAccesser = httpContextAccesser;
        }

        public string GatMyName()
        {
            var result = string.Empty;
            if (_httpContextAccesser.HttpContext != null)
            {
                result = _httpContextAccesser.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            }
            return result;
        }
    }
}
