using Application.Common;
using Application.DTOs;

namespace Application.Services.Interfaces
{
    public interface IGameService
    {
        Task<ServiceResponse<List<GameDto>>> GetAllAsync();
        Task<ServiceResponse<GameDto>> GetByIdAsync(int id);
        Task<ServiceResponse<GameDto>> CreateAsync(CreateGameDto dto);
        Task<ServiceResponse<bool>> DeleteAsync(int id); // 🆕 додано
    }
}
