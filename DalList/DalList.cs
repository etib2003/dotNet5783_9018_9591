using DalApi;
namespace Dal;


sealed internal class DalList : IDal
{
    public static IDal Instance { get; } = new DalList();

    public IProduct Product { get; }
    public IOrder Order { get; }
    public IOrderItem OrderItem { get; }
    private DalList()
    {
        Product = new dalProduct();
        Order = new dalOrder();
        OrderItem = new dalOrderItem();
    }

}

