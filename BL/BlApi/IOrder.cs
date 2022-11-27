
namespace BlApi;

public interface IOrder
{
    /// <summary>
    /// Gets a list of orders
    /// </summary>
    /// <returns>list of orders</returns>
    public IEnumerable<BO.OrderForList> GetOrderListForManager();

    /// <summary>
    /// get an order's datails
    /// </summary>
    /// <param name="orderID">the order's id of the order that you want to get its details</param>
    /// <returns> order</returns>
    /// <exception cref="BO.BoDoesNoExistException">order does not exist"</exception>

    public BO.Order GetOrderDetails(int orderID);

    /// <summary>
    /// Update the order ship date
    /// </summary>
    /// <param name="orderID">the order's id that you wants to update its ship date </param>
    /// <returns>ordre</returns>
    /// <exception cref="BO.DateAlreadyUpdatedException">ship date is already updated</exception>
    /// <exception cref="BO.BoDoesNoExistException">order does not exist</exception>
    public BO.Order UpdateOrderShip(int orderID);

    /// <summary>
    /// Update the order delivery date
    /// </summary>
    /// <param name="orderID">the order's id that you wants to update its delivery date</param>
    /// <returns>order</returns>
    /// <exception cref="BO.DateAlreadyUpdatedException">Delivery date is already updated</exception>
    /// <exception cref="BO.BoDoesNoExistException">order does not exist</exception>
    public BO.Order UpdateOrderDelivery(int orderID);

    /// <summary>
    /// Track the order
    /// </summary>
    /// <param name="orderID">the order's id that you wants to track</param>
    /// <returns>order tracking object</returns>
    /// <exception cref="BO.BoDoesNoExistException">order does not exist</exception>
    public BO.OrderTracking TrakingOrder(int orderID);
 
}
