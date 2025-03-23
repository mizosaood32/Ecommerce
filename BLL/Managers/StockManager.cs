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
    public class StockManager : IStockManager
    {
        private readonly IStockRepository _StockRepo; //  = new ProductRepository();

        public StockManager(IStockRepository StockRepo)
        {
            _StockRepo = StockRepo;
        }

        public Task GetStockByBookId(int bookId) => _StockRepo.GetStockByBookId(bookId);


        public async Task<IEnumerable<StockDisplayModel>> GetStocks(string sTerm)
        {
            var stocks = await _StockRepo.GetStocks(sTerm);
            return stocks.Select(stock => new StockDisplayModel
            {
                Id = stock.Id,
                BookId = stock.BookId,
                Quantity = stock.Quantity,
                BookName = stock.BookName
            });


        }

        public async Task ManangeStock(StockDTO stockDto)
        {
            var stock = new Stock
            {
                BookId = stockDto.BookId,
                Quantity = stockDto.Quantity
            };
            await _StockRepo.ManageStock(stock);
        }

        public Task ManangeStock(int bookId)
        {
           return _StockRepo.ManageStock(new Stock { BookId = bookId, Quantity = 1 });

        }



        //public Task ManangeStock(int bookId) => _StockRepo.ManangeStock(bookId);

    }
}
