using DO;
using System.Collections.Generic;
using System.Linq;

namespace Dal;

public class DalOrderItem
{
    //CRUD for Student

    public int Create(OrderItem Oi)
    {
        if (!DataSource.OrderItems.Exists(x => x.ID == Oi.ID))
        {
            DataSource.OrderItems.Add(Oi);
            return Oi.ID;
        }
        else
            throw new Exception("cannot create an OrderItem,that is already exists");
    }

    public List<OrderItem> RequestAll()
    {
        List<OrderItem> listToReturn = DataSource.OrderItems;
        return listToReturn;
    }

    public OrderItem RequestById(int id)
    {
        if (!DataSource.OrderItems.Exists(x => x.ID == id))
            throw new Exception("the OrderItem is not exist");

        return DataSource.OrderItems.Find(i => i.ID == id);
    }
    //public List<OrderItem> RequestByOrderId(int orderId)
    //{
    //    if (!DataSource.OrderItems.Exists(x => x.OrderID == orderId))
    //        throw new Exception("the OrderId is not exist");

    //    List<OrderItem> listByOrderId= new List<OrderItem>();
    //    //foreach (OrderItem orderId in DataSource.OrderItems.OrderId)
    //    //{
    //    //    DataSource.OrderItems.ForEach(listByOrderId.Add())
    //    //}
    //    //return DataSource.OrderItems.Find(i => i.OrderID == orderId);
    //}

    public OrderItem RequestByOrderIDProductID(int orderId, int productId)
    {
        if (!DataSource.OrderItems.Exists(i => i.OrderID == orderId && i.ProductID == productId))
            throw new Exception("the OrderItem is not exist");

        return DataSource.OrderItems.Find(i => i.OrderID == orderId && i.ProductID == productId);
    }

    public void Update(OrderItem Oi)
    {
        //if OrderItems does not exist throw exception 
        if (!DataSource.OrderItems.Exists(i => i.ID == Oi.ID))
            throw new Exception("cannot update an OrderItem, that is not exists");
        OrderItem OiToRemove = DataSource.OrderItems.Find(i => i.ID == Oi.ID); //מחזיר את האובייקט
        int index = DataSource.OrderItems.IndexOf(OiToRemove);//מחזיר אינדקס לאובייקט ברשימה
        DataSource.OrderItems.Remove(OiToRemove);//מסיר את האובייקט
        DataSource.OrderItems.Insert(index, OiToRemove);//שם את המעודכן שמקום של האינדקס
    }
    public void Delete(int id)
    {
        //if OrderItems does not exist throw exception 
        if (!DataSource.OrderItems.Exists(i => i.ID == id))
            throw new Exception("cannot delete an OrderItem,that is not exists");
        OrderItem OiToRemove = DataSource.OrderItems.Find(i => i.ID == id);
        DataSource.OrderItems.Remove(OiToRemove);
    }

   

}
