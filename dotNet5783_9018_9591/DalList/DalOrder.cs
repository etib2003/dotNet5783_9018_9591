using DO;

namespace Dal;

public class DalOrder
{
    public int Create(Order O)
    {
        if (!DataSource.Orders.Exists(i => i.ID == O.ID))
        {
            DataSource.Orders.Add(O);
            return O.ID;
        }
        else
            throw new Exception("cannot create an order that is already exists");

    }

    public List<Order> RequestAll()
    {
        List<Order> listToReturn = DataSource.Orders;
        return listToReturn;

    }

    public Order RequestById(int id)
    {
        if (!DataSource.Orders.Exists(i => i.ID == id))
            throw new Exception("the order is not exist");

        return DataSource.Orders.Find(i => i.ID == id);
    }
  
    public void Update(Order O)
    {
        //if order does not exist throw exception 
        if (!DataSource.Orders.Exists(i => i.ID == O.ID))
            throw new Exception("cannot update an order,that is not exists");
        Order OToRemove = DataSource.Orders.Find(i => i.ID == O.ID); //מחזיר את האובייקט
        int index = DataSource.Orders.IndexOf(OToRemove);//מחזיר אינדקס לאובייקט ברשימה
        DataSource.Orders.Remove(OToRemove);//מסיר את האובייקט
        DataSource.Orders.Insert(index, O);//שם את המעודכן שמקום של האינדקס


    }
    public void Delete(int id)
    {
        //if student does not exist throw exception 
        if (!DataSource.Orders.Exists(i => i.ID ==id))
            throw new Exception("cannot delete an order,that is not exists");
        Order  OToRemove = DataSource.Orders.Find(i => i.ID == id);
        DataSource.Orders.Remove(OToRemove);
 
    }

}