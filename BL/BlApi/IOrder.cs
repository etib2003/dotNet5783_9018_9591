
namespace BlApi;

public interface IOrder
{
    /// <summary>
    /// Gets a list of orders
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.OrderForList> GetOrderListForManager();

    /// <summary>
    ///  help function to calculate the order's status
    /// </summary>
    /// <param name="order">the order you wants to update its status</param>
    /// <returns></returns>
    public BO.Order GetOrderDetails(int orderID);


    public BO.Order UpdateOrderShip(int orderID);
    public BO.Order UpdateOrderDelivery(int orderID);

    public BO.OrderTracking TrakingOrder(int orderID);
    //public void UpdateOrder();//בונוס
}
