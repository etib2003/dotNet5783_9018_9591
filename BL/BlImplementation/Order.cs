using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using DalApi;

namespace BlImplementation;

internal class Order : IOrder
{
    private DalApi.IDal Dal = new Dal.DalList();
    public BO.Order GetOrderDetails(int orderID)
    {
        // if(orderID <0)
        // throw
        DO.Order DOorder = Dal.Order.RequestById(orderID);
        BO.Order
    }
        


    public BO.OrderForList GetOrderListForCustomer()
    {
        throw new NotImplementedException();
    }

    public BO.OrderForList GetOrderListForManager() //להמשיך
    {
        IEnumerable<Do.OrderForList> orderList = from order in Dal.Order.RequestAll()
                                                 select new Do.OrderForList
                                                 {
                                                     ID = order.seqNum,
                                                     CustomerName= order.CustomerName,
                                                      Status=order.,
                                              
                                                 };
        return productList;


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
