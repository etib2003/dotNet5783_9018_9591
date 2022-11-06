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

    static DataSource() //בנאי
    {
        s_Initialize();
    }
    static void s_Initialize()
    {
        OrderInitialize();
        ProductInitialize();
        OrderItemInitialize();
    }

    static void OrderInitialize()
    {
        string[] CustomerName = { "Hadar Haim" , "Chavi Bombach" , "Eti Bernat", "Michal Gad", "Shir Ben", "Gad Dan",
                                  "Dan Bar", "Tal Cohen", "Ori Gal", "Gal Adam", "Adam Prais", "Hadar Muchtar", "Bibi Netanyahu",
                                  "Eden Ben Zaken", "Daniel Yona" };

        

        Order order2 = new Order
        {
            ID = 1111,
            CustomerName = "Chavi",
            CustomerEmail = "Hadar@gmail.com",
            CustomerAdress = "Gan 1, jerusalem",
            OrderDate = DateTime.Now.AddDays(1),
            ShipDate = DateTime.Now.AddDays(2),
            DeliveryDate = DateTime.Now.AddDays(7)
        };

        Product product = new Product();
        product.Category = Enums.category.

    }

    static void ProductInitialize()
    {

    }

    static void OrderItemInitialize()
    {

    }

    internal static class config
    {
        private static int LastIdOr = 100000;//לבדוק לגבי זה
        private static int LastIdOi = 100000;//לבדוק לגבי זה


    static int indexOr = 0;
        static int indexpdct = 0;
        static int indexOi = 0;
    }


}
