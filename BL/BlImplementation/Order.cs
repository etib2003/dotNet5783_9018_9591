using DalApi;
using OtherFunctions;
using System.Runtime.Serialization;

internal class Order : BlApi.IOrder
{
    private DalApi.IDal _dal = new Dal.DalList();

    public IEnumerable<BO.OrderForList> GetOrderListForManager()
    {
        return from order in _dal.Order.RequestAll()
               let orderItems = _dal.OrderItem.RequestByOrderId(order.seqNum)
               select new BO.OrderForList
               {
                   ID = order.seqNum,
                   CustomerName = order.CustomerName,
                   AmountOfItems = orderItems.Count(),
                   Status = getOrderStatus(order),
                   TotalPrice = orderItems.Sum(orderItem => orderItem.Price * orderItem.Amount),
               }; ;
         
    }
    private BO.OrderStatus getOrderStatus(DO.Order order)//לבדוק איך צריך להיות כתוב השם של הפונקציה
    {
        return order.DeliveryDate != DateTime.MinValue ? BO.OrderStatus.provided : order.ShipDate != DateTime.MinValue ?
        BO.OrderStatus.shipped : BO.OrderStatus.confirmed;
    }

    private BO.Order getBoOrder(DO.Order doOrder) //מעתיק הזמנה DO להזמנה BO
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

            //exception
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

                                }).ToList();//orderitemsהמרה לרשימה כדי שיכנס ל 


            return order;
        }
        catch (DalApi.DalDoesNoExistException ex)
        {

            throw new BoDoesNoExistException(string.Empty, ex);
        }
    }

    public BO.Order UpdateOrderShip(int orderID)
    {
        if (orderID < 0)
            throw new Exception();
        DO.Order doOrder = _dal.Order.RequestById(orderID);
        //אם הוא לא קיים אז תהיה חריגה
        BO.Order order = new BO.Order();
        if (doOrder.OrderDate != DateTime.MinValue && doOrder.ShipDate == DateTime.MinValue)
        {
            doOrder.ShipDate = DateTime.Now;
            _dal.Order.Update(doOrder);
            order = getBoOrder(doOrder);
        }
        else
        {
            throw new Exception("ship date is already updated");
        }
        return order;
    }

    public BO.Order UpdateOrderDelivery(int orderID)
    {
        if (orderID < 0)
            throw new Exception();
        DO.Order doOrder = _dal.Order.RequestById(orderID);
        //אם הוא לא קיים אז תהיה חריגה
        BO.Order order = new BO.Order();
        if (doOrder.ShipDate != DateTime.MinValue && doOrder.DeliveryDate == DateTime.MinValue)
        {
            doOrder.DeliveryDate = DateTime.Now;
            _dal.Order.Update(doOrder);
            order = getBoOrder(doOrder);
        }
        else
        {
            throw new Exception("Delivery date is already updated");
        }
        return order;
    }

    public BO.OrderTracking TrakingOrder(int orderID)
    {
        if (orderID < 0)
            throw new Exception();
        DO.Order doOrder = _dal.Order.RequestById(orderID);
        List<Tuple<DateTime, string>> tupleList = new List<Tuple<DateTime, string>>();
        Tuple<DateTime, string> tuple;
        BO.OrderTracking orderTracking = new BO.OrderTracking();
        if (doOrder.OrderDate != DateTime.MinValue)
        {
            tuple = new(doOrder.OrderDate, "The order has been confirmed");
            tupleList.Add(tuple);
            if (doOrder.ShipDate != DateTime.MinValue)
            {
                tuple = new(doOrder.ShipDate, "The invitation has been shipped");
                tupleList.Add(tuple);

                if (doOrder.DeliveryDate != DateTime.MinValue)
                {
                    tuple = new(doOrder.DeliveryDate, "The invitation has been provided");
                    tupleList.Add(tuple);
                }

            }
        }
        orderTracking.OrderProgress = tupleList;
        orderTracking.ID = orderID;
        orderTracking.Status = GetOrderStatus(doOrder);
         //הגענו עד לפה
        return orderTracking;



        throw new NotImplementedException();

    }

    //public void UpdateOrder() //בונוס
    //{
    //    throw new NotImplementedException();
    //}




}

