using Application.DTOs;
using Application.Common;

namespace Application.Services.Interfaces
{
    public interface IGameService
    {
        Task<ServiceResponse<GameDto>> CreateAsync(GameCreateDto dto);
        Task<ServiceResponse<GameDto>> UpdateAsync(GameUpdateDto dto);
        Task<ServiceResponse<bool>> DeleteAsync(int id);
        Task<ServiceResponse<IEnumerable<GameDto>>> GetAllAsync();
        Task<ServiceResponse<GameDto>> GetByIdAsync(int id);
        Task<ServiceResponse<IEnumerable<GameDto>>> GetByGenreAsync(string genre);
    }
}
