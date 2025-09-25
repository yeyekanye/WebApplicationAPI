using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly AppDbContext _context;

        public GameRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Game> CreateAsync(Game game)
        {
            _context.Games.Add(game);
            await _context.SaveChangesAsync();
            return game;
        }

        public async Task<Game?> UpdateAsync(Game game)
        {
            var existing = await _context.Games.FindAsync(game.Id);
            if (existing == null) return null;

            existing.Title = game.Title;
            existing.Genre = game.Genre;
            existing.Price = game.Price;
            existing.ReleaseDate = game.ReleaseDate;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null) return false;

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Game?> GetByIdAsync(int id)
        {
            return await _context.Games.FindAsync(id);
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _context.Games.ToListAsync();
        }

        public async Task<IEnumerable<Game>> GetByGenreAsync(string genre)
        {
            return await _context.Games
                                 .Where(g => g.Genre.ToLower() == genre.ToLower())
                                 .ToListAsync();
        }
    }
}
