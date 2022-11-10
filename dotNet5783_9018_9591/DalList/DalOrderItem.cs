using DO;
using System.Collections.Generic;
using System.Linq;
using static Dal.DataSource;

namespace Dal;

public class DalOrderItem
{
    //CRUD for Student

    public int Create(OrderItem Oi)
    {
        if (DataSource.OrderItems.Exists(x => x.seqNum == Oi.seqNum))
            throw new Exception("cannot create an OrderItem,that is already exists");
        if(!(DataSource.Orders.Exists(x => x.seqNum == Oi.OrderID)&& DataSource.Products.Exists(x => x.ID == Oi.ProductID)))
            throw new Exception("cannot create an OrderItem,because there is no this order/product");
        Oi.seqNum = config.SeqNumOi;
        DataSource.OrderItems.Add(Oi);
        return Oi.seqNum;
    }

    public List<OrderItem> RequestAll()
    {
        List<OrderItem> listToReturn = new List<OrderItem>();
        for (int i = 0; i < DataSource.OrderItems.Count; i++)
            listToReturn.Add(DataSource.OrderItems[i]);
        return listToReturn;
    }

    public OrderItem RequestBySeqNum(int id)
    {
        if (!DataSource.OrderItems.Exists(x => x.seqNum == id))
            throw new Exception("the OrderItem is not exist");

        return DataSource.OrderItems.Find(x => x.seqNum == id);
    }

    public OrderItem RequestByOrderIDProductID(int orderId, int productId)
    {
        if (!DataSource.OrderItems.Exists(x => x.OrderID == orderId && x.ProductID == productId))
            throw new Exception("the OrderItem is not exist");

        return DataSource.OrderItems.Find(x => x.OrderID == orderId && x.ProductID == productId);
    }
    public List<OrderItem> RequestByOrderId(int orderId)
    {
        if (!DataSource.OrderItems.Exists(x => x.OrderID == orderId))
            throw new Exception("the OrderId is not exist");

        List<OrderItem> listToReturn = DataSource.OrderItems.FindAll(x => x.OrderID == orderId);

        return listToReturn;
    }

    public void Update(OrderItem Oi)
    {
        //if OrderItems does not exist throw exception 
        if (!DataSource.OrderItems.Exists(x => x.seqNum == Oi.seqNum))
            throw new Exception("cannot update an OrderItem, that is not exists");
        OrderItem OiToRemove = DataSource.OrderItems.Find(x => x.seqNum == Oi.seqNum); //מחזיר את האובייקט
        Oi.seqNum = OiToRemove.seqNum;
        DataSource.OrderItems.Remove(OiToRemove);//מסיר את האובייקט
        DataSource.OrderItems.Add(Oi);//שם את המעודכן שמקום של האינדקס
    }
    public void Delete(int id)
    {
        //if OrderItems does not exist throw exception 
        if (!DataSource.OrderItems.Exists(x => x.seqNum == id))
            throw new Exception("cannot delete an OrderItem,that is not exists");
        OrderItem OiToRemove = DataSource.OrderItems.Find(x => x.seqNum == id);
        DataSource.OrderItems.Remove(OiToRemove);
    }



}
