using Application.Common;
using Application.DTOs;
using Application.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;


namespace Application.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly string _rootPath;

        public GameService(IGameRepository gameRepository, IConfiguration config)
        {
            _gameRepository = gameRepository;
            _rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        }

        public async Task<ServiceResponse<List<GameDto>>> GetAllAsync()
        {
            var games = await _gameRepository.GetAllAsync();
            var result = games.Select(g => new GameDto
            {
                Id = g.Id,
                Name = g.Name,
                Description = g.Description,
                ImageFolderPath = g.ImageFolderPath
            }).ToList();

            return new ServiceResponse<List<GameDto>> { Data = result };
        }

        public async Task<ServiceResponse<GameDto>> GetByIdAsync(int id)
        {
            var game = await _gameRepository.GetByIdAsync(id);
            if (game == null)
                return new ServiceResponse<GameDto> { Success = false, Message = "Game not found" };

            return new ServiceResponse<GameDto>
            {
                Data = new GameDto
                {
                    Id = game.Id,
                    Name = game.Name,
                    Description = game.Description,
                    ImageFolderPath = game.ImageFolderPath
                }
            };
        }

        public async Task<ServiceResponse<GameDto>> CreateAsync(CreateGameDto dto)
        {
            var game = new Game
            {
                Name = dto.Name,
                Description = dto.Description,
                ImageFolderPath = dto.ImageFolderPath
            };

            await _gameRepository.CreateAsync(game);

            return new ServiceResponse<GameDto>
            {
                Data = new GameDto
                {
                    Id = game.Id,
                    Name = game.Name,
                    Description = game.Description,
                    ImageFolderPath = game.ImageFolderPath
                },
                Message = "Game created successfully"
            };
        }

        // 🆕 Видалення гри + видалення папки зображень
        public async Task<ServiceResponse<bool>> DeleteAsync(int id)
        {
            var game = await _gameRepository.GetByIdAsync(id);
            if (game == null)
                return new ServiceResponse<bool> { Success = false, Message = "Game not found" };

            await _gameRepository.DeleteAsync(game);

            // 🔥 Видаляємо папку зображень
            if (!string.IsNullOrEmpty(game.ImageFolderPath))
            {
                string fullPath = Path.Combine(_rootPath, game.ImageFolderPath);
                if (Directory.Exists(fullPath))
                {
                    Directory.Delete(fullPath, true);
                }
            }

            return new ServiceResponse<bool>
            {
                Data = true,
                Message = "Game deleted successfully"
            };
        }
    }
}
