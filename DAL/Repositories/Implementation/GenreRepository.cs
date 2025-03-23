using DAL.Context;
using DAL.Entities;
using DAL.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implementation;


public class GenreRepository : IGenreRepository
{
    private readonly ApplicationDbContext _context;
    public GenreRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddGenre(Genre genre)
    {
        _context.Genres.Add(genre);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateGenre(Genre genre)
    {
        _context.Genres.Update(genre);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteGenre(Genre genre)
    {
        //_context.Genres.Remove(genre);
        //await _context.SaveChangesAsync();
        var existing = _context.Genres.Local.FirstOrDefault(g => g.Id == genre.Id);

        if (existing != null)
        {
            _context.Entry(existing).State = EntityState.Detached; // ✅ Detach existing tracked entity
        }

        _context.Genres.Attach(genre);
        _context.Genres.Remove(genre);
        await _context.SaveChangesAsync();
    }

    public async Task<Genre?> GetGenreById(int id)
    {
        return await _context.Genres.FindAsync(id);
    }

    public async Task<IEnumerable<Genre>> GetGenres()
    {
        return await _context.Genres.ToListAsync();
    }


}
