using DO;
using DalApi;
using System.Runtime.CompilerServices;
using DocumentFormat.OpenXml.Office2010.Excel;
using System.Linq;
//using static Dal.DataSource;

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
        Or.Id = DataSource.config.SeqNumOr;
        if (DataSource._orders.Exists(x => x?.Id == Or.Id))
            throw new DalAlreadyExistsException("Order");
        DataSource._orders.Add(Or);
        return Or.Id;
    }

    /// <summary>
    /// the function returns the order of the given id
    /// </summary>
    /// <param name="id">the order's id </param >
    /// <returns>the order of the given id</returns >
    /// <exception cref="the order isn't exist"></exception >
    public Order GetById(int id)
    {
        return Get(order => order?.Id == id);
    }

    /// <summary>
    /// the function updates a certain order with the given one
    /// </summary>
    /// <param name="Or">the new order you want to put instead of the old one</param >
    /// <exception cref="the order you want to update does not exist"></exception >
    public void Update(Order Or)
    {
        if(GetById(Or.Id) is Order order)
        {
            DataSource._orders.Remove(order);
            DataSource._orders.Add(Or);
        }
    }

    /// <summary>
    /// the function deletes the order with the given id
    /// </summary>
    /// <param name="id"> the id of the order you want to delete</param >
    /// <exception cref="if the order does not exist"></exception >
    public void Delete(int id)
    {
        DataSource._orders.Remove(GetById(id));
    }

    public Order Get(Func<Order?, bool>? cond)
    {
        return DataSource._orders.FirstOrDefault(cond!) ?? throw new DalDoesNoExistException("Order");
    }

    IEnumerable<Order?> ICrud<Order>.RequestAll(Func<Order?, bool>? cond)
    {
        return DataSource._orders.Where(order => cond is null ? true : cond!(order));
    }
}