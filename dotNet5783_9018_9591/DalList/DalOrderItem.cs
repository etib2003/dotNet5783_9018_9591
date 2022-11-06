using DO;

namespace Dal;

public class DalOrderItem
{
    //CRUD for Student

    public void Create(OrderItem Oi)
    {

        //if (int i = 0;i< DataSource.OrderItems.)
        if (!DataSource.OrderItems.Exists(x => x.ID == Oi.ID))
            DataSource.OrderItems.Add(Oi);
        else
            throw new Exception("cannot create a student, is already exists");
    }

    public List<OrderItem> RequestAll()
    {
        return DataSource.OrderItems;
    }


    public OrderItem RequestById(int id)
    {
        if (!DataSource.OrderItems.Exists(x => x.ID == id))
            throw new Exception("the student is not exist");

        return DataSource.OrderItems.Find(i => i.ID == id);
    }

    public void Update(OrderItem Oi)
    {
        //if student exist throw exception 
        if (!DataSource.OrderItems.Exists(i => i.ID == Oi.ID))
            throw new Exception("cannot update a student, is not exists");
        OrderItem sToRemove = DataSource.OrderItems.Find(i => i.ID == Oi.ID);
        DataSource.OrderItems.Remove(sToRemove);
        DataSource.OrderItems.Add(Oi);

        //פונקציה נוספת
        //if(DataSource.OrderItems.Exists(i => i.OrderID == Order_ID && i.ProductID == Product_ID))
        //    return DataSource.OrderItems.Find(i => i.OrderID== Order_ID && i.ProductID == Product_ID);
    }


    public void Delete(OrderItem Oi)
    {
        //if student exist throw exception 
        if (!DataSource.OrderItems.Exists(i => i.ID == Oi.ID))
            throw new Exception("cannot delete a student, is not exists");
        DataSource.OrderItems.Remove(Oi); //or set Active..
    }
}
