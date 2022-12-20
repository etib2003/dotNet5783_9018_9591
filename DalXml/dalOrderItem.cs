using DalApi;
using DO;
using DocumentFormat.OpenXml.Office2010.Excel;
using System.Xml.Linq;

namespace Dal;

internal class dalOrderItem : IOrderItem
{
    string path =@"../xml/orderItems.xml";
    string configPath = @"../xml/config.xml";
    XElement ordersItemsRoot;
    public dalOrderItem()
    {
        LoadData();
    }

    private void LoadData()
    {
        try
        {
            if (File.Exists(path))
                ordersItemsRoot = XElement.Load(path);
            else
            {
                ordersItemsRoot = new XElement("orderItems");
                ordersItemsRoot.Save(path);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("order Item File upload problem" + ex.Message);
        }
    }
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

        XElement Id = new XElement("Id", Oi.Id);
        XElement OrderID = new XElement("OrderID", Oi.OrderID);
        XElement ProductID = new XElement("ProductID", Oi.ProductID);
        XElement Price = new XElement("Price", Oi.Price);
        XElement Amount = new XElement("Amount", Oi.Amount);

        ordersItemsRoot.Add(new XElement("OrderItem", Id, OrderID, ProductID, Price, Amount));
        ordersItemsRoot.Save(path);
        return Oi.Id;
    }

    public void Delete(int id)
    {
        XElement OiElement = (from oi in ordersItemsRoot.Elements()
                              where int.Parse(oi.Element("id").Value) == id
                              select oi).FirstOrDefault() ?? throw new DalDoesNoExistException("Order Item");
        OiElement.Remove();
        ordersItemsRoot.Save(path);
    }

    public OrderItem Get(Func<OrderItem?, bool>? cond)
    {
        return RequestAll(cond).Single() ?? throw new DalDoesNoExistException("Order Item");
    }

    public OrderItem GetById(int id)
    {
        return Get(orderItem => orderItem?.Id == id);
    }

    public IEnumerable<OrderItem?> RequestAll(Func<OrderItem?, bool>? cond = null)
    {
        return (from orderItem in ordersItemsRoot.Elements()
                                         select (OrderItem?) new OrderItem
                                         {
                                             Id = int.Parse(orderItem.Element("Id")!.Value),
                                             OrderID = int.Parse(orderItem.Element("OrderID")!.Value),
                                             ProductID = int.Parse(orderItem.Element("ProductID")!.Value),
                                             Price = double.Parse(orderItem.Element("Price")!.Value),
                                             Amount = int.Parse(orderItem.Element("Amount")!.Value),

                                         }).Where(o => cond is null ? true : cond(o));

    }

    public void Update(OrderItem Oi)
    {
        XElement OiElement = (from oi in ordersItemsRoot.Elements()
                                   where int.Parse(oi.Element("id").Value) == Oi.Id
                                   select oi).FirstOrDefault() ?? throw new DalDoesNoExistException("Order Item");

        OiElement.Element("OrderID").Value = Oi.OrderID.ToString();
        OiElement.Element("ProductID").Value = Oi.ProductID.ToString();
        OiElement.Element("Price").Value = Oi.Price.ToString();
        OiElement.Element("Amount").Value = Oi.Amount.ToString();
        ordersItemsRoot.Save(path);
    }
}

