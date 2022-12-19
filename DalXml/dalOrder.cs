using Dal;
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
            throw new Exception("order File upload problem" + ex.Message);
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
        int.TryParse(configRoot.Element("orderSeq").Value, out int nextSeqNum);
        nextSeqNum++;
        Or.Id = nextSeqNum;
        //update config file
        configRoot.Element("orderSeq").SetValue(nextSeqNum);
        configRoot.Save(configPath);
        List<Order> OrLst = XmlTools.LoadListFromXMLSerializer<Order>(path);
        if (OrLst.Exists(x => x.Id == Or.Id))
            throw new DalAlreadyExistsException("Order");
        OrLst.Add(Or);
        XmlTools.SaveListToXMLSerializer(OrLst, path);
        return Or.Id;
    }

    public void Delete(int id)
    {
        List<Order> OrLst = XmlTools.LoadListFromXMLSerializer<Order>(path);
        OrLst.Remove(GetById(id));
        XmlTools.SaveListToXMLSerializer(OrLst, path);
    }

    public Order Get(Func<Order?, bool>? cond)
    {
        return XmlTools.LoadListFromXMLSerializer<DO.Order?>(path).FirstOrDefault(cond!)
            ?? throw new DalDoesNoExistException("Order");
    }

    public Order GetById(int id)
    {
        return Get(x => x?.Id == id);
    }

    public IEnumerable<Order?> RequestAll(Func<Order?, bool>? cond = null)
    {
        List<DO.Order?> OrLst = XmlTools.LoadListFromXMLSerializer<DO.Order?>(path);

        if (cond == null)
            return OrLst.AsEnumerable();

        return OrLst.Where(cond);
    }

    public void Update(Order Or)
    {
        List<Order> OrLst = XmlTools.LoadListFromXMLSerializer<Order>(path);
        Delete(Or.Id);
        OrLst.Add(Or);
        XmlTools.SaveListToXMLSerializer(OrLst, path);
    }
}


