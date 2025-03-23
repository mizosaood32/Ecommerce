using System.Diagnostics;
using BLL.Contract;

using DAL.Repositories.Abstraction;
using Ecommerce.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DAL.Entities;
using Ecommerce.Models.DTOs;

namespace Ecommerce.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        //private readonly IHomeManager _homeManag;



        //public HomeController(ILogger<HomeController> logger, IHomeManager homeManag)
        //{
        //    _homeManag = homeManag;
        //    _logger = logger;
        //}





        //public async Task<IActionResult> Index(string sterm = "", int genreId = 0)
        //{
        //    Console.WriteLine($"Search Term: {sterm}, Genre ID: {genreId}");

        //    var books = await _homeManag.GetBooks(sterm, genreId);
        //    var genres = await _homeManag.Genres();
        //    BookDisplayModelVM bookModel = new BookDisplayModelVM
        //    {

        //        Books = books,
        //        Genres = genres,
        //        STerm = sterm,
        //        GenreId = genreId

        //    };

        //    return View(bookModel);
        //}


        //public IActionResult Privacy()
        //{
        //    return View();
        //}




        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ViewModel.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}


        private readonly ILogger<HomeController> _logger;
        private readonly IHomeRepository _homeRepository;

        public HomeController(ILogger<HomeController> logger, IHomeRepository homeRepository)
        {

            _homeRepository = homeRepository;
            _logger = logger;

        }

        public async Task<IActionResult> Index(string sterm = "", int genreId = 0)
        {
            IEnumerable<Book> books = await _homeRepository.GetBooks(sterm, genreId);
            IEnumerable<Genre> genres = await _homeRepository.Genres();
            BookDisplayModel bookModel = new BookDisplayModel
            {
                Books = books,
                Genres = genres,
                STerm = sterm,
                GenreId = genreId
            };

            return View(bookModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

    

