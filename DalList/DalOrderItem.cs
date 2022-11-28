using DO;
using DalApi;

using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml.Drawing.Charts;
//using static Dal.dataSource;

namespace Dal;

internal class dalOrderItem:IOrderItem
{
    //CRUD for Student
    /// <summary>
    /// the function adds a new orderItem to the orderItems' list
    /// </summary>
    /// <param name="Oi">the orderItem you want to add</param>
    /// <returns>the added orderItem id</returns >
    public int Create(OrderItem Oi)
    {
        Oi.seqNum = dataSource.config.SeqNumOi;
        if (dataSource._orderItems.Exists(x => x.seqNum == Oi.seqNum))
            throw new DalAlreadyExistsException("OrderItem");
        dataSource._orderItems.Add(Oi);
        return Oi.seqNum;
    }

    /// <summary>
    /// the function returns the orderItems' list
    /// </summary>
    /// <returns>the orderItems' list</returns >
    public IEnumerable<OrderItem> RequestAll()
    {
        List<OrderItem> listToReturn = new List<OrderItem>();
        for (int i = 0; i < dataSource._orderItems.Count; i++)
            listToReturn.Add(dataSource._orderItems[i]);
        return listToReturn;
    }

    /// <summary>
    /// the function returns the orderItem that matches the given seqNum
    /// </summary>
    /// <param name="id">the given seqNum</param >
    /// <returns>the orderItem of the given seqNum</returns >
    /// <exception cref="the OrderItem does not exist"></exceptionthe  >
    public OrderItem RequestById(int id)
    {
        if (!dataSource._orderItems.Exists(x => x.seqNum == id))
            throw new DalDoesNoExistException("OrderItem");

        return dataSource._orderItems.Find(x => x.seqNum == id);
    }

    /// <summary>
    /// the function returns the orderItem that matches the given order's id and product's id
    /// </summary>
    /// <param name="orderId">the given order's id</param >
    /// <param name="productId"> the given product's id</param>
    /// <returns>the orderItem that matches the given order's id and product's id</returns>
    /// <exception cref="the OrderItem does not exist"></exception >
    public OrderItem RequestByOrderIDProductID(int orderId, int productId)
    {
        if (!dataSource._orderItems.Exists(x => x.OrderID == orderId && x.ProductID == productId))
            throw new DalDoesNoExistException("OrderItem");

        return dataSource._orderItems.Find(x => x.OrderID == orderId && x.ProductID == productId);
    }

    /// <summary>
    /// the function returns the orderItem that matches the given order's id
    /// </summary>
    /// <param name="orderId">the given order's id</param>
    /// <returns>the orderItem that matches the given order's id</returns>
    /// <exception cref="the OrderItem does not exist"></exception>
    public List<OrderItem> RequestByOrderId(int orderId)
    {
        if (!dataSource._orderItems.Exists(x => x.OrderID == orderId))
            throw new DalDoesNoExistException("OrderItem");

        List<OrderItem> listToReturn = dataSource._orderItems.FindAll(x => x.OrderID == orderId);

        return listToReturn;
    }

    /// <summary>
    /// the function updates a certain orderItem with the given one
    /// </summary>
    /// <param name="Oi"> the new orderItem you want to put instead of the old one</param>
    /// <exception cref="the orderItem you want to update does not exist"> </exception>
    public void Update(OrderItem Oi)
    {
        //if _orderItems does not exist throw exception 
        if (!dataSource._orderItems.Exists(x => x.seqNum == Oi.seqNum))
            throw new DalDoesNoExistException("OrderItem");
        OrderItem OiToRemove = dataSource._orderItems.Find(x => x.seqNum == Oi.seqNum);
        Oi.seqNum = OiToRemove.seqNum;
        dataSource._orderItems.Remove(OiToRemove);
        dataSource._orderItems.Add(Oi);
    }

    /// <summary>
    /// the function deletes the orderItem with the given id
    /// </summary>
    /// <param name="id"> the id of the orderItem you want to delete</param>
    /// <exception cref="Exception">the orderItem already exist</exception>
    public void Delete(int id)
    {
        //if _orderItems does not exist throw exception 
        if (!dataSource._orderItems.Exists(x => x.seqNum == id))
            throw new DalDoesNoExistException("OrderItem");
        OrderItem OiToRemove = dataSource._orderItems.Find(x => x.seqNum == id);
        dataSource._orderItems.Remove(OiToRemove);
    }
}
