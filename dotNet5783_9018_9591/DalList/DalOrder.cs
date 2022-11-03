namespace DalList;
using System.Collections.Generic;

public class DalOrder
{
 void  s_Initialize()
    {

    }
    public void Add(Order O)
    {
        if (DataSource.Order_vec.Exists(i => i.ID == s.ID))
            throw new Exception("cannot create an order that is already exists");
        DataSource.Order_vec.Add(O);
    }

    public Order[] RequestAll()
    {
        return DataSource.Order_vec ;
    }

    public Order RequestById(int id)
    {
        if (!DataSource.Order_vec.Exists(i => i.ID == id))
            throw new Exception("the order is not exist");

        return DataSource.Order_vec.Find(i => i.ID == id);
    }

    public void Update(Order s)
    {
        //if order exist throw exception 
        if (!DataSource.students.Exists(i => i.StudentId == s.StudentId))
            throw new Exception("cannot update a student, is not exists");
        Student sToRemove = DataSource.students.Find(i => i.StudentId == s.StudentId);
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