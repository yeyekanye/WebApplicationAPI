using Application.DTOs;

namespace Application.Services.Interfaces
{
    public interface IGameService
    {
        Task<GameDto> CreateAsync(GameCreateDto dto);
        Task<GameDto?> UpdateAsync(GameUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<GameDto>> GetAllAsync();
        Task<GameDto?> GetByIdAsync(int id);
        Task<IEnumerable<GameDto>> GetByGenreAsync(string genre);
    }
}