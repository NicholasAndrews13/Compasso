using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pokemon
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        private IRepository<Pokemon> _repo;
        public PokemonController(IRepository<Pokemon> repository)
        {
            _repo = repository;
        }
    }
}
