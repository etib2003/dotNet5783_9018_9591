using DO;

namespace Dal;
internal static class DataSource
{
    /// <summary>
    /// internal static readonly object Order_vec
    /// </summary>
    static readonly Random random = new Random();

    /// <summary>
    /// List of _products
    /// </summary>
    internal static List<Product?> _products = new List<Product?>();

    /// <summary>
    /// List of _orders
    /// </summary>
    internal static List<Order?> _orders = new List<Order?>();

    /// <summary>
    /// List of _orderItems
    /// </summary>
    internal static List<OrderItem?> _orderItems = new List<OrderItem?>();


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
        productInitialize();
        orderItemInitialize();
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
            order.Id = config.SeqNumOr;//adds to the Id 1
            string customerFirstName = CustomerFirstName[random.Next(0, 14)];
            string customerLastName = CustomerLastName[random.Next(0, 14)];
            order.CustomerName = customerFirstName + " " + customerLastName;
            order.CustomerEmail = customerFirstName + customerLastName + order.Id + "@gmail.com";
            order.CustomerAddress = Customer_Adress[random.Next(0, 11)];
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

            _orders.Add(order);  //adds the new order to the order's list.
        }
    }

    /// <summary>
    /// initialize the products list
    /// </summary>
    static void productInitialize()//initializing products
    {
        string[] images = { "C:\\dotNet5783_9018_9591\\PL\\photoes\\percussions\\A set of cymbals.jpg",
            "C:\\dotNet5783_9018_9591\\PL\\photoes\\string instruments\\black electric guitar.jpg",
        "C:\\dotNet5783_9018_9591\\PL\\photoes\\wind instruments\\10 hole harmonica.jpg",
        "C:\\dotNet5783_9018_9591\\PL\\photoes\\keyboard\\Electric piano 88 keys.jpg",
        "C:\\dotNet5783_9018_9591\\PL\\photoes\\bow instruments\\violin 4.4.jpg",
        "C:\\dotNet5783_9018_9591\\PL\\photoes\\percussions\\wooden drum.jpg",
        "C:\\dotNet5783_9018_9591\\PL\\photoes\\string instruments\\red electric guitar.jpg",
        "C:\\dotNet5783_9018_9591\\PL\\photoes\\wind instruments\\trombone.jpg",
        "C:\\dotNet5783_9018_9591\\PL\\photoes\\keyboard\\Grand piano.jpg",
        "C:\\dotNet5783_9018_9591\\PL\\photoes\\bow instruments\\electric violin.jpg"};
        int AmountOfProducts = 10;
        for (int i = 1; i <= AmountOfProducts; i++)
        {
            Product product = new Product();//create a new object
            do
            {
                product.Id = 100000 + i;//random.Next(100000, 999999);
            }
            while (_products.Exists(x => x?.Id == product.Id));
            product.Name = "product" + i;
            product.Price = random.Next(200, 2000);
            product.Category = (Category)(i % 5);
            product.Image = images[i-1];
            if (i <= AmountOfProducts * 0.05 + 1)
                product.InStock = 0;
            else
                product.InStock = i * 3;
            _products.Add(product);
        }
    }


    /// <summary>
    /// initialize the products list
    /// </summary>
    //static void productInitialize()//initializing products
    //{
    //    DirectoryInfo directoryInfo = new DirectoryInfo("C:\\Users\\user\\Desktop\\לימודים שנה ב\\מיניפ\\instruments photos");
    //    List<Category> categories = Enum.GetValues(typeof(Category)).Cast<Category>().ToList();

    //    Dictionary<string, List<string>> imagesNames = directoryInfo.GetDirectories()
    //        .Where(f => f.Name != "background photos" || f.Name != "tools")
    //        .Select(f => (f.Name, f.GetFiles().Select(f => f.Name)))
    //        .ToDictionary(f => f.Name.Trim().ToUpper(), f => f.Item2.ToList());

    //    foreach (var category in categories)
    //    {
    //        foreach (var name in imagesNames[category.ToString().ToUpper()])
    //        {
    //            Product product = new Product();//create a new object
    //            do
    //            {
    //                product.Id = random.Next(100000, 999999);
    //            }
    //            while (_products.Exists(x => x?.Id == product.Id));

    //            product.Name = name;
    //            product.Price = random.Next(200, 2000);
    //            product.Category = category;

    //            if ((Convert.ToInt32(imagesNames.Count()) * 0.05) > 0)
    //                product.InStock = 0;
    //            else
    //                product.InStock = random.Next(3, 30);
    //            _products.Add(product);
    //        }
    //    }

        
    //}

    /// <summary>
    /// initialize the order items list
    /// </summary>
    static void orderItemInitialize()//initializing orderItems 
    {
        foreach (var order in _orders)
        {
            int number = random.Next(1, 4);
            for (int i = 0; i < number; i++)
            { 
                OrderItem orderItem = new OrderItem();//create a new object
                orderItem.Id = config.SeqNumOi;//adds to the Id 1;
                orderItem.OrderID = (int)(order?.Id)!;
                Product p = (Product)_products[random.Next(0, 9)]!;
                orderItem.ProductID = p.Id;
                orderItem.Price = p.Price;
                orderItem.Amount = random.Next(1, 4);
                _orderItems.Add(orderItem);
            }
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
