using DO;

namespace Dal;
internal static class DataSource
{
    /// <summary>
    /// internal static readonly object Order_vec
    /// </summary>
    static readonly Random random = new Random();

    /// <summary>
    /// List of Products
    /// </summary>
    internal static List<Product> Products = new List<Product>();

    /// <summary>
    /// List of Orders
    /// </summary>
    internal static List<Order> Orders = new List<Order>();

    /// <summary>
    /// List of OrderItems
    /// </summary>
    internal static List<OrderItem> OrderItems = new List<OrderItem>();


    /// <summary>
    /// constructor
    /// </summary>
    static DataSource()
    {
        s_Initialize();
    }

    /// <summary>
    /// call the order/product*orderItem initialize methods
    /// </summary>
    static void s_Initialize()
    {
        OrderInitialize();
        ProductInitialize();
        OrderItemInitialize();
    }

    /// <summary>
    /// initialize the orders list
    /// </summary>
    static void OrderInitialize()
    {
        //arrays for initialization of objects
        string[] CustomerFirstName = { "Shawn" , "Chavi" , "Eti", "Michal", "Shir",
                                       "Gad","Dan","Tal","Ori","Gal","Adam",
                                       "Hadar", "Bibi", "Eden", "Daniel" };
        string[] CustomerLastName = { "Mendes" , "Bombach" , "Bernat", "Gad", "Ben",
                                      "Dan","Bar","Cohen","Gal","Adam","Prais",
                                      "Muchtar", "Netanyahu", "Ben Zaken", "Yona" };
        string[] Customer_Adress =  { "Pinkas 1,Bnei Brak", "Eliezer 54, Petach Tikva", "David 12,Herzelia", "Herzog 14,Bnei Brak",
                                      "gehula 30, jerusalem","Mohaliver 9, Bnei Brak", "Menachem 7, Tel Aviv", "Byalik 87, Ramat Gan",
                                      "Daniel 8, Beit Shemesh", "Segal 21, Netania", "Havradim 3, Chulon","Rotshild 17, Tel Aviv" };
        int AmountOfOrders = 25;

        for (int i = 1; i <= AmountOfOrders; i++)//initializing orders
        {

            Order order = new Order();//create a new object
            order.seqNum = config.SeqNumOr;//adds to the seqNum 1
            string customerFirstName = CustomerFirstName[random.Next(0, 14)];
            string customerLastName = CustomerLastName[random.Next(0, 14)];
            order.CustomerName = customerFirstName + " " + customerLastName;
            order.CustomerEmail = customerFirstName + customerLastName + order.seqNum + "@gmail.com";
            order.CustomerAdress = Customer_Adress[random.Next(0, 11)];
            order.OrderDate = DateTime.Now.AddMonths(random.Next(-4, -1));
            if (i <= AmountOfOrders * 0.8)
                order.ShipDate = order.OrderDate + new TimeSpan (random.Next(24), random.Next(60), random.Next(60));
            else
                order.ShipDate = null;
            if (i <= AmountOfOrders
                * 0.6 * 0.8)
                order.DeliveryDate = order.ShipDate + new TimeSpan(random.Next(24), random.Next(60), random.Next(60));
            else
                order.DeliveryDate = null;

            Orders.Add(order);  //adds the new order to the order's list.
        }
    }

    /// <summary>
    /// initialize the products list
    /// </summary>
    static void ProductInitialize()//initializing products
    {
        int AmountOfProducts = 10;
        for (int i = 1; i <= AmountOfProducts; i++)
        {
            Product product = new Product();//create a new object
            do
            {
                product.ID = random.Next(100000, 999999);
            }
            while (Products.Exists(x => x.ID == product.ID));
            product.Name = "product" + i;
            product.Price = random.Next(200, 2000);
            product.Category = (Category)(i % 5);
            product.Color = (Color)(i % 4);
            if (i <= AmountOfProducts * 0.05 + 1)
                product.InStock = 0;
            else
                product.InStock = i * 3;
            Products.Add(product);
        }
    }

    /// <summary>
    /// initialize the order items list
    /// </summary>
    static void OrderItemInitialize()//initializing orderItems 
    {
        int AmountOfOrderItem = 40;

        for (int i = 1; i <= AmountOfOrderItem; i++)
        {
            OrderItem orderItem = new OrderItem();//create a new object
            orderItem.seqNum = config.SeqNumOi;//adds to the seqNum 1;
            orderItem.OrderID = Orders[i % 25].seqNum;
            Product p = Products[random.Next(0, 9)];
            orderItem.ProductID = p.ID;
            orderItem.Price = p.Price;
            orderItem.Amount = random.Next(1, 4);
            OrderItems.Add(orderItem);
        }
    }

    /// <summary>
    /// class for a running number
    /// </summary>
    internal static class config
    {
        internal static int seqNumOi = 1;
        internal static int seqNumOr = 1;
 
        public static int SeqNumOi => seqNumOi++;
        public static int SeqNumOr => seqNumOr++;

    }
}
