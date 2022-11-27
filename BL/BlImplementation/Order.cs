 
using OtherFunctions;
 

internal class Order : BlApi.IOrder
{
    private DalApi.IDal _dal = new Dal.DalList();

    public IEnumerable<BO.OrderForList> GetOrderListForManager()
    { 
            IEnumerable<DO.Order> orderItemsList = _dal.Order.RequestAll();
            return from order in orderItemsList
                   let orderItems = _dal.OrderItem.RequestByOrderId(order.seqNum)
                   select new BO.OrderForList
                   {
                       ID = order.seqNum,
                       CustomerName = order.CustomerName,
                       AmountOfItems = orderItems.Count(),
                       Status = getOrderStatus(order),
                       TotalPrice = orderItems.Sum(orderItem => orderItem.Price * orderItem.Amount),
                   }; 
        
      
    }

    private BO.OrderStatus getOrderStatus(DO.Order order)
    {
        return order switch
        {
            DO.Order _order when _order.DeliveryDate != null => BO.OrderStatus.provided,
            DO.Order _order when _order.ShipDate != null => BO.OrderStatus.shipped,
            _ => BO.OrderStatus.confirmed,
        };
    }

    private BO.Order getBoOrder(DO.Order doOrder)  
    {
        BO.Order boOrder = new BO.Order
        {
            ID = doOrder.seqNum,
            CustomerName = doOrder.CustomerName,
            CustomerEmail = doOrder.CustomerEmail,
            CustomerAdress = doOrder.CustomerAdress,
            Status = getOrderStatus(doOrder),
            OrderDate = doOrder.OrderDate,
            DeliveryDate = doOrder.DeliveryDate,
            ShipDate = doOrder.ShipDate
        };
        return boOrder;
    }
    public BO.Order GetOrderDetails(int orderID)
    {
        try
        {
            orderID.negativeNumber();

             
            DO.Order DOorder = _dal.Order.RequestById(orderID);

            BO.Order order = getBoOrder(DOorder);

            IEnumerable<DO.OrderItem> orderItemsList = _dal.OrderItem.RequestByOrderId(orderID);
            order.OrderItems = (from orderItem in orderItemsList
                                select new BO.OrderItem
                                {
                                    OrderID = orderItem.seqNum,
                                    Name = _dal.Product.RequestById(orderItem.ProductID).Name,
                                    ProductID = orderItem.ProductID,
                                    Price = orderItem.Price,
                                    Amount = orderItem.Amount,
                                    TotalPrice = orderItem.Price * orderItem.Amount

                                }).ToList();

            order.TotalPrice = orderItemsList.Sum(orderItem=> orderItem.Price * orderItem.Amount);

            return order;
        }
        catch (DalApi.DalDoesNoExistException ex)
        {

            throw new BO.BoDoesNoExistException("order does not exist", ex);
        }
    }

    public BO.Order UpdateOrderShip(int orderID)
    {
        try
        {
            orderID.negativeNumber();

            DO.Order doOrder = _dal.Order.RequestById(orderID);

            BO.Order order = new BO.Order();
            if (doOrder.OrderDate != DateTime.MinValue && doOrder.ShipDate == DateTime.MinValue)
            {
                doOrder.ShipDate = DateTime.Now;
                _dal.Order.Update(doOrder);
                order = getBoOrder(doOrder);
            }
            else
            {            
                throw new BO.DateAlreadyUpdatedException("ship date is already updated");
            }
            return order;
        }
        catch (DalApi.DalDoesNoExistException ex)
        {

            throw new BO.BoDoesNoExistException("order does not exist", ex);
        }
    }

    public BO.Order UpdateOrderDelivery(int orderID)
    {
        try
        {
            orderID.negativeNumber();

            DO.Order doOrder = _dal.Order.RequestById(orderID);

            BO.Order order = new BO.Order();
            if (doOrder.ShipDate != null && doOrder.DeliveryDate == null)
            {
                doOrder.DeliveryDate = DateTime.Now;
                _dal.Order.Update(doOrder);
                order = getBoOrder(doOrder);
            }
            else
            {
                throw new BO.DateAlreadyUpdatedException("Delivery date is already updated");
            }
            return order;
        }
        catch (DalApi.DalDoesNoExistException ex)
        {

            throw new BO.BoDoesNoExistException("order does not exist", ex);
        }
    }

    public BO.OrderTracking TrakingOrder(int orderID)
    {
        try
        {
            orderID.negativeNumber();

            DO.Order doOrder = _dal.Order.RequestById(orderID);
            List<Tuple<DateTime?, string>> tupleList = new List<Tuple<DateTime?, string>>();
            Tuple<DateTime?, string> tuple;
            BO.OrderTracking orderTracking = new BO.OrderTracking();
            if (doOrder.OrderDate != null)
            {
                tuple = new(doOrder.OrderDate, "The order has been confirmed");
                tupleList.Add(tuple);
                if (doOrder.ShipDate != null)
                {
                    tuple = new(doOrder.ShipDate, "The invitation has been shipped");
                    tupleList.Add(tuple);

                    if (doOrder.DeliveryDate != null)
                    {
                        tuple = new(doOrder.DeliveryDate, "The invitation has been provided");
                        tupleList.Add(tuple);
                    }

                }
            }
            orderTracking.OrderProgress = tupleList;
            orderTracking.ID = orderID;
            orderTracking.Status = getOrderStatus(doOrder);
            
            return orderTracking;

         }
        catch (DalApi.DalDoesNoExistException ex)
        {

            throw new BO.BoDoesNoExistException("order does not exist", ex);
        }
    }
}

