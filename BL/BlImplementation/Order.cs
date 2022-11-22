﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using DalApi;
using DocumentFormat.OpenXml.Vml.Spreadsheet;

namespace BlImplementation;

internal class Order : IOrder
{
    private DalApi.IDal Dal = new Dal.DalList();

    public IEnumerable<BO.OrderForList> GetOrderListForManager()
    {
        IEnumerable<BO.OrderForList> orderList = from order in Dal.Order.RequestAll()
                                                 select new BO.OrderForList
                                                 {
                                                     ID = order.seqNum,
                                                     CustomerName = order.CustomerName,
                                                     Status = order.Status,
                                                     AmountOfItems = order.AmountOfItems,
                                                     TotalPrice = order.TotalPrice,
                                                 };
        return orderList;
        //throw new Exception();

    }
    public BO.Order GetOrderDetails(int orderID)
    {
        // if(orderID <0)
        // throw
        DO.Order DOorder = Dal.Order.RequestById(orderID);
        // BO.Order
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