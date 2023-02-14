using JwtWebApi.Data.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineHotel.Repository.UnitOfWork2;
using Microsoft.EntityFrameworkCore;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly IUnitofWork _uow;

        public SuperHeroController(IUnitofWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetAll()
        {
            return Ok( _uow.GenericRepository<SuperHero>().GetAll().ToList());
        }
        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> Create(SuperHero hero)
        {
            await _uow.GenericRepository<SuperHero>().AddAsync(hero);
            _uow.Save();
            return Ok(_uow.GenericRepository<SuperHero>().GetAll().ToList());
        }
        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> Update(SuperHero hero)
        {
            var dbhero = await _uow.GenericRepository<SuperHero>().GetByIdAsync(hero.Id);
            if(dbhero == null)
            {
                return NotFound("Hero Not Found");
            }

            dbhero.Name = hero.Name;
            dbhero.FirstName = hero.FirstName;
            dbhero.LastName = hero.LastName;
            dbhero.Place = hero.Place;

            _uow.Save();

            return Ok(_uow.GenericRepository<SuperHero>().GetAll().ToList());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(object id)
        {
            var dbHero = await _uow.GenericRepository<SuperHero>().GetByIdAsync(id);
            if(dbHero == null)
            {
                return NotFound("Hero Not Found");
            }

            await _uow.GenericRepository<SuperHero>().DeleteAsync(dbHero);

            _uow.Save();
            return Ok(_uow.GenericRepository<SuperHero>().GetAll().ToList());
        }
    }
}
