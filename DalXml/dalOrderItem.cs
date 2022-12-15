using DalApi;
using DO;
using System.Xml.Linq;

namespace Dal;

internal class dalOrderItem : IOrderItem
{
    string path = XmlTools.dir + "ordersItems.xml";
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
                ordersItemsRoot = new XElement("ordersItems");
                ordersItemsRoot.Save(path);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("product File upload problem" + ex.Message);
        }
    }
    public int Create(OrderItem Or)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public OrderItem Get(Func<OrderItem?, bool>? cond)
    {
        return RequestAll(cond).Single() ?? throw new Exception();
    }

    public OrderItem GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<OrderItem?> RequestAll(Func<OrderItem?, bool>? cond = null)
    {
        return (IEnumerable<OrderItem?>)(from orderItem in ordersItemsRoot.Elements()
                                         select new OrderItem
                                         {
                                             Id = int.Parse(orderItem.Element("Id").Value),
                                             OrderID = int.Parse(orderItem.Element("OrderID").Value),
                                             ProductID = int.Parse(orderItem.Element("ProductID").Value),
                                             Price = int.Parse(orderItem.Element("Price").Value),
                                             Amount = int.Parse(orderItem.Element("Amount").Value),


                                         }).Where(orderItem => cond is null ? true : cond(orderItem));

    }

    public void Update(OrderItem Or)
    {
        throw new NotImplementedException();
    }
}

