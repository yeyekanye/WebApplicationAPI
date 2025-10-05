using Application.DTOs;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _service;

        public GamesController(IGameService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.GetAllAsync();
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _service.GetByIdAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpGet("genre/{genre}")]
        public async Task<IActionResult> GetByGenre(string genre)
        {
            var response = await _service.GetByGenreAsync(genre);
            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(GameCreateDto dto)
        {
            var response = await _service.CreateAsync(dto);
            return response.Success
                ? CreatedAtAction(nameof(GetById), new { id = response.Data!.Id }, response)
                : BadRequest(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, GameUpdateDto dto)
        {
            if (id != dto.Id)
                return BadRequest(new { Message = "Id mismatch" });

            var response = await _service.UpdateAsync(dto);
            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _service.DeleteAsync(id);
            return response.Success ? NoContent() : NotFound(response);
        }
    }
}