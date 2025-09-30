using Application.DTOs;
using Application.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interfaces;

namespace Application.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _repository;

        public GameService(IGameRepository repository)
        {
            _repository = repository;
        }

        public async Task<GameDto> CreateAsync(GameCreateDto dto)
        {
            var game = new Game
            {
                Title = dto.Title,
                Genre = dto.Genre,
                Price = dto.Price,
                ReleaseDate = dto.ReleaseDate
            };

            var created = await _repository.CreateAsync(game);

            return new GameDto
            {
                Id = created.Id,
                Title = created.Title,
                Genre = created.Genre,
                Price = created.Price
            };
        }

        public async Task<GameDto?> UpdateAsync(GameUpdateDto dto)
        {
            var game = new Game
            {
                Id = dto.Id,
                Title = dto.Title,
                Genre = dto.Genre,
                Price = dto.Price,
                ReleaseDate = dto.ReleaseDate
            };

            var updated = await _repository.UpdateAsync(game);
            if (updated == null) return null;

            return new GameDto
            {
                Id = updated.Id,
                Title = updated.Title,
                Genre = updated.Genre,
                Price = updated.Price
            };
        }

        public async Task<bool> DeleteAsync(int id) =>
            await _repository.DeleteAsync(id);

        public async Task<IEnumerable<GameDto>> GetAllAsync()
        {
            var games = await _repository.GetAllAsync();
            return games.Select(g => new GameDto
            {
                Id = g.Id,
                Title = g.Title,
                Genre = g.Genre,
                Price = g.Price
            });
        }

        public async Task<GameDto?> GetByIdAsync(int id)
        {
            var game = await _repository.GetByIdAsync(id);
            return game == null ? null : new GameDto
            {
                Id = game.Id,
                Title = game.Title,
                Genre = game.Genre,
                Price = game.Price
            };
        }

        public async Task<IEnumerable<GameDto>> GetByGenreAsync(string genre)
        {
            var games = await _repository.GetByGenreAsync(genre);
            return games.Select(g => new GameDto
            {
                Id = g.Id,
                Title = g.Title,
                Genre = g.Genre,
                Price = g.Price
            });
        }
    }
}