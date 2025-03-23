using BLL.DTO;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Contract
{
    public interface IBookManger
    {


        Task AddBook(BookDTO book);

        Task DeleteBook(BookDTO book);
        Task<BookDTO> GetBookById(int id);
        Task<IEnumerable<Book>>  GetBooks();
        Task UpdateBook(UpdateBookDTO book);

    }
}
