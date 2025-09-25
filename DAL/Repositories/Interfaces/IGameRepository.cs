using DAL.Entities;

namespace DAL.Repositories.Interfaces
{
    public interface IGameRepository
    {
        Task<Game> CreateAsync(Game game);
        Task<Game?> UpdateAsync(Game game);
        Task<bool> DeleteAsync(int id);
        Task<Game?> GetByIdAsync(int id);
        Task<IEnumerable<Game>> GetAllAsync();
        Task<IEnumerable<Game>> GetByGenreAsync(string genre);
    }
}
