using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DalApi;
using DocumentFormat.OpenXml.Vml.Spreadsheet;

namespace BlImplementation;

internal class Order : BlApi.IOrder
{
    private DalApi.IDal Dal = new Dal.DalList();

    public IEnumerable<BO.OrderForList> GetOrderListForManager()
    {
        IEnumerable<BO.OrderForList> orderList = from order in Dal.Order.RequestAll()
                                                 let orderItems = Dal.OrderItem.RequestByOrderId(order.seqNum)
                                                 select new BO.OrderForList
                                                 {
                                                     ID = order.seqNum,
                                                     CustomerName = order.CustomerName,
                                                     AmountOfItems = orderItems.Count(),
                                                     Status = GetOrderDetails(order.seqNum).Status,
                                                     TotalPrice = orderItems.Sum(orderItem => orderItem.Price * orderItem.Amount),                                               
                                                 };
        return orderList;
        //throw new Exception();
    }
    private OrderStatus GetOrderStatus(DO.Order order)
    {
        if (order.OrderDate < DateTime.Now && order.ShipDate > DateTime.Now)
            return OrderStatus.confirmed;
        else if (order.ShipDate < DateTime.Now && order.DeliveryDate > DateTime.Now)
            return OrderStatus.shipped;
        else
            return OrderStatus.provided;
    }

    public BO.Order GetOrderDetails(int orderID)
    {
        // if(orderID <0)
        // throw

        IEnumerable<BO.OrderItem> orderItemList = from orderItem in Dal.OrderItem.RequestByOrderId(orderID)
                                                  select new BO.OrderItem
                                                  {
                                                      ID = orderItem.seqNum,
                                                      Name = Dal.Product.RequestById(orderItem.ProductID).Name,
                                                      ProductID = orderItem.ProductID,
                                                      Price = orderItem.Price,
                                                      //Amount = orderItem.Amount,
                                                      //TotalPrice = orderItem.Price * orderItem.Amount

                                                  };
        DO.Order  DOorder = Dal.Order.RequestById(orderID);
       
        BO.Order order = new BO.Order
        {
            ID = DOorder.seqNum,
            CustomerName = DOorder.CustomerName,
            CustomerEmail = DOorder.CustomerEmail,
            CustomerAdress = DOorder.CustomerAdress,
            Status = GetOrderStatus(DOorder),
            OrderDate = DOorder.OrderDate,
            DeliveryDate = DOorder.DeliveryDate,
            ShipDate = DOorder.ShipDate
        };


        throw new Exception();// זה בשביל עכשיו שלא יהיה טעות קומפילציה
    }
        
    public BO.OrderForList GetOrderListForCustomer()
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