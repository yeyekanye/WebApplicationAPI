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

        // ✅ Отримати всі ігри
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var games = await _repository.GetAllAsync();
            return Ok(games);
        }

        // ✅ Отримати гру за Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var game = await _repository.GetByIdAsync(id);
            if (game == null) return NotFound();
            return Ok(game);
        }

        // ✅ Отримати ігри за категорією (жанром)
        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetByCategory(string category)
        {
            var games = await _repository.GetByGenreAsync(category);
            return Ok(games);
        }

        // ✅ Створити гру
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Game game)
        {
            var created = await _repository.CreateAsync(game);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // ✅ Оновити гру
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Game game)
        {
            if (id != game.Id) return BadRequest("Id in URL and body must match");

            var updated = await _repository.UpdateAsync(game);
            if (updated == null) return NotFound();

            return Ok(updated);
        }

        // ✅ Видалити гру
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
