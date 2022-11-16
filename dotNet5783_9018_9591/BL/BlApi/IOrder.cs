using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

internal interface IOrder
{
    public OrderForList GetOrderListForManager();
    public Order GetOrderDetails(int orderID);
    public Order UpdateOrderShip(int orderID);
    public Order UpdateOrderDelivery(int orderID);
    public Order TrakingOrder(int orderID);
    public void UpdateOrder();//בונוס
    public OrderForList GetOrderListForCustomer();
}
