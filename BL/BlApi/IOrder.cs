
namespace BlApi;

public interface IOrder
{
    public IEnumerable<BO.OrderForList> GetOrderListForManager();
    public BO.Order GetOrderDetails(int orderID);

    public BO.Order UpdateOrderShip(int orderID);
    public BO.Order UpdateOrderDelivery(int orderID);

    public BO.OrderTracking TrakingOrder(int orderID);
    //public void UpdateOrder();//בונוס
}
