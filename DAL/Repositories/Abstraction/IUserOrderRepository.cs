using DAL.Constants;
using DAL.Entities;

namespace DAL.Repositories.Abstraction;

public interface IUserOrderRepository
{
    Task<IEnumerable<Order>> UserOrders(bool getAll = false);
    //Task ChangeOrderStatus(UpdateOrderStatusModel data);
    Task TogglePaymentStatus(int orderId);
    Task<Order?> GetOrderById(int id);
    
    //Task<IEnumerable<OrderStatus>> GetOrderStatuses();

}