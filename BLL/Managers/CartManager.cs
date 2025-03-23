using BLL.Contract;
using BLL.DTO;
using DAL.Constants;
using DAL.Entities;
using DAL.Repositories.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Managers
{
    public class CartManager:ICartManager
    {
        private readonly ICartRepository _cartRepo; //  = new ProductRepository();
        public CartManager(ICartRepository cartRepo)
        {
            _cartRepo = cartRepo;
        }

        public async Task<int> AddItem(int bookId, int qty)
        {
           var itemcount = await _cartRepo.AddItem(bookId, qty);
            return itemcount;
        }

        public Task<bool> DoCheckout(CheckoutModelDTO model)
        {
            checkoutModel CheckoutModel = new checkoutModel
            {
                PaymentMethod = model.PaymentMethod,
                Name = model.Name,
                Email = model.Email,
                MobileNumber = model.MobileNumber,
                Address = model.Address
            };
             var exist = _cartRepo.DoCheckout(CheckoutModel);
            return exist;
            //          public string Name { get; set; }
            //public string Email { get; set; }
            //public string MobileNumber { get; set; }
            //public string PaymentMethod { get; set; }
            //public string Address { get; set; }

        }

        public Task<ShoppingCart> GetCart(string userId)
        {

            var cart = _cartRepo.GetCart(userId);
            return cart;



        }

        public Task<int> GetCartItemCount(string userId = "")
        {
            var cartcount = _cartRepo.GetCartItemCount(userId);
            return cartcount;

        }

        public Task<ShoppingCart> GetUserCart()
        {


            var cartcount = _cartRepo.GetUserCart();
            return cartcount;


        }

        public Task<int> RemoveItem(int bookId)
        {

            var itemcount = _cartRepo.RemoveItem(bookId);
            return itemcount;
        }
    }
}
