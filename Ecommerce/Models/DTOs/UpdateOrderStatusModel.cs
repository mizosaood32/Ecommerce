using DAL.Constants;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.DTOs;

public class UpdateOrderStatusModel
{
    public int OrderId { get; set; }

    [Required]
    public OrderStatus OrderStatus { get; set; }

    public IEnumerable<SelectListItem>? OrderStatusList { get; set; }
}
