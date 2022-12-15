using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

internal class dalOrder : IOrder
{

    string path = "orders.xml";
    string configPath = "config.xml";
    XElement ordersRoot;

    private void LoadData()
    {
        try
        {
            if (File.Exists(path))
                ordersRoot = XElement.Load(path);
            else
            {
                ordersRoot = new XElement("orders");
                ordersRoot.Save(path);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("product File upload problem" + ex.Message);
        }
    }

    public dalOrder()
    {
        LoadData();
    }

    public int Create(Order Or)
    {

        //Read config file
        XElement configRoot = XElement.Load(configPath);

        int nextSeqNum = Convert.ToInt32(configRoot.Element("orderSeq").Value);
        nextSeqNum++;
        Or.Id = nextSeqNum;
        //update config file
        configRoot.Element("orderSeq").SetValue(nextSeqNum);
        configRoot.Save(configPath);

        XElement Id = new XElement("Id", Or.Id);
        XElement CustomerName = new XElement("CustomerName", Or.CustomerName);
        XElement CustomerEmail = new XElement("CustomerEmail", Or.CustomerEmail);
        XElement CustomerAdress = new XElement("CustomerAdress", Or.CustomerAdress);
        XElement OrderDate = new XElement("OrderDate", Or.OrderDate);
        XElement ShipDate = new XElement("ShipDate", Or.ShipDate);
        XElement DeliveryDate = new XElement("DeliveryDate", Or.DeliveryDate);

        ordersRoot.Add(new XElement("Order", Id, CustomerName, CustomerEmail, CustomerAdress, OrderDate, ShipDate, DeliveryDate));
        ordersRoot.Save(path);

        return Or.Id;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Order Get(Func<Order?, bool>? cond)
    {
        throw new NotImplementedException();
    }

    public Order GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Order?> RequestAll(Func<Order?, bool>? cond = null)
    {
        throw new NotImplementedException();
    }

    public void Update(Order Or)
    {
        throw new NotImplementedException();
    }
}


