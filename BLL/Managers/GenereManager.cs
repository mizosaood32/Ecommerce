using BLL.Contract;
using BLL.DTO;
using DAL.Entities;
using DAL.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Managers
{
    public class GenereManager : IGenereManager
    {
        private readonly IGenreRepository _GenereRepo; //  = new ProductRepository();

        public GenereManager(IGenreRepository GenereRepo) {
            _GenereRepo = GenereRepo;
        }
        public async Task AddGenre(GenreDTO genre)
        {
            var newgenre = new Genre
            {
                Id = genre.Id,
                GenreName = genre.GenreName,
                //Books = genre.Books,
                
            };
            await _GenereRepo.AddGenre(newgenre); ;
        }

        public async Task DeleteGenre(GenreDTO genre)
        {
            Genre delGenre = new Genre
            {

                Id = genre.Id,
                GenreName = genre.GenreName,

            };
            await _GenereRepo.DeleteGenre(delGenre);


        }

        public async Task<GenreDTO?> GetGenreById(int id)
        {
            var product = await _GenereRepo.GetGenreById(id);
            var GenreDTO = new GenreDTO
            {
                Id = product.Id,
                GenreName = product.GenreName,
           
            };
            return GenreDTO;
        }

        public async Task<List<GenreDTO>> GetGenres()
        {
            var Generes = await _GenereRepo.GetGenres();
            // Business Logic

            // Mapping   Entity  ====>  DTO

            var GenreDtoList = Generes
                .Select(p => new GenreDTO
                {
                    Id = p.Id,
                    GenreName = p.GenreName,
                   
                })
                .ToList();

            return GenreDtoList;
        }

        public async Task UpdateGenre(GenreDTO genre)
        {
            //_context.Genres.Update(genre);
            var result = new Genre
            {

                Id = genre.Id,
                GenreName = genre.GenreName,
                //Books = genre.Books,
            };


            await _GenereRepo.UpdateGenre(result);
        }
    }
}
