using BLL.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Contract
{
    public interface IStockManager
    {
        
          Task ManangeStock(StockDTO stock);
          Task ManangeStock(int bookId);
          Task GetStockByBookId(int bookId);
          Task<IEnumerable<StockDisplayModel>> GetStocks(string sTerm);
    }
}
