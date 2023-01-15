using DO;
using DO;

using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml.Drawing.Charts;
//using static Dal.DataSource;

namespace Dal;

internal class dalOrderItem :IOrderItem
{

    /// <summary>
    /// the function adds a new orderItem to the orderItems' list
    /// </summary>
    /// <param name="Oi">the orderItem you want to add</param>
    /// <returns>the added orderItem id</returns >
    public int Create(OrderItem Oi)
    {
        Oi.Id = DataSource.config.SeqNumOi;
        if (DataSource._orderItems.Exists(x => x?.Id == Oi.Id))
            throw new DalAlreadyExistsException("OrderItem");

        DataSource._orderItems.Add(Oi);
        return Oi.Id;
    }

    /// <summary>
    /// the function returns the orderItem that matches the given Id
    /// </summary>
    /// <param name="id">the given Id</param>
    /// <returns>the orderItem of the given Id</returns>
    /// <exception cref="the orderItem does not exist"></exception>
    public OrderItem GetById(int id)
    {
        return Get(orderItem => orderItem?.Id == id);
    }

    /// <summary>
    /// the function updates a certain orderItem with the given one
    /// </summary>
    /// <param name="Oi"> the new orderItem you want to put instead of the old one</param>
    /// <exception cref="the orderItem you want to update does not exist"> </exception>
    public void Update(OrderItem Oi)
    {
        //if _orderItems does not exist throw exception 
        if (GetById(Oi.Id) is OrderItem orderItem)
        {
            DataSource._orderItems.Remove(orderItem);
            DataSource._orderItems.Add(Oi);
        }
    }

    /// <summary>
    /// the function deletes the orderItem with the given id
    /// </summary>
    /// <param name="id"> the id of the orderItem you want to delete</param>
    /// <exception cref="the orderItem already exist"></exception>
    public void Delete(int id)
    {
        DataSource._orderItems.Remove(GetById(id));
    }

    /// <summary>
    /// the function returns the order item according to the given condition
    /// </summary>
    /// <param name="cond">the given condition</param>
    /// <returns>order item according to the given condition</returns>
    /// <exception cref="the order item you want to get does not exist"></exception>
    public OrderItem Get(Func<OrderItem?, bool>? cond)
    {
        return DataSource._orderItems.FirstOrDefault(cond!) ?? throw new DalDoesNoExistException("Order Item");
    }

    /// <summary>
    /// the function returns a list of order items according to the given condition
    /// </summary>
    /// <param name="cond">the given condition</param>
    /// <returns>a list of order items according to the given condition</returns>
    IEnumerable<OrderItem?> ICrud<OrderItem>.RequestAll(Func<OrderItem?, bool>? cond)
    {
        return DataSource._orderItems.Where(orderItem => cond is null ? true : cond!(orderItem));
    } 
}
