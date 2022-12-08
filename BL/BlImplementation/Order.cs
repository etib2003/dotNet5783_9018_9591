 using OtherFunctions;

internal class Order : BlApi.IOrder
{
    private DalApi.IDal _dal = new Dal.DalList();

    public IEnumerable<BO.OrderForList> GetOrderListForManager()
    { 
            IEnumerable<DO.Order?> orderList = _dal.Order.RequestAll();//gets all the orders from the data layer
            return from order in orderList
                   let orderItems = _dal.OrderItem.RequestAll(x => x?.OrderID == order?.Id)//gets the right order using its seqnum
                   //Initializes the data
                   select new BO.OrderForList
                   {
                       Id = order?.Id??0,
                       CustomerName = order?.CustomerName,
                       AmountOfItems = orderItems.Count(),
                       Status = getOrderStatus(order),
                       TotalPrice = orderItems.Sum(orderItem => orderItem?.Price * orderItem?.Amount)??0,//calculate the total price
                   };   
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

    /// <summary>
    /// Help function that gets an order from the data layer
    /// </summary>
    /// <param name="doOrder">an order from the data layer</param>
    /// <returns></returns>
    private BO.Order getBoOrder(DO.Order doOrder)  
    {
        BO.Order boOrder = new BO.Order //create a new order of the logical layer
        {
            //Initializes the data
            Id = doOrder.Id,
            CustomerName = doOrder.CustomerName,
            CustomerEmail = doOrder.CustomerEmail,
            CustomerAdress = doOrder.CustomerAdress,
            Status = getOrderStatus(doOrder),
            OrderDate = doOrder.OrderDate,
            ShipDate = doOrder.ShipDate,
            DeliveryDate = doOrder.DeliveryDate
        };

        IEnumerable<DO.OrderItem?> orderItemsList = _dal.OrderItem.RequestAll(x => x!.Value.OrderID == doOrder.Id);
        boOrder.OrderItems = (from orderItem in orderItemsList
                            select new BO.OrderItem
                            {
                                //Initializes the data for each order item
                                Id = orderItem?.Id??0,
                                Name = _dal.Product.RequestById(orderItem.Value.ProductID).Name,
                                ProductID = orderItem?.ProductID??0,
                                Price = orderItem?.Price??0,
                                Amount = orderItem?.Amount??0,
                                TotalPrice = orderItem?.Price??0 * orderItem?.Amount??0

                            }).ToList();

        boOrder.TotalPrice = orderItemsList.Sum(orderItem => orderItem!.Value.Price * orderItem.Value.Amount);//calculate the order's total price
        return boOrder;
    }

    public BO.Order GetOrderDetails(int orderID)
    {
        try
        {
            orderID.negativeNumber();//exception
    
            return getBoOrder(_dal.Order.RequestById(orderID)); //gets the right order using its id,call the help function
        }
        catch (DalApi.DalDoesNoExistException ex)//catches the exception from the data layer
        {
            throw new BO.BoDoesNoExistException("Data exception:", ex);
        }
    }

    
    public BO.Order UpdateOrderShip(int orderID)
    {
        try
        {
            orderID.negativeNumber();//exception

            DO.Order doOrder = _dal.Order.RequestById(orderID);//gets the order by using its id

            BO.Order order = new BO.Order();//create a new order of the logical layer
            if (doOrder.OrderDate != null && doOrder.ShipDate == null)
            {
                doOrder.ShipDate = DateTime.Now;
                _dal.Order.Update(doOrder);
                order = getBoOrder(doOrder);//call the help function
            }
            else
            {            
                throw new BO.DateAlreadyUpdatedException("ship date is already updated");//exception
            }
            return order;
        }
        catch (DalApi.DalDoesNoExistException ex)//catches the exception from the data layer
        {
            throw new BO.BoDoesNoExistException("Data exception:", ex);
        }
    }

    public BO.Order UpdateOrderDelivery(int orderID)
    {
        try
        {
            orderID.negativeNumber();//exception

            DO.Order doOrder = _dal.Order.RequestById(orderID);//gets the order by using its id

            BO.Order order = new BO.Order();//create a new order of the logical layer
            if (doOrder.ShipDate != null && doOrder.DeliveryDate == null)
            {
                doOrder.DeliveryDate = DateTime.Now;
                _dal.Order.Update(doOrder);
                order = getBoOrder(doOrder);//call the help function
            }
            else if (doOrder.OrderDate!= null && doOrder.ShipDate == null)
            {
                throw new BO.DateHasNotUpdatedYetException("Ship date has not updated yet");//exception
            }
            else
            {
                throw new BO.DateAlreadyUpdatedException("Delivery date is already updated");//exception
            }
            return order;
        }
        catch (DalApi.DalDoesNoExistException ex)//catches the exception from the data layer
        {
            throw new BO.BoDoesNoExistException("Data exception:", ex);
        }
    }
   
    public BO.OrderTracking TrackingOrder(int orderID)
    {
        try
        {
            orderID.negativeNumber();//exception

            DO.Order doOrder = _dal.Order.RequestById(orderID);//gets the right order by using its id
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
            orderTracking.OrderProgress = tupleList;
            orderTracking.Id = orderID;
            orderTracking.Status = getOrderStatus(doOrder);//call the help function
            
            return orderTracking;

         }
        catch (DalApi.DalDoesNoExistException ex)//catches the exception from the data layer
        {
            throw new BO.BoDoesNoExistException("Data exception:", ex);
        }
    }
}

