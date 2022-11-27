using DO;
using DalApi;
using System.Runtime.CompilerServices;
//using static Dal.DataSource;

namespace Dal;
/// <summary>
/// The class of orders
/// </summary>
internal class DalOrder: IOrder
{
    /// <summary>
    /// the function adds a new order to the orders' list
    /// </summary>
    /// <param name="Or">the order you want to add</param>
    /// <returns>the added order id</returnsreturns >
    public int Create(Order Or)
    {
        Or.seqNum = DataSource.config.SeqNumOr;
        if (DataSource.OrderItems.Exists(x => x.seqNum == Or.seqNum))
            throw new DalAlreadyExistsException("OrderItem");
        DataSource.Orders.Add(Or);
        return Or.seqNum;
    }

    /// <summary>
    /// the function returns the orders' list
    /// </summary>
    /// <returns> the order already exists </returns x>
    public IEnumerable<Order> RequestAll()
    {
        List<Order> listToReturn = new List<Order>();
        for (int i = 0; i < DataSource.Orders.Count; i++)
            listToReturn.Add(DataSource.Orders[i]);

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
        if (!DataSource.Orders.Exists(x => x.seqNum == id))
            throw new DalDoesNoExistException("cannot return an order,that does not exist");

        return DataSource.Orders.Find(x => x.seqNum == id);
    }

    /// <summary>
    /// the function updates a certain order with the given one
    /// </summary>
    /// <param name="Or">the new order you want to put instead of the old one</param >
    /// <exception cref="the order you want to update does not exist"></exception >
    public void Update(Order Or)
    {
        //if order does not exist throw exception 
        if (!DataSource.Orders.Exists(x => x.seqNum == Or.seqNum))
            throw new DalDoesNoExistException("cannot update an order,that does not exist");
        Order OToRemove = DataSource.Orders.Find(x => x.seqNum == Or.seqNum); 
        Or.seqNum = OToRemove.seqNum;
        DataSource.Orders.Remove(OToRemove);
        DataSource.Orders.Add(Or);
    }

    /// <summary>
    /// the function deletes the order with the given id
    /// </summary>
    /// <param name="id"> the id of the order you want to delete</param >
    /// <exception cref="if the order does not exist"></exception >
    public void Delete(int id)
    {
        //if student does not exist throw exception 
        if (!DataSource.Orders.Exists(x => x.seqNum == id))
            throw new DalDoesNoExistException("cannot delete an order,that does not exist");
        Order OToRemove = DataSource.Orders.Find(x => x.seqNum == id);
        DataSource.Orders.Remove(OToRemove);
    }

}