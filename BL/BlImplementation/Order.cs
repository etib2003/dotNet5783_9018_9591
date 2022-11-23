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
                                                     Status = GetOrderStatus(order),
                                                     TotalPrice = orderItems.Sum(orderItem => orderItem.Price * orderItem.Amount),
                                                 };
        return orderList;
        //throw new Exception();
    }
    private OrderStatus GetOrderStatus(DO.Order order)
    {
        return order.DeliveryDate != DateTime.MinValue ? OrderStatus.provided : order.ShipDate != DateTime.MinValue ?
        OrderStatus.shipped : OrderStatus.confirmed;
    }

    public BO.Order GetOrderDetails(int orderID)
    {
        // if(orderID <0)
        // throw
        DO.Order DOorder = Dal.Order.RequestById(orderID);

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
        IEnumerable<DO.OrderItem> orderItemsList = Dal.OrderItem.RequestByOrderId(orderID);
        order.OrderItems = (from orderItem in orderItemsList
                            select new BO.OrderItem
                            {
                                OrderID = orderItem.seqNum,
                                Name = Dal.Product.RequestById(orderItem.ProductID).Name,
                                ProductID = orderItem.ProductID,
                                Price = orderItem.Price,
                                Amount = orderItem.Amount,
                                TotalPrice = orderItem.Price * orderItem.Amount

                            }).ToList();//orderitemsהמרה לרשימה כדי שיכנס ל 


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