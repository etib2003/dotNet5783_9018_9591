using DO;

namespace Dal;

sealed internal class DalXml : IDal
{
    public IProduct Product { get; } =new dalProduct();
    public IOrder Order { get; } = new dalOrder();
    public IOrderItem OrderItem { get; } = new dalOrderItem();
    public static IDal Instance { get; } = new DalXml();
    private DalXml() { }

}
