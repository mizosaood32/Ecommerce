//using BookShoppingCartMvcUI.Models;

using DAL.Entities;

namespace Ecommerce.ViewModel
{
    public class BookDisplayModelVM
    {
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public string STerm { get; set; } = "";
        public int GenreId { get; set; } = 0;

    }


}
