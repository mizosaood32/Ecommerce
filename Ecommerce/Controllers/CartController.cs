using BLL.Contract;
using DAL.Constants;
using Ecommerce.ActionRequest;
using Ecommerce.Mapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShoppingCartMvcUI.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartManager _cartMana;
        private readonly ILogger<CartController> _logger;

        public CartController(ICartManager cartmana, ILogger<CartController> logger)
        {
            _cartMana = cartmana;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> AddItem(int bookId, int qty = 1, int redirect = 0)
        {
            try
            {
                var cartCount = await _cartMana.AddItem(bookId, qty);
                if (redirect == 0)
                    return Ok(cartCount);
            }
            catch (Exception ex)
            {
                TempData[NotificationType.ErrorMessage] = "Something went wrong!!";
                _logger.LogError(ex.Message);
            }
            return RedirectToAction("GetUserCart");
        }

        public async Task<IActionResult> RemoveItem(int bookId)
        {
            var cartCount = await _cartMana.RemoveItem(bookId);
            return RedirectToAction("GetUserCart");
        }
        public async Task<IActionResult> GetUserCart()
        {
            var cart = await _cartMana.GetUserCart();
            return View(cart);
        }

        public async Task<IActionResult> GetTotalItemInCart()
        {
            int cartItem = await _cartMana.GetCartItemCount();
            return Ok(cartItem);
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutModelActionRequest model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var modelDTO = model.ToDto();
            bool isCheckedOut = await _cartMana.DoCheckout(modelDTO);
            if (!isCheckedOut)
                return RedirectToAction(nameof(OrderFailure));
            return RedirectToAction(nameof(OrderSuccess));
        }

        public IActionResult OrderSuccess()
        {
            return View();
        }

        public IActionResult OrderFailure()
        {
            return View();
        }

    }
}
