using Do;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IOrder
{
    public BO.OrderForList GetOrderListForManager();
    public BO.Order GetOrderDetails(int orderID);
    public BO.Order UpdateOrderShip(int orderID);
    public BO.Order UpdateOrderDelivery(int orderID);
    public BO.Order TrakingOrder(int orderID);
    public void UpdateOrder();//בונוס
    public BO.OrderForList GetOrderListForCustomer();
}
