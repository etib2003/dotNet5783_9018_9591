using DO;
using System.Runtime.CompilerServices;
using static Dal.DataSource;

namespace Dal;
/// <summary>
/// The class of orders
/// </summary>
public class DalOrder
{
    /// <summary>
    /// the function adds a new order to the orders' list
    /// </summary>
    /// <param name="Or"></param the order you want to add>
    /// <returns></returnsreturns the added order id>
    /// <exception cref="Exception"></exception  the order is already exist >
    public int Create(Order Or)
    {
        if (DataSource.Orders.Exists(x => x.seqNum == Or.seqNum))//לוודא לגבי זה כי בעיקרון לא יכול להיות קיים כי עכשיו המספר המזהה נוצר
            throw new Exception("cannot create an order that is already exists");
        Or.seqNum = config.SeqNumOr;
        DataSource.Orders.Add(Or);
        return Or.seqNum;
    }
    /// <summary>
    /// the function returns the orders' list
    /// </summary>
    /// <returns></returns the orders' list>
    public List<Order> RequestAll()
    {
        List<Order> listToReturn = new List<Order>();
        for (int i = 0; i < DataSource.Orders.Count; i++)
            listToReturn.Add(DataSource.Orders[i]);

        return listToReturn;
    }
    /// <summary>
    /// the function returns the order with the given id
    /// </summary>
    /// <param name="id"></param the order id >
    /// <returns></returns>
    /// <exception cref="Exception"></exception the order isn't exist>
    public Order RequestById(int id)
    {
        if (!DataSource.Orders.Exists(x => x.seqNum == id))
            throw new Exception("the order is not exist");

        return DataSource.Orders.Find(x => x.seqNum == id);
    }

    public void Update(Order Or)
    {
        //if order does not exist throw exception 
        if (!DataSource.Orders.Exists(x => x.seqNum == Or.seqNum))
            throw new Exception("cannot update an order,that is not exists");
        Order OToRemove = DataSource.Orders.Find(x => x.seqNum == Or.seqNum); //מחזיר את האובייקט
        Or.seqNum = OToRemove.seqNum;
        DataSource.Orders.Remove(OToRemove);//מסיר את האובייקט
        DataSource.Orders.Add(Or);//שם את המעודכן שמקום של האינדקס
    }
    public void Delete(int id)
    {
        //if student does not exist throw exception 
        if (!DataSource.Orders.Exists(x => x.seqNum == id))
            throw new Exception("cannot delete an order,that is not exists");
        Order OToRemove = DataSource.Orders.Find(x => x.seqNum == id);
        DataSource.Orders.Remove(OToRemove);
    }

}