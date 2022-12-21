using Dal;
using DalApi;
using DO;
using System.Xml.Linq;
 

internal class dalOrder : IOrder
{
    //Access to the xml files
    string path = @"../xml/orders.xml"; 
    string configPath = @"../xml/config.xml";
    XElement ordersRoot;

    /// <summary>
    /// A function to load data from the  xml files
    /// </summary>
    /// <exception cref="Exception">order File upload problem</exception>
    private void LoadData()
    {
        try
        {
            if (File.Exists(path)) //checks if the file exists
                ordersRoot = XElement.Load(path);
            else
            {
                ordersRoot = new XElement("orders"); //create a root
                ordersRoot.Save(path); //save to the file
            }
        }
        catch (Exception ex)
        {
            throw new Exception("order File upload problem" + ex.Message);
        }
    }

    /// <summary>
    /// constructor
    /// </summary>
    public dalOrder() 
    {
        LoadData();
    }

    /// <summary>
    /// create a new order and save it to the file
    /// </summary>
    /// <param name="Or">the order you want to add</param>
    /// <returns>the addad order id</returns>
    /// <exception cref="DalAlreadyExistsException"></exception>
    public int Create(Order Or)
    {
        //Read config file
        XElement configRoot = XElement.Load(configPath);
        int.TryParse(configRoot.Element("orderSeq").Value, out int nextSeqNum);
        nextSeqNum++;
        Or.Id = nextSeqNum;
        //update config file
        configRoot.Element("orderSeq").SetValue(nextSeqNum);//update in the file the orderseq
        configRoot.Save(configPath);
        List<Order> OrLst = XmlTools.LoadListFromXMLSerializer<Order>(path);//load from the file all the orders
        if (OrLst.Exists(x => x.Id == Or.Id))
            throw new DalAlreadyExistsException("Order");
        OrLst.Add(Or);//add the order
        XmlTools.SaveListToXMLSerializer(OrLst, path);//save the new order to the file
        return Or.Id;
    }

    /// <summary>
    /// delete an order
    /// </summary>
    /// <param name="id">the order id</param>
    public void Delete(int id)
    {
        List<Order> OrLst = XmlTools.LoadListFromXMLSerializer<Order>(path);//load a list of all the orders from the file
        OrLst.Remove(GetById(id));//delete the order
        XmlTools.SaveListToXMLSerializer(OrLst, path);//save the changes to the file
    }

    /// <summary>
    /// load an order from the file that matches the given condition
    /// </summary>
    /// <param name="cond">condition</param>
    /// <returns>list of orders</returns>
    /// <exception cref="DalDoesNoExistException"></exception>
    public Order Get(Func<Order?, bool>? cond)
    {
        return XmlTools.LoadListFromXMLSerializer<DO.Order?>(path).FirstOrDefault(cond!)
            ?? throw new DalDoesNoExistException("Order");
    }

    /// <summary>
    /// gets an order by its id
    /// </summary>
    /// <param name="id">order id</param>
    /// <returns>order</returns>
    public Order GetById(int id)
    {
        return Get(x => x?.Id == id);
    }

    /// <summary>
    /// gets all the orders that matches the cond
    /// </summary>
    /// <param name="cond">condition</param>
    /// <returns>list of orders</returns>
    public IEnumerable<Order?> RequestAll(Func<Order?, bool>? cond = null)
    {
        List<DO.Order?> OrLst = XmlTools.LoadListFromXMLSerializer<DO.Order?>(path);

        if (cond == null)
            return OrLst.AsEnumerable();

        return OrLst.Where(cond);
    }

    /// <summary>
    /// update an order
    /// </summary>
    /// <param name="Or">ordrer</param>
    public void Update(Order Or)
    {
        List<Order> OrLst = XmlTools.LoadListFromXMLSerializer<Order>(path);
        Delete(Or.Id);//delete the old one
        OrLst.Add(Or);//add the new one
        XmlTools.SaveListToXMLSerializer(OrLst, path);//save to the file
    }
}


