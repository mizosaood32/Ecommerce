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
using static System.Net.Mime.MediaTypeNames;

namespace BLL.Managers
{
    public class BookManager: IBookManger
    {
        private readonly IBookRepository _BookRepo; //  = new ProductRepository();
        public BookManager(IBookRepository BookRepo)
        {
            _BookRepo = BookRepo;
        }

        public async Task AddBook(BookDTO book)
        {
            var newBook = new Book
            {
                Id = book.Id,

                BookName = book.BookName,
                AuthorName = book.AuthorName,
                GenreId = book.GenreId,
                Price = book.Price,
                Image = book.Image,
                  
            };
            await _BookRepo.AddBook(newBook);




        }

        public async Task DeleteBook(BookDTO book)
        {
            var newBook = new Book
            {
                Id = book.Id,
                BookName = book.BookName,
                AuthorName = book.AuthorName,
                GenreId = book.GenreId,
                Price = book.Price,
                Image = book.Image,
            };
            await _BookRepo.DeleteBook(newBook);


        }

        public async Task<BookDTO> GetBookById(int id)
        {
           
            var book = await _BookRepo.GetBookById(id);
            var bookdto = new BookDTO
            {
                Id = book.Id,
                BookName = book.BookName,
                AuthorName = book.AuthorName,
                GenreId = book.GenreId,
                Price = book.Price,
                Image = book.Image,
            };
            return bookdto;
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
           var books= await _BookRepo.GetBooks();
            return books;
        }

        public async Task UpdateBook(UpdateBookDTO book)
        {
            var newBook = new Book
            {
                Id = book.Id,
                BookName = book.BookName,
                AuthorName = book.AuthorName,
                GenreId = book.GenreId,
                Price = book.Price,
                Image = book.Image,
            };
            await _BookRepo.UpdateBook(newBook);
            

        }
    }
}
