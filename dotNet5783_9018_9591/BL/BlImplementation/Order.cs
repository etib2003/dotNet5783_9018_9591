using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;

namespace BlImplementation;

internal class Order : IOrder
{
    public BO.Order GetOrderDetails(int orderID)
    {
        throw new NotImplementedException();
    }

    public BO.OrderForList GetOrderListForCustomer()
    {
        throw new NotImplementedException();
    }

    public BO.OrderForList GetOrderListForManager()
    {
        throw new NotImplementedException();
    }

    public BO.Order TrakingOrder(int orderID)
    {
        throw new NotImplementedException();
        //Tuple<int,string> tuple=12, "exist";
    }

    public void UpdateOrder()
    {
        throw new NotImplementedException();
    }

    public BO.Order UpdateOrderDelivery(int orderID)
    {
        throw new NotImplementedException();
    }

    public BO.Order UpdateOrderShip(int orderID)
    {
        throw new NotImplementedException();
    }
}
