namespace DAL.Constants;

public enum OrderStatus
{
    Pending = 1,
    Shipped,
    Delivered,
    Cancelled,
    Returned,
    Refunded
}