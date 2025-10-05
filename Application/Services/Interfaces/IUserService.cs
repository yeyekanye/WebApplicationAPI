using Application.Common;
using Application.DTOs;

namespace Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponse<AuthResponseDto>> RegisterAsync(RegisterDto dto);
    }
}