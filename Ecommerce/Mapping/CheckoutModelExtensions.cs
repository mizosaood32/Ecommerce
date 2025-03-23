using BLL.DTO;
using Ecommerce.ActionRequest;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Ecommerce.Mapping
{
    public static class CheckoutModelExtensions
    {
        public static CheckoutModelDTO ToDto(this CheckoutModelActionRequest request)
        {
            return new CheckoutModelDTO
            {
                Name = request.Name,
                Email = request.Email,
                MobileNumber = request.MobileNumber,
                Address = request.Address,
                PaymentMethod = request.PaymentMethod
            };
        }
    }
}


//public string? Name { get; set; }

//[Required]
//[EmailAddress]
//[MaxLength(30)]
//public string? Email { get; set; }
//[Required]
//public string? MobileNumber { get; set; }
//[Required]
//[MaxLength(200)]
//public string? Address { get; set; }

//[Required]
//public string? PaymentMethod { get; set; }
//    }