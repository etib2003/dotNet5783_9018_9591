using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;
namespace Dal;

internal class dalOrder : IOrder
{
    public int Create(Order Or)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Order Get(Func<Order?, bool>? cond)
    {
        throw new NotImplementedException();
    }

    public Order GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Order?> RequestAll(Func<Order?, bool>? cond = null)
    {
        throw new NotImplementedException();
    }

    public void Update(Order Or)
    {
        throw new NotImplementedException();
    }
}


