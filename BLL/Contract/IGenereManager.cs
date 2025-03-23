using BLL.DTO;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Contract
{
    public interface IGenereManager
    {

         Task DeleteGenre(GenreDTO genre);
         Task AddGenre(GenreDTO genre);
         Task<GenreDTO?> GetGenreById(int id);

         Task<List<GenreDTO>> GetGenres();

         Task UpdateGenre(GenreDTO genre);

    }
}
