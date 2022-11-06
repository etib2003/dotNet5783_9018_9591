using DO;

namespace Dal;
internal static class DataSource
{
    //internal static readonly object Order_vec;

    static readonly Random random = new Random();
    internal static List<Product> Products = new List<Product>();
    internal static List<Order> Orders = new List<Order>();
    internal static List<OrderItem> OrderItems = new List<OrderItem>();

    //static int x = random.Next(2, 10);

    static void s_Initialize()
    {

    }
}
