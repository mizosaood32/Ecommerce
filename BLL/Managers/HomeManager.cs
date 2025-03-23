using BLL.Contract;
using DAL.Entities;
using DAL.Repositories.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Managers
{
    public class HomeManager : IHomeManager
    {
        private readonly IHomeRepository _HomeRepo; //  = new ProductRepository();

        public HomeManager(IHomeRepository HomeRepo)
        {
            _HomeRepo = HomeRepo;
        }

        public async Task<IEnumerable<Genre>> Genres()
        {

            return await _HomeRepo.Genres();
        }

        public async Task<IEnumerable<Book>> GetBooks(string sTerm = "", int genreId = 0)
        {
            return await _HomeRepo.GetBooks();

        }
    }
}
