using JwtWebApi.Data.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetAll()
        {
            return new List<SuperHero>
            {
                new SuperHero
                {
                    Id = 1,
                    Name="SpiderMan",
                    FirstName="Peter",
                    LastName="Parker",
                    Place = "New York City"
                }
            };
        }
    }
}
