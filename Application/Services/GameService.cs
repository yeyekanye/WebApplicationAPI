using Application.DTOs;
using Application.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Application.Common;

namespace Application.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _repository;

        public GameService(IGameRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<GameDto>> CreateAsync(GameCreateDto dto)
        {
            var game = new Game
            {
                Title = dto.Title,
                Genre = dto.Genre,
                Price = dto.Price,
                ReleaseDate = dto.ReleaseDate
            };

            var created = await _repository.CreateAsync(game);

            return new ServiceResponse<GameDto>
            {
                Data = new GameDto
                {
                    Id = created.Id,
                    Title = created.Title,
                    Genre = created.Genre,
                    Price = created.Price
                },
                Message = "Game created successfully"
            };
        }

        public async Task<ServiceResponse<GameDto>> UpdateAsync(GameUpdateDto dto)
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
            if (updated == null)
            {
                return new ServiceResponse<GameDto>
                {
                    Success = false,
                    Message = $"Game with id {dto.Id} not found"
                };
            }

            return new ServiceResponse<GameDto>
            {
                Data = new GameDto
                {
                    Id = updated.Id,
                    Title = updated.Title,
                    Genre = updated.Genre,
                    Price = updated.Price
                },
                Message = "Game updated successfully"
            };
        }

        public async Task<ServiceResponse<bool>> DeleteAsync(int id)
        {
            var deleted = await _repository.DeleteAsync(id);

            if (!deleted)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = $"Game with id {id} not found",
                    Data = false
                };
            }

            return new ServiceResponse<bool>
            {
                Data = true,
                Message = "Game deleted successfully"
            };
        }

        public async Task<ServiceResponse<IEnumerable<GameDto>>> GetAllAsync()
        {
            var games = await _repository.GetAllAsync();
            return new ServiceResponse<IEnumerable<GameDto>>
            {
                Data = games.Select(g => new GameDto
                {
                    Id = g.Id,
                    Title = g.Title,
                    Genre = g.Genre,
                    Price = g.Price
                })
            };
        }

        public async Task<ServiceResponse<GameDto>> GetByIdAsync(int id)
        {
            var game = await _repository.GetByIdAsync(id);
            if (game == null)
            {
                return new ServiceResponse<GameDto>
                {
                    Success = false,
                    Message = $"Game with id {id} not found"
                };
            }

            return new ServiceResponse<GameDto>
            {
                Data = new GameDto
                {
                    Id = game.Id,
                    Title = game.Title,
                    Genre = game.Genre,
                    Price = game.Price
                }
            };
        }

        public async Task<ServiceResponse<IEnumerable<GameDto>>> GetByGenreAsync(string genre)
        {
            var games = await _repository.GetByGenreAsync(genre);

            return new ServiceResponse<IEnumerable<GameDto>>
            {
                Data = games.Select(g => new GameDto
                {
                    Id = g.Id,
                    Title = g.Title,
                    Genre = g.Genre,
                    Price = g.Price
                }),
                Message = games.Any() ? "" : $"No games found with genre {genre}"
            };
        }
    }
}