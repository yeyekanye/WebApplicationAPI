using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IGameRepository _repository;

        public GamesController(IGameRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var game = await _repository.GetByIdAsync(id);
            return game == null ? NotFound() : Ok(game);
        }

        [HttpGet("genre/{genre}")]
        public async Task<IActionResult> GetByGenre(string genre) =>
            Ok(await _repository.GetByGenreAsync(genre));

        [HttpPost]
        public async Task<IActionResult> Create(Game game)
        {
            var created = await _repository.CreateAsync(game);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Game game)
        {
            if (id != game.Id) return BadRequest();
            var updated = await _repository.UpdateAsync(game);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
