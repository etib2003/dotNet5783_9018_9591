using DalApi;
namespace Dal;


sealed public class DalList : IDal
{
    public IProduct Product=> new dalProduct();
    public IOrder Order => new dalOrder();
    public IOrderItem OrderItem=>new dalOrderItem();
}

