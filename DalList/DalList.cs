using DalApi;
namespace Dal;


sealed internal class DalList : IDal
{
    public static IDal Instance { get; } = new DalList();
    private DalList() { }
    public IProduct Product => new dalProduct();
    public IOrder Order => new dalOrder();
    public IOrderItem OrderItem => new dalOrderItem();
}

