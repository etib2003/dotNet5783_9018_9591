using DO;
using System.Windows.Markup;
using static DO.Enums;

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
        string[] CustomerFirstName = { "Shawn" , "Chavi" , "Eti", "Michal", "Shir",
                                       "Gad","Dan","Tal","Ori","Gal","Adam",
                                       "Hadar", "Bibi", "Eden", "Daniel" };
        string[] CustomerLastName = { "Mendes" , "Bombach" , "Bernat", "Gad", "Ben",
                                      "Dan","Bar","Cohen","Gal","Adam","Prais",
                                      "Muchtar", "Netanyahu", "Ben_Zaken", "Yona" };
        string[] Customer_Adress =  { "Pinkas 1,Bnei Brak", "Eliezer 54, Petach Tikva", "David 12,Herzelia", "Herzog 14,Bnei Brak",
                                      "gehula 30, jerusalem","Mohaliver 9, Bnei Brak", "Menachem 7, Tel Aviv", "Byalik 87, Ramat Gan",
                                      "Daniel 8, Beit Shemesh", "Segal 21, Netania", "Havradim 3, Chulon","Rotshild 17, Tel Aviv" };

        
        for (int i = 1; i <= 20; i++)
        {
            Order order = new Order();
            //לוודא שלא יצא אותו בן אדם- נשלח דרך create

            order.seqNum = config._LastIdOr;
            string CustomerFName = CustomerFirstName[random.Next(0, 14)];
            string CustomerLName = CustomerLastName[random.Next(0, 14)];
            order.CustomerName = CustomerFName + " " + CustomerLName;
            order.CustomerEmail = CustomerFName + CustomerLName + "@gmail.com";
            order.CustomerAdress = Customer_Adress[random.Next(0, 14)];
            order.OrderDate = DateTime.Now.AddMonths(random.Next(-5, -1));
            if (i <= 16)
            {
                order.ShipDate = order.OrderDate.AddDays(random.Next(1, 3));//לבדוק
                if (i <= 12)
                    order.DeliveryDate = order.ShipDate.AddDays(random.Next(7, 21));
            }
            Orders.Add(order); //Orders.create(order);
            //Orders.Add(new Order() { CustomerAdress = order.CustomerAdress, OrderDate = order.OrderDate });
        }

    }

    static void ProductInitialize()
    {
        Array Values = Enum.GetValues(typeof(category));
        //Product product = new Product();
        for (int i = 1; i <= 10; i++)
        {
             Product product = new Product();
            do
            { product.ID = random.Next(100000, 99999); }
            while (Products.Exists(x => x.ID == product.ID));

            product.Name = "product" + i;
            product.Price = random.Next(400, 2000);

            //product.Category = (category)values.Getvalue(random.Next(Values.Length));
            product.Category = (Enums.category)random.Next(0, 4); //לבדוק איך יודעים שיש את כל הקטגוריות
            if (i == 1)
                product.InStock = 0;
            else
                product.InStock = i * 3;
            Products.Add(product);
        }

    }

    static void OrderItemInitialize()
    {
        //OrderItem orderItem = new OrderItem();
        for (int i = 1; i <= 40; i++)
        {
            OrderItem orderItem = new OrderItem();
            orderItem.seqNum = config._LastIdOi;
            orderItem.OrderID = Orders[random.Next(0, 19)].seqNum;
            Product p = Products[random.Next(0, 9)];
            orderItem.ProductID = p.ID;
            orderItem.Price = p.Price;
            orderItem.Amount = random.Next(1, 4);
            OrderItems.Add(orderItem);
        }
    }

    internal static class config
    {
        static private int LastIdOr = 100000;
        static private int LastIdOi = 100000;

        //public static int _LastIdOr => LastIdOr++;  צורה  מקוצרת
        // public static int _LastIdOi=> _LastIdOi++

        public static int _LastIdOr
        {
            get { return LastIdOr++; }
        }

        public static int _LastIdOi
        {
            get { return LastIdOi++; }
        }

        static int indexOr = 0;
        static int indexpdct = 0;
        static int indexOi = 0;
    }


}




/*
 * 
 * 
 * 
 * 
 * 
 * לאה:
using DO;

namespace Dal;

internal static class DataSource
{
    /// <summary>
    /// 
    /// </summary>
    public static readonly Random rand = new Random();

    internal static List<Order> _lstOreders = new List<Order>();
    internal static List<OrderItem> _lstOrderItems = new List<OrderItem>();
    internal static List<Product> _lstPruducts = new List<Product>();

    static DataSource()
    {
        s_Initialize();
    }
    private static void s_Initialize()
    {
        // array of product's possible names
        string[] productsNamesArray = { "product1", "product2", "product3", "product4", "product5", "product6", "product7", "product8", "product9", "product10" };

        string[] firstNames = { "Sara", "Rebeka", "Rachel", "Leah", "Naomi" };

        string[] lastNames = { "Cohen", "Levi", "Israel", "Goldenkoif", "Kachanelbuge" };

        string[] cities = { "Jerusalem", "Ramat Gan", "Bnei Brak", "Beit Shemesh", "Ashdod" };

        string[] streets = { "Ben Guryon", "Habrosh", "Hazait", "Vaitzman", "Begin" };

        for (int i = 0; i < 10; i++)
        {
            // create new product
            Product product = new Product();

            // draws an id while there's alredy a product in the list with the same id
            do
            {
                product.ID = rand.Next(100000, 999999);
            }
            while (_lstPruducts.Exists(x => x.ID == product.ID));

            // select the name of the product from the names array
            product.Name = productsNamesArray[i];
            // draw the price of the product
            product.Price = (double)rand.Next(50, 200);
            // the stock is 0 the  first time
            product.InStock = i;
            //draw the category of the froduct
            product.Category = (Enums.Category)rand.Next(0, 4);

            // add the product to the list
            _lstPruducts.Add(product);
        }

        for (int i = 0; i < 20; i++)
        {
            // create new order
            Order order = new Order();

            // gets the next available id
            order.ID = config._OrderID;
            // draw a name and last name from the names and last names arrays
            string custumerFirstName = firstNames[rand.Next(0, 4)];
            string custumerLastName = lastNames[rand.Next(0, 4)];

            order.CustumerName = custumerFirstName + " " + custumerLastName;
            order.CustumerEmail = custumerFirstName + custumerLastName + "@gmail.com";
            // draw a city and a street fron the cities and streeats arrays
            order.CustumerAdress = streets[rand.Next(0, 4)] + " " + rand.Next(1, 100) + " " + cities[rand.Next(0, 4)];
            // draw a date in the rang between last year and two months ago
            order.OrderDate = DateTime.Now.AddMonths(rand.Next(-12, -2));

            //TimeSpan timeSpan = (TimeSpan)(order.OrderDate.Value.AddMonths(2)-order.OrderDate) ;

            // draw a date in the range between the order date and 7 days after
            order.ShipDate = order.OrderDate.Value.AddDays(rand.Next(7, 21));
            // draw a date in the range between the ship date and 2 days after
            order.DeliveryDate = order.ShipDate.Value.AddDays(rand.Next(2, 6));

            _lstOreders.Add(order);
        }
        for(int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                OrderItem orderItem = new OrderItem();

                orderItem.OrderItemID = config._ItemOrderID;
                Product p = _lstPruducts[rand.Next(0, 9)];
                orderItem.ProductID = p.ID;
                orderItem.Price = p.Price;
                orderItem.OrderID = 100000 + i;
                orderItem.Amount = rand.Next(1, 10);

                _lstOrderItems.Add(orderItem);
            }
        }

    }

private static void addOrder(Order or)
    {
        _lstOreders.Add(or);
    }
    private static void addProduct(Product pro)
    {
        _lstPruducts.Add(pro);
    }
    private static void addOrderedItem(OrderItem orit)
    {
        _lstOrderItems.Add(orit);
    }

    internal static class config
    {
        private static int _itemOrderID = 100000;
        private static int _orderID = 100000;

        public static int _ItemOrderID
        {
            get { return _itemOrderID++; }
        }

        public static int _OrderID
        {
            get { return _orderID++; }
        }///
    }

}
 * */
