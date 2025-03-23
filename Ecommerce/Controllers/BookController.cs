using BLL.Contract;
using BLL.DTO;
using DAL.Constants;
using Ecommerce.ActionRequest;
using Ecommerce.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookShoppingCartMvcUI.Controllers;

[Authorize(Roles = nameof(Roles.Admin))]
public class BookController : Controller
{
    private readonly IBookManger _bookMan;
    private readonly IGenereManager _genreMan;
    private readonly IFileService _fileService;

    public BookController(IBookManger bookRepo, IGenereManager genreRepo, IFileService fileService)
    {
        _bookMan = bookRepo;
        _genreMan = genreRepo;
        _fileService = fileService;
    }

    public async Task<IActionResult> Index()
    {
        var books = await _bookMan.GetBooks();
        return View(books);
    }

    public async Task<IActionResult> AddBook()
    {
        var genreSelectList = (await _genreMan.GetGenres()).Select(genre => new SelectListItem
        {
            Text = genre.GenreName,
            Value = genre.Id.ToString(),
        });
        BookDTO bookToAdd = new() { GenreList = genreSelectList };
        return View(bookToAdd);
    }

    [HttpPost]
    public async Task<IActionResult> AddBook(CreatActionBookDTO bookToAdd)
    {
        var genreSelectList = (await _genreMan.GetGenres()).Select(genre => new SelectListItem
        {
            Text = genre.GenreName,
            Value = genre.Id.ToString(),
        });
        bookToAdd.GenreList = genreSelectList;

        if (!ModelState.IsValid)
            return View(bookToAdd);

        try
        {
            if (bookToAdd.ImageFile != null)
            {
                if(bookToAdd.ImageFile.Length> 1 * 1024 * 1024)
                {
                    throw new InvalidOperationException("Image file can not exceed 1 MB");
                }
                string[] allowedExtensions = [".jpeg",".jpg",".png"];
                string imageName=await _fileService.SaveFile(bookToAdd.ImageFile, allowedExtensions);
                bookToAdd.Image = imageName;
            }
            // manual mapping of BookDTO -> Book
            BookDTO book = new()
            {
                Id = bookToAdd.Id,
                BookName = bookToAdd.BookName,
                AuthorName = bookToAdd.AuthorName,
                Image = bookToAdd.Image,
                GenreId = bookToAdd.GenreId,
                Price = bookToAdd.Price
            };
            await _bookMan.AddBook(book);
            TempData["successMessage"] = "Book is added successfully";
            return RedirectToAction(nameof(AddBook));
        }
        catch (InvalidOperationException ex)
        {
            TempData["errorMessage"]= ex.Message;
            return View(bookToAdd);
        }
        catch (FileNotFoundException ex)
        {
            TempData["errorMessage"] = ex.Message;
            return View(bookToAdd);
        }
        catch (Exception ex)
        {
            TempData["errorMessage"] = "Error on saving data";
            return View(bookToAdd);
        }
    }

    public async Task<IActionResult> UpdateBook(int id)
    {
        var book = await _bookMan.GetBookById(id);
        if(book==null)
        {
            TempData["errorMessage"] = $"Book with the id: {id} does not found";
            return RedirectToAction(nameof(Index));
        }
        var genreSelectList = (await _genreMan.GetGenres()).Select(genre => new SelectListItem
        {
            Text = genre.GenreName,
            Value = genre.Id.ToString(),
            Selected=genre.Id==book.GenreId
        });
        BookVM bookToUpdate = new() 
        {
            Id = book.Id,
            GenreList = genreSelectList,
            BookName=book.BookName,
            AuthorName=book.AuthorName,
            GenreId=book.GenreId,
            Price=book.Price,
            Image=book.Image 
        };
        return View(bookToUpdate);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateBook(UpdateActionBookDTO bookToUpdate)
    {
        var genreSelectList = (await _genreMan.GetGenres()).Select(genre => new SelectListItem
        {
            Text = genre.GenreName,
            Value = genre.Id.ToString(),
            Selected=genre.Id==bookToUpdate.GenreId
        });
        bookToUpdate.GenreList = genreSelectList;// fiha error;


        if (!ModelState.IsValid)
            return View(bookToUpdate);

        try
        {
            string oldImage = "";
            if (bookToUpdate.ImageFile != null)
            {
                if (bookToUpdate.ImageFile.Length > 1 * 1024 * 1024)
                {
                    throw new InvalidOperationException("Image file can not exceed 1 MB");
                }
                string[] allowedExtensions = [".jpeg", ".jpg", ".png"];
                string imageName = await _fileService.SaveFile(bookToUpdate.ImageFile, allowedExtensions);
                // hold the old image name. Because we will delete this image after updating the new
                oldImage = bookToUpdate.Image;
                bookToUpdate.Image = imageName;
            }
            // manual mapping of BookDTO -> Book
            UpdateBookDTO book = new()
            {
                Id=bookToUpdate.Id,
                BookName = bookToUpdate.BookName,
                AuthorName = bookToUpdate.AuthorName,
                GenreId = bookToUpdate.GenreId,
                Price = bookToUpdate.Price,
                Image = bookToUpdate.Image
            };
            await _bookMan.UpdateBook(book);
            // if image is updated, then delete it from the folder too
            if(!string.IsNullOrWhiteSpace(oldImage))
            {
                _fileService.DeleteFile(oldImage);
            }
            TempData["successMessage"] = "Book is updated successfully";
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            TempData["errorMessage"] = ex.Message;
            return View(bookToUpdate);
        }
        catch (FileNotFoundException ex)
        {
            TempData["errorMessage"] = ex.Message;
            return View(bookToUpdate);
        }
        catch (Exception ex)
        {
            TempData["errorMessage"] = "Error on saving data";
            return View(bookToUpdate);
        }
    }

    //public async Task<IActionResult> DeleteBook(int id)
    //{
    //    try
    //    {
    //        var book = await _bookMan.GetBookById(id);
    //        if (book == null)
    //        {
    //            TempData["errorMessage"] = $"Book with the id: {id} does not found";
    //        }
    //        else
    //        {
    //            await _bookMan.DeleteBook(book);
    //            if (!string.IsNullOrWhiteSpace(book.Image))
    //            {
    //                _fileService.DeleteFile(book.Image);
    //            }
    //        }
    //    }
    //    catch (InvalidOperationException ex)
    //    {
    //        TempData["errorMessage"] = ex.Message;
    //    }
    //    catch (FileNotFoundException ex)
    //    {
    //        TempData["errorMessage"] = ex.Message;
    //    }
    //    catch (Exception ex)
    //    {
    //        TempData["errorMessage"] = "Error on deleting the data";
    //    }
    //    return RedirectToAction(nameof(Index));
    //}

    public async Task<IActionResult> DeleteBook(int id)
    {
        try
        {
            var book = await _bookMan.GetBookById(id); // ✅ This should now return AsNoTracking()

            if (book == null)
            {
                TempData["errorMessage"] = $"Book with ID {id} not found.";
            }
            else
            {
                await _bookMan.DeleteBook(book); // ✅ Calls updated delete method

                if (!string.IsNullOrWhiteSpace(book.Image))
                {
                    _fileService.DeleteFile(book.Image);
                }

                TempData["successMessage"] = "Book deleted successfully.";
            }
        }
        catch (InvalidOperationException ex)
        {
            TempData["errorMessage"] = ex.Message;
        }
        catch (FileNotFoundException ex)
        {
            TempData["errorMessage"] = ex.Message;
        }
        catch (Exception)
        {
            TempData["errorMessage"] = "Error deleting the book.";
        }

        return RedirectToAction(nameof(Index));
    }

}
