﻿
using BO;
using System.Numerics;
using static Order;

namespace BlApi;

public interface IOrder
{
    /// <summary>
    /// Gets a list of orders
    /// </summary>
    /// <returns>list of orders</returns>
    public IEnumerable<BO.OrderForList?> GetOrderListForManager();

    /// <summary>
    /// Get orders grouping by dates
    /// </summary>
    /// <returns>Orders grouping by dates</returns>
    public IEnumerable<OrderStatistics> GroupByStatistics();

    /// <summary>
    /// get an order's datails
    /// </summary>
    /// <param name="orderID">the order's id of the order that you want to get its details</param>
    /// <returns> order</returns>
    /// <exception cref="order does not exist"></exception>

    public OrderForList GetOrderForList(int orderId);

    /// <summary>
    /// Get Order Details
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns>BO.Order Order Details(</returns>
    public BO.Order GetOrderDetails(int orderID);

    /// <summary>
    /// Update the order ship date
    /// </summary>
    /// <param name="orderID">the order's id that you wants to update its ship date </param>
    /// <returns>ordre</returns>
    /// <exception cref="ship date is already updated"></exception>
    /// <exception cref="order does not exist"></exception>
    public BO.Order UpdateOrderShip(int orderID);

    /// <summary>
    /// Update the order delivery date
    /// </summary>
    /// <param name="orderID">the order's id that you wants to update its delivery date</param>
    /// <returns>order</returns>
    /// <exception cref="Delivery date is already updated"></exception>
    /// <exception cref="order does not exist"></exception>
    public BO.Order UpdateOrderDelivery(int orderID);

    /// <summary>
    /// Track the order
    /// </summary>
    /// <param name="orderID">the order's id that you wants to track</param>
    /// <returns>order tracking object</returns>
    /// <exception cref="order does not exist"></exception>
    public BO.OrderTracking TrackingOrder(int orderID);

    /// <summary>
    /// Returns the order that has not been updated the longest
    /// </summary>
    /// <returns>id of the order that has not been updated the longest</returns>
    public int? GetOldestOrder();
}
