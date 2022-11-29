using DO;
using DalApi;
using System.Runtime.CompilerServices;
//using static Dal.dataSource;

namespace Dal;
/// <summary>
/// The class of orders
/// </summary>
internal class dalOrder: IOrder
{
    /// <summary>
    /// the function adds a new order to the orders' list
    /// </summary>
    /// <param name="Or">the order you want to add</param>
    /// <returns>the added order id</returns>
    /// <exception cref="order already exist"></exception>
    public int Create(Order Or)
    {
        Or.seqNum = dataSource.config.SeqNumOr;
        if (dataSource._orders.Exists(x => x.seqNum == Or.seqNum))
            throw new DalAlreadyExistsException("Order");
        dataSource._orders.Add(Or);
        return Or.seqNum;
    }

    /// <summary>
    /// the function returns the orders' list
    /// </summary>
    /// <returns>the order already exists</returns>
    public IEnumerable<Order> RequestAll()
    {
        List<Order> listToReturn = new List<Order>();
        for (int i = 0; i < dataSource._orders.Count; i++)
            listToReturn.Add(dataSource._orders[i]);

        return listToReturn;
    }

    /// <summary>
    /// the function returns the order of the given id
    /// </summary>
    /// <param name="id">the order's id </param >
    /// <returns>the order of the given id</returns >
    /// <exception cref="the order isn't exist"></exception >
    public Order RequestById(int id)
    {
        if (!dataSource._orders.Exists(x => x.seqNum == id))
            throw new DalDoesNoExistException("Order");

        return dataSource._orders.Find(x => x.seqNum == id);
    }

    /// <summary>
    /// the function updates a certain order with the given one
    /// </summary>
    /// <param name="Or">the new order you want to put instead of the old one</param >
    /// <exception cref="the order you want to update does not exist"></exception >
    public void Update(Order Or)
    {
        //if order does not exist throw exception 
        if (!dataSource._orders.Exists(x => x.seqNum == Or.seqNum))
            throw new DalDoesNoExistException("Order");
        Order OToRemove = dataSource._orders.Find(x => x.seqNum == Or.seqNum); 
        Or.seqNum = OToRemove.seqNum;
        dataSource._orders.Remove(OToRemove);
        dataSource._orders.Add(Or);
    }

    /// <summary>
    /// the function deletes the order with the given id
    /// </summary>
    /// <param name="id"> the id of the order you want to delete</param >
    /// <exception cref="if the order does not exist"></exception >
    public void Delete(int id)
    {
        //if student does not exist throw exception 
        if (!dataSource._orders.Exists(x => x.seqNum == id))
            throw new DalDoesNoExistException("Order");
        Order OToRemove = dataSource._orders.Find(x => x.seqNum == id);
        dataSource._orders.Remove(OToRemove);
    }

}