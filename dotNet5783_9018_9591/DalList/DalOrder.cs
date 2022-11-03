namespace DalList;

using Dal;
using System.Collections.Generic;

public class DalOrder
{
 void  s_Initialize()
    {

    }
    public void Add(Order O)
    {
        if (DataSource.Orders.Exists(i => i.ID == O.ID))
            throw new Exception("cannot create an order that is already exists");
        DataSource.Orders.Add(O);
    }

    public list<Order> RequestAll()
    {
        return DataSource.Orders ;
    }

    public Order RequestById(int id)
    {
        if (!DataSource.Orders.Exists(i => i.ID == id))
            throw new Exception("the order is not exist");

        return DataSource.Orders.Find(i => i.ID == id);
    }

    public void Update(Order s)
    {
        //if order exist throw exception 
        if (!DataSource.Orders.Exists(i => i.ID == O.ID))
            throw new Exception("cannot update a student, is not exists");
        Order sToRemove = DataSource.Orders.Find(i => i.ID == s.ID);
        DataSource.students.Remove(sToRemove);
        DataSource.students.Add(s);

    }

    public void Delete(Student s)
    {
        //if student exist throw exception 
        if (!DataSource.students.Exists(i => i.StudentId == s.StudentId))
            throw new Exception("cannot delete a student, is not exists");
        DataSource.students.Remove(s); //or set Active..
    }


}