using DalApi;
using DO;
using System.Xml.Linq;
namespace Dal;

internal class dalOrderItem : IOrderItem
{    
    //Access to the xml files
    string path =@"../xml/orderItems.xml";
    string configPath = @"../xml/config.xml";
    XElement ordersItemsRoot;
    /// <summary>
    /// constructor
    /// </summary>
    public dalOrderItem()
    {
        LoadData();
    }

    /// <summary>
    /// A function to load data from the  xml files
    /// </summary>
    /// <exception cref="Exception"></exception>
    private void LoadData()
    {
        try
        {
            if (File.Exists(path))//checks if the file exists
                ordersItemsRoot = XElement.Load(path);
            else
            {
                ordersItemsRoot = new XElement("orderItems");//create a root
                ordersItemsRoot.Save(path); //save to the file
            }
        }
        catch (Exception ex)
        {
            throw new Exception("order Item File upload problem" + ex.Message);
        }
    }

    /// <summary>
    /// create a new orderItem and save it to the file
    /// </summary>
    /// <param name="Oi">the orderItem you want to add</param>
    /// <returns>the addad orderItem id</returns>
    public int Create(OrderItem Oi)
    {
        //Read config file
        XElement configRoot = XElement.Load(configPath);
        int.TryParse(configRoot.Element("orderItemSeq").Value, out int nextSeqNum);
        nextSeqNum++;
        Oi.Id = nextSeqNum;
        //update config file
        configRoot.Element("orderItemSeq").SetValue(nextSeqNum);
        configRoot.Save(configPath);
        //update the data
        XElement Id = new XElement("Id", Oi.Id);
        XElement OrderID = new XElement("OrderID", Oi.OrderID);
        XElement ProductID = new XElement("ProductID", Oi.ProductID);
        XElement Price = new XElement("Price", Oi.Price);
        XElement Amount = new XElement("Amount", Oi.Amount);

        ordersItemsRoot.Add(new XElement("OrderItem", Id, OrderID, ProductID, Price, Amount));//add the xelement of the orderItem to the file
        ordersItemsRoot.Save(path);//save the changes
        return Oi.Id;
    }

    /// <summary>
    /// delete an order item from the file
    /// </summary>
    /// <param name="id">the orderItem id</param>
    /// <exception cref="DalDoesNoExistException"></exception>
    public void Delete(int id)
    {
        //finds the right orderItem:
        XElement OiElement = (from oi in ordersItemsRoot.Elements()
                              where int.Parse(oi.Element("id").Value) == id
                              select oi).FirstOrDefault() ?? throw new DalDoesNoExistException("Order Item");
        OiElement.Remove();//remove the orderItem from the file
        ordersItemsRoot.Save(path);//save the changes
    }

    /// <summary>
    /// get a list of orderItems that matches the given condition
    /// </summary>
    /// <param name="cond">condition</param>
    /// <returns> a list of orderItems that matches the given condition</returns>
    /// <exception cref="DalDoesNoExistException"></exception>
    public OrderItem Get(Func<OrderItem?, bool>? cond)
    {
        return RequestAll(cond).Single() ?? throw new DalDoesNoExistException("Order Item");
    }

    /// <summary>
    /// gets an orderItem that matches the id
    /// </summary>
    /// <param name="id">orderItem id</param>
    /// <returns>orderItem</returns>
    public OrderItem GetById(int id)
    {
        return Get(orderItem => orderItem?.Id == id);
    }

    /// <summary>
    /// get a list of OrderItems that match the condition
    /// </summary>
    /// <param name="cond">condition</param>
    /// <returns>list of orderItems</returns>
    public IEnumerable<OrderItem?> RequestAll(Func<OrderItem?, bool>? cond = null)
    {
        return (from orderItem in ordersItemsRoot.Elements()
                                         select (OrderItem?) new OrderItem
                                         {
                                             //convert from string to the right type:
                                             Id = int.Parse(orderItem.Element("Id")!.Value),
                                             OrderID = int.Parse(orderItem.Element("OrderID")!.Value),
                                             ProductID = int.Parse(orderItem.Element("ProductID")!.Value),
                                             Price = double.Parse(orderItem.Element("Price")!.Value),
                                             Amount = int.Parse(orderItem.Element("Amount")!.Value),

                                         }).Where(o => cond is null ? true : cond(o));

    }

    /// <summary>
    /// update an orderItem
    /// </summary>
    /// <param name="Oi">orderItem</param>
    /// <exception cref="DalDoesNoExistException"></exception>
    public void Update(OrderItem Oi)
    {
        //gets the right orderItem
        XElement OiElement = (from oi in ordersItemsRoot.Elements()
                                   where int.Parse(oi.Element("id").Value) == Oi.Id
                                   select oi).FirstOrDefault() ?? throw new DalDoesNoExistException("Order Item");

        //update the data:
        OiElement.Element("OrderID").Value = Oi.OrderID.ToString();
        OiElement.Element("ProductID").Value = Oi.ProductID.ToString();
        OiElement.Element("Price").Value = Oi.Price.ToString();
        OiElement.Element("Amount").Value = Oi.Amount.ToString();
        ordersItemsRoot.Save(path);//save the changes
    }
}

