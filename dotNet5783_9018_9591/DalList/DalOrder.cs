using DO;
using System.Runtime.CompilerServices;
using static Dal.DataSource;

namespace Dal;

public class DalOrder
{
    public int Create(Order Or)
    {
        if (DataSource.Orders.Exists(x => x.seqNum == Or.seqNum))//לוודא לגבי זה כי בעיקרון לא יכול להיות קיים כי עכשיו המספר המזהה נוצר
            throw new Exception("cannot create an order that is already exists");
        Or.seqNum = config.SeqNumOr;
        DataSource.Orders.Add(Or);
        return Or.seqNum;
    }

    public List<Order> RequestAll()
    {
        List<Order> listToReturn = new List<Order>();
        for (int i = 0; i < DataSource.Orders.Count; i++)
            listToReturn.Add(DataSource.Orders[i]);

        return listToReturn;
    }

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