 using OtherFunctions;

internal class Order : BlApi.IOrder
{
    private DalApi.IDal _dal = new Dal.DalList();

    public IEnumerable<BO.OrderForList> GetOrderListForManager()
    { 
            IEnumerable<DO.Order> orderItemsList = _dal.Order.RequestAll();//gets all the orders from the data layer
            return from order in orderItemsList
                   let orderItems = _dal.OrderItem.RequestByOrderId(order.Id)//gets the right order using its seqnum
                   //Initializes the data
                   select new BO.OrderForList
                   {
                       ID = order.Id,
                       CustomerName = order.CustomerName,
                       AmountOfItems = orderItems.Count(),
                       Status = getOrderStatus(order),
                       TotalPrice = orderItems.Sum(orderItem => orderItem.Price * orderItem.Amount),//calculate the total price
                   };   
    }

    /// <summary>
    ///  help function to calculate the order's status
    /// </summary>
    /// <param name="order">the order you wants to update its status</param>
    /// <returns></returns>
    private BO.OrderStatus getOrderStatus(DO.Order order)
    {
        return order switch
        {
            DO.Order _order when _order.DeliveryDate != null => BO.OrderStatus.provided,
            DO.Order _order when _order.ShipDate != null => BO.OrderStatus.shipped,
            _ => BO.OrderStatus.confirmed,
        };
    }

    /// <summary>
    /// help function that gets an order from the data layer
    /// </summary>
    /// <param name="doOrder">an order from the data layer</param>
    /// <returns></returns>
    private BO.Order getBoOrder(DO.Order doOrder)  
    {
        BO.Order boOrder = new BO.Order //create a new order of the logical layer
        {
            //Initializes the data
            ID = doOrder.Id,
            CustomerName = doOrder.CustomerName,
            CustomerEmail = doOrder.CustomerEmail,
            CustomerAdress = doOrder.CustomerAdress,
            Status = getOrderStatus(doOrder),
            OrderDate = doOrder.OrderDate,
            ShipDate = doOrder.ShipDate,
            DeliveryDate = doOrder.DeliveryDate
        };

        IEnumerable<DO.OrderItem> orderItemsList = _dal.OrderItem.RequestByOrderId(doOrder.Id);
        boOrder.OrderItems = (from orderItem in orderItemsList
                            select new BO.OrderItem
                            {
                                //Initializes the data for each order item
                                Id = orderItem.Id,
                                Name = _dal.Product.RequestById(orderItem.ProductID).Name,
                                ProductID = orderItem.ProductID,
                                Price = orderItem.Price,
                                Amount = orderItem.Amount,
                                TotalPrice = orderItem.Price * orderItem.Amount

                            }).ToList();

        boOrder.TotalPrice = orderItemsList.Sum(orderItem => orderItem.Price * orderItem.Amount);//calculate the order's total price
        return boOrder;
    }

    public BO.Order GetOrderDetails(int orderID)
    {
        try
        {
            orderID.negativeNumber();//exception
    
            DO.Order DOorder = _dal.Order.RequestById(orderID);//gets the right order using its id

            BO.Order order = getBoOrder(DOorder);//call the help function

            return order;
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
            orderTracking.ID = orderID;
            orderTracking.Status = getOrderStatus(doOrder);//call the help function
            
            return orderTracking;

         }
        catch (DalApi.DalDoesNoExistException ex)//catches the exception from the data layer
        {
            throw new BO.BoDoesNoExistException("Data exception:", ex);
        }
    }
}

