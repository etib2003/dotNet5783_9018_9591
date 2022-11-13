using DO;
using System.Collections.Generic;
using System.Linq;
//using static Dal.DataSource;

namespace Dal;

public class DalOrderItem
{
    //CRUD for Student
    /// <summary>
    /// the function adds a new orderItem to the orderItems' list
    /// </summary>
    /// <param name="Oi">the orderItem you want to add</param>
    /// <returns>the added orderItem id</returns >
    public int Create(OrderItem Oi)
    {
        Oi.seqNum = DataSource.config.SeqNumOi;
        DataSource.OrderItems.Add(Oi);
        return Oi.seqNum;
    }

    /// <summary>
    /// the function returns the orderItems' list
    /// </summary>
    /// <returns>the orderItems' list</returns >
    public List<OrderItem> RequestAll()
    {
        List<OrderItem> listToReturn = new List<OrderItem>();
        for (int i = 0; i < DataSource.OrderItems.Count; i++)
            listToReturn.Add(DataSource.OrderItems[i]);
        return listToReturn;
    }

    /// <summary>
    /// the function returns the orderItem that matches the given seqNum
    /// </summary>
    /// <param name="id">the given seqNum</param >
    /// <returns>the orderItem of the given seqNum</returns >
    /// <exception cref="the OrderItem does not exist"></exceptionthe  >
    public OrderItem RequestBySeqNum(int id)
    {
        if (!DataSource.OrderItems.Exists(x => x.seqNum == id))
            throw new Exception("the OrderItem does not exist");

        return DataSource.OrderItems.Find(x => x.seqNum == id);
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
        if (!DataSource.OrderItems.Exists(x => x.OrderID == orderId && x.ProductID == productId))
            throw new Exception("the OrderItem does not exist");

        return DataSource.OrderItems.Find(x => x.OrderID == orderId && x.ProductID == productId);
    }

    /// <summary>
    /// the function returns the orderItem that matches the given order's id
    /// </summary>
    /// <param name="orderId">the given order's id</param>
    /// <returns>the orderItem that matches the given order's id</returns>
    /// <exception cref="the OrderItem does not exist"></exception>
    public List<OrderItem> RequestByOrderId(int orderId)
    {
        if (!DataSource.OrderItems.Exists(x => x.OrderID == orderId))
            throw new Exception("the OrderItem with this OrderId does not exist");

        List<OrderItem> listToReturn = DataSource.OrderItems.FindAll(x => x.OrderID == orderId);

        return listToReturn;
    }

    /// <summary>
    /// the function updates a certain orderItem with the given one
    /// </summary>
    /// <param name="Oi"> the new orderItem you want to put instead of the old one</param>
    /// <exception cref="the orderItem you want to update does not exist"> </exception>
    public void Update(OrderItem Oi)
    {
        //if OrderItems does not exist throw exception 
        if (!DataSource.OrderItems.Exists(x => x.seqNum == Oi.seqNum))
            throw new Exception("cannot update an OrderItem, that is not exists");
        OrderItem OiToRemove = DataSource.OrderItems.Find(x => x.seqNum == Oi.seqNum);
        Oi.seqNum = OiToRemove.seqNum;
        DataSource.OrderItems.Remove(OiToRemove);
        DataSource.OrderItems.Add(Oi);
    }

    /// <summary>
    /// the function deletes the orderItem with the given id
    /// </summary>
    /// <param name="id"> the id of the orderItem you want to delete</param>
    /// <exception cref="Exception">the orderItem already exist</exception>
    public void Delete(int id)
    {
        //if OrderItems does not exist throw exception 
        if (!DataSource.OrderItems.Exists(x => x.seqNum == id))
            throw new Exception("cannot delete an OrderItem,that is not exists");
        OrderItem OiToRemove = DataSource.OrderItems.Find(x => x.seqNum == id);
        DataSource.OrderItems.Remove(OiToRemove);
    }
}
