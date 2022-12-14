using DalApi;
using DO;
namespace Dal;

internal class dalOrderItem : IOrderItem
{
    public int Create(OrderItem Or)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public OrderItem Get(Func<OrderItem?, bool>? cond)
    {
        throw new NotImplementedException();
    }

    public OrderItem GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<OrderItem?> RequestAll(Func<OrderItem?, bool>? cond = null)
    {
        throw new NotImplementedException();
    }

    public void Update(OrderItem Or)
    {
        throw new NotImplementedException();
    }
}

