using BO;
using DocumentFormat.OpenXml.Office2013.Word;
using DocumentFormat.OpenXml.Wordprocessing;
using OtherFunctions;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;

internal class Order : BlApi.IOrder
{
    private DO.IDal? dal = DO.Factory.Get();

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<BO.OrderForList> GetOrderListForManager()
    {
        try
        {
            IEnumerable<DO.Order?> doOrderList = dal!.Order.RequestAll();//gets all the orders from the data layer
            return doOrderList.Select(order =>
            {
                BO.OrderForList boOrderForList = order.CopyPropTo(new BO.OrderForList());
                boOrderForList.Status = getOrderStatus(order);
                var oiOfOrder = dal?.OrderItem.RequestAll(x => x?.OrderID == boOrderForList.Id);
                boOrderForList.AmountOfItems = oiOfOrder!.Count();
                boOrderForList.TotalPrice = oiOfOrder!.Sum(orderItem => orderItem?.Price * orderItem?.Amount) ?? 0;//calculate the total price
                return boOrderForList;
            });
        }
        catch (DO.DalDoesNoExistException ex)//catches the exception from the data layer
        {
            throw new BO.BoDoesNoExistException("Data exception:", ex);
        }
    }

    /// <summary>
    ///  help function to calculate the order's status
    /// </summary>
    /// <param name="order">the order you wants to update its status</param>
    /// <returns></returns>
    private BO.OrderStatus getOrderStatus(DO.Order? order)
    {
        return order switch
        {
            DO.Order _order when _order.DeliveryDate != null => BO.OrderStatus.provided,
            DO.Order _order when _order.ShipDate != null => BO.OrderStatus.shipped,
            _ => BO.OrderStatus.confirmed,
        };
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    /// <summary>
    /// Help function that gets an order from the data layer
    /// </summary>
    /// <param name="doOrder">an order from the data layer</param>
    /// <returns></returns>
    private BO.Order getBoOrder(DO.Order doOrder)
    {
        try
        {
            BO.Order boOrder = doOrder.CopyPropTo(new BO.Order());
            boOrder.Status = getOrderStatus(doOrder);

            IEnumerable<DO.OrderItem?> orderItemsList = dal?.OrderItem.RequestAll(x => x?.OrderID == doOrder.Id)!;
            boOrder.OrderItems = (from orderItem in orderItemsList
                                  select orderItem.CopyPropTo(new BO.OrderItem()
                                  {
                                      TotalPrice = (orderItem?.Price ?? 0) * (orderItem?.Amount ?? 0),
                                      Name = dal?.Product.GetById(orderItem?.ProductID ?? 0).Name,
                                  })
                                  ).ToList();
            boOrder.TotalPrice = boOrder.OrderItems.Sum(OrderItem => OrderItem.TotalPrice);
            return boOrder;
        }
        catch (DO.DalDoesNoExistException ex)//catches the exception from the data layer
        {
            throw new BO.BoDoesNoExistException("Data exception:", ex);
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Order GetOrderDetails(int orderID)
    {
        try
        {
            orderID.negativeNumber();//exception
            return getBoOrder(dal?.Order.GetById(orderID) ?? default); //gets the right order using its id,call the help function
        }
        catch (DO.DalDoesNoExistException ex)//catches the exception from the data layer
        {
            throw new BO.BoDoesNoExistException("Data exception:", ex);
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Order UpdateOrderShip(int orderID)
    {
        try
        {
            orderID.negativeNumber();//exception

            DO.Order doOrder = dal?.Order.GetById(orderID) ?? default;//gets the order by using its id

            BO.Order order = new BO.Order();//create a new order of the logical layer
            if (doOrder.OrderDate != null && doOrder.ShipDate == null)
            {
                doOrder.ShipDate = DateTime.Now;
                dal?.Order.Update(doOrder);
                order = getBoOrder(doOrder);//call the help function
            }
            else
            {
                throw new BO.DateAlreadyUpdatedException("ship date is already updated");//exception
            }
            return order;
        }
        catch (DO.DalDoesNoExistException ex)//catches the exception from the data layer
        {
            throw new BO.BoDoesNoExistException("Data exception:", ex);
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Order UpdateOrderDelivery(int orderID)
    {
        try
        {
            orderID.negativeNumber();//exception

            DO.Order doOrder = dal?.Order.GetById(orderID) ?? default;//gets the order by using its id

            BO.Order order = new BO.Order();//create a new order of the logical layer
            if (doOrder.ShipDate != null && doOrder.DeliveryDate == null)
            {
                doOrder.DeliveryDate = DateTime.Now;
                dal?.Order.Update(doOrder);
                order = getBoOrder(doOrder);//call the help function
            }
            else if (doOrder.OrderDate != null && doOrder.ShipDate == null)
            {
                throw new BO.DateHasNotUpdatedYetException("Ship date has not updated yet");//exception
            }
            else
            {
                throw new BO.DateAlreadyUpdatedException("Delivery date is already updated");//exception
            }
            return order;
        }
        catch (DO.DalDoesNoExistException ex)//catches the exception from the data layer
        {
            throw new BO.BoDoesNoExistException("Data exception:", ex);
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.OrderTracking TrackingOrder(int orderID)
    {
        try
        {
            orderID.negativeNumber();//exception

            DO.Order doOrder = dal?.Order.GetById(orderID) ?? default;//gets the right order by using its id
            List<Tuple<DateTime?, string>> tupleList = new List<Tuple<DateTime?, string>>();
            Tuple<DateTime?, string> tuple;
            BO.OrderTracking orderTracking = new BO.OrderTracking();//create a new OrderTracking object
            if (doOrder.OrderDate != null) //The order has been confirmed
            {
                tuple = new(doOrder.OrderDate, "The order has been confirmed");
                tupleList.Add(tuple);
                if (doOrder.ShipDate != null)//The order has been shipped
                {
                    tuple = new(doOrder.ShipDate, "The order has been shipped");
                    tupleList.Add(tuple);

                    if (doOrder.DeliveryDate != null)//The order has been provided
                    {
                        tuple = new(doOrder.DeliveryDate, "The order has been provided");
                        tupleList.Add(tuple);
                    }

                }
            }
            //Initializes the data  
            orderTracking.OrderProgress = tupleList!;
            orderTracking.Id = orderID;
            orderTracking.Status = getOrderStatus(doOrder);//call the help function

            return orderTracking;

        }
        catch (DO.DalDoesNoExistException ex)//catches the exception from the data layer
        {
            throw new BO.BoDoesNoExistException("Data exception:", ex);
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public OrderForList GetOrderForList(int orderId)
    {
        try
        {
            DO.Order? doOrderForList = dal?.Order.GetById(orderId);//gets all the orders from the data layer

            BO.OrderForList orderForList = doOrderForList.CopyPropTo(new OrderForList());
            orderForList.Status = getOrderStatus(doOrderForList);
            var oiOfOrder = dal?.OrderItem.RequestAll(x => x?.OrderID == orderForList.Id);
            orderForList.AmountOfItems = oiOfOrder!.Count();
            orderForList.TotalPrice = oiOfOrder!.Sum(orderItem => orderItem?.Price * orderItem?.Amount) ?? 0;
            return orderForList;
        }
        catch (DO.DalDoesNoExistException ex)//catches the exception from the data layer
        {
            throw new BO.BoDoesNoExistException("Data exception:", ex);
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<OrderStatistics> GroupByStatistics()
    {
        try
        {
            return from order in dal?.Order.RequestAll()
                   group order by getOrderStatus(order) into newGroup
                   select new OrderStatistics
                   {
                       OrderStatus = newGroup.Key,
                       CountPerStatus = newGroup.Count()
                   };
        }
        catch (Exception)
        {
            throw new Exception("Failed to divide into groups");
        }
    }

    //הפונקציה מחזירה את האחרונה שלא טופלה
    //ובסוף זה יהיה נאל
    //עבור סימולטור
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int? GetOldestOrder()
    {
        try
        {
            var orders = dal?.Order.RequestAll(x => x?.DeliveryDate is null);
            return orders!.Any() ? orders!.MinBy(x => x?.ShipDate is null ? x?.OrderDate : x?.ShipDate)?.Id : null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

public class OrderStatistics
{
    public OrderStatus OrderStatus { get; set; }
    public int CountPerStatus { get; set; }
}