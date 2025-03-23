using BLL.DTO;
using DAL.Constants;



//using DAL.Constants;
using DAL.Context;
using DAL.Entities;
using DAL.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implementation
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;

        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Stock?> GetStockByBookId(int bookId) => await _context.Stocks.FirstOrDefaultAsync(s => s.BookId == bookId);

        public async Task ManageStock(Stock stockToManage)
        {
            // if there is no stock for given book id, then add new record
            // if there is already stock for given book id, update stock's quantity
            var existingStock = await GetStockByBookId(stockToManage.BookId);
            if (existingStock is null)
            {
                var stock = new Stock { BookId = stockToManage.BookId, Quantity = stockToManage.Quantity };
                _context.Stocks.Add(stock);
            }
            else
            {
                existingStock.Quantity = stockToManage.Quantity;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<StockDisplayModel>> GetStocks(string sTerm)
        {
            var stocksQuery = _context.Books
                                      .AsNoTracking()
                                      .Include(b => b.Stock)
                                      .AsQueryable();

            if (!string.IsNullOrWhiteSpace(sTerm))//modified
            {
                stocksQuery = stocksQuery.Where(b => EF.Functions.Like(b.BookName, sTerm + "%"));
            }


            var stocks = await stocksQuery
                .Select(book => new StockDisplayModel
                {
                    BookId = book.Id,
                    BookName = book.BookName ?? string.Empty,
                    Quantity = book.Stock == null ? 0 : book.Stock.Quantity
                })
                .ToListAsync(); // ✅ Executes query asynchronously

            return stocks;
        }
    }
}
