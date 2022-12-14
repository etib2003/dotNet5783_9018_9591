using DalApi;

namespace Dal;

sealed internal class DalXml : IDal
{
    public IProduct Product { get; }
    public IOrder Order { get; }
    public IOrderItem OrderItem { get; }
    public static IDal Instance { get; } = new DalXml();
    private DalXml() 
    {
        Product= new dalProduct();
        Order = new dalOrder();
        OrderItem = new dalOrderItem();
    }

}
