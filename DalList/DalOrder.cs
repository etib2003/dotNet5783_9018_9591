using DO;
using DalApi;
using System.Runtime.CompilerServices;
using DocumentFormat.OpenXml.Office2010.Excel;
//using static Dal.dataSource;

namespace Dal;
/// <summary>
/// The class of orders
/// </summary>
internal class dalOrder : IOrder
{
    /// <summary>
    /// the function adds a new order to the orders' list
    /// </summary>
    /// <param name="Or">the order you want to add</param>
    /// <returns>the added order id</returns>
    /// <exception cref="order already exist"></exception>
    public int Create(Order Or)
    {
        Or.Id = dataSource.config.SeqNumOr;
        if (dataSource._orders.Exists(x => x?.Id == Or.Id))
            throw new DalAlreadyExistsException("Order");
        dataSource._orders.Add(Or);
        return Or.Id;
    }

    /// <summary>
    /// the function returns the order of the given id
    /// </summary>
    /// <param name="id">the order's id </param >
    /// <returns>the order of the given id</returns >
    /// <exception cref="the order isn't exist"></exception >
    public Order RequestById(int id)
    {

        return dataSource._orders.Find(x => x?.Id == id) ??
              throw new DalDoesNoExistException("Order");

    }

    /// <summary>
    /// the function updates a certain order with the given one
    /// </summary>
    /// <param name="Or">the new order you want to put instead of the old one</param >
    /// <exception cref="the order you want to update does not exist"></exception >
    public void Update(Order Or)
    {
        if(RequestById(Or.Id) is Order order)
        {
            dataSource._orders.Remove(order);
            dataSource._orders.Add(Or);
        }
    }

    /// <summary>
    /// the function deletes the order with the given id
    /// </summary>
    /// <param name="id"> the id of the order you want to delete</param >
    /// <exception cref="if the order does not exist"></exception >
    public void Delete(int id)
    {
        dataSource._orders.Remove(RequestById(id));
    }

    public IEnumerable<Order> RequestAll(Func<Order?, bool>? cond = null)
    {
        bool check = cond is null;
        return dataSource._orders.Where(order => check ? check : cond(order)).Select(order => order.Value);
    }

    public Order GetByCondition(Func<Order, bool>? cond)
    {
        throw new NotImplementedException();
    }
}