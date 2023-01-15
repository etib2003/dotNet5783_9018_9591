using DO;

using DO;
using System.Linq;

namespace Dal;
/// <summary>
/// The class of products
/// </summary>
internal class dalProduct : IProduct
{

    //CRUD for _products:

    /// <summary>
    /// the function adds a new product to the orders' list
    /// </summary>
    /// <param name="product"> the product you want to add</param>
    /// <returns>the added product id/barcode</returns>
    /// <exception cref="cannot create a product,that already exists"></exception>
    public int Create(Product product)
    {
        if (DataSource._products.Exists(x => x?.Id == product.Id))
            throw new DalAlreadyExistsException("Product");

        DataSource._products.Add(product);
        return product.Id;  
    }

    /// <summary>
    /// the function returns the product of the given id
    /// </summary>
    /// <param name="id"> the produc's id</param>
    /// <returns> a list of all the products with the given id</returns>
    /// <exception cref="the product does not exist"></exception >
    public Product GetById(int id)
    {
        return Get(product => product?.Id == id);
    }

    /// <summary>
    /// the function updates a certain product with the given one
    /// </summary>
    /// <param name="product"> the new product you want to put instead of the old one</param >
    /// <exception cref="the product you want to update does not exist"></exception >
    public void Update(Product product)
    {
        //if product does not exist throw exception 
        if(GetById(product.Id) is Product pdct)
        {
            DataSource._products.Remove(pdct);
            DataSource._products.Add(product);
        }
    }

    /// <summary>
    /// the function deletes the product with the given id
    /// </summary>
    /// <param name="id">the id of the product you want to delete</param  >
    /// <exception cref="the product does not exist"></exception  >
    public void Delete(int id)
    {
        DataSource._products.Remove(GetById(id));
    }

    /// <summary>
    /// the function returns the product according to the given condition
    /// </summary>
    /// <param name="cond">the given condition</param>
    /// <returns>product according to the given condition</returns>
    /// <exception cref="the product you want to get does not exist"></exception>
    public Product Get(Func<Product?, bool>? cond)
    {
        return DataSource._products.FirstOrDefault(cond!) ?? throw new DalDoesNoExistException("Product");
    }
    /// <summary>
    /// the function returns a list of product according to the given condition
    /// </summary>
    /// <param name="cond">the given condition</param>
    /// <returns>a list of products according to the given condition</returns>
    IEnumerable<Product?> ICrud<Product>.RequestAll(Func<Product?, bool>? cond)
    {
        return DataSource._products.Where(product => cond is null ? true : cond!(product));
    }
}

/*
 //dal-config.xml
<?xml version="1.0" encoding="UTF-8"?>
<config>
  <dal>list</dal>
  <dal-packages>
    <list>DalList</list>
    <xml>DalXml</xml>
  </dal-packages>
</config>

//static class dal config to read the dal-config.xml file to static fields  s_dalName and  s_dalPackages
namespace DO;
using System.Xml.Linq;

static class DalConfig
{
    internal class DalImplementation
    {
        internal string? _package;
        internal string? _namespace;
        internal string? _class;
    }

    internal static string s_dalName;
    internal static Dictionary<string, DalImplementation> s_dalPackages;

    static DalConfig()
    {
        XElement dalConfig = XElement.Load(@"..\xml\dal-config.xml") ?? throw new DalConfigException("dal-config.xml file is not found");
        s_dalName = dalConfig.Element("dal")?.Value ?? throw new DalConfigException("<dal> element is missing");
        var packages = dalConfig.Element("dal-packages")?.Elements() ?? throw new DalConfigException("<dal-packages> element is missing");
        s_dalPackages = (from item in packages
                         let pkg = item.Value
                         let ns = item.Attribute("namespace")?.Value ?? "Dal"
                         let cls = item.Attribute("class")?.Value ?? pkg
                         select new
                         {
                             item.Name,
                             Value = new DalImplementation { _package = pkg, _namespace = ns, _class = cls }
                         }
                      ).ToDictionary(p => "" + p.Name, p => p.Value);
    }
}

[Serializable]
public class DalConfigException : Exception
{
    public DalConfigException(string msg) : base(msg) { }
    public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
}


//create DalFactory in DalFacade
namespace DO;
using System.Reflection;
using static DO.DalConfig;

public static class Factory
{
    public static IDal Get()
    {
        string dalType = s_dalName ?? throw new DalConfigException($"DAL name is not extracted from the configuration");
        DalImplementation dal = s_dalPackages[dalType] ?? throw new DalConfigException($"Package for {dalType} is not found in packages list in dal-config.xml");

        try { Assembly.Load(dal._package ?? throw new DalConfigException($"Package {dal._package} is null")); }
        catch (Exception) { throw new DalConfigException($"Failed to load {dal._package}.dll package"); }

        Type type = Type.GetType($"{dal._namespace}.{dal._class}, {dal._package}") ??
            throw new DalConfigException($"Class {dal._namespace}.{dal._class} was not found in {dal._package}.dll");

        return type.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) as IDal ??
            throw new DalConfigException($"Class {dal._class} is not a singleton or wrong property name for Instance");
    }
}



//dalList and dalXML implement Idal and Singleton
using DO;

namespace Dal;

sealed internal class DalList : IDal
{
    public static IDal Instance { get; } = new DalList();
    private DalList() { }

    public IOrder Order => new Order();

    public IProduct Product => new Product();

    public IOrderItem OrderItem => new OrderItem();
}


using DO;

namespace Dal;

sealed internal class DalXml : IDal
{
    public static IDal Instance { get; } = new DalList();
    private DalList() { }

    public IOrder Order => new Order();

    public IProduct Product => new Product();

    public IOrderItem OrderItem => new OrderItem();
}

//project DALXML include DalXml class like DalList, and three classes for implements IOrder,IProduct and IOrderProduct interface
//in addithion a class with assistance functions:

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DL
{
    class XMLTools
    {
        static string dir = @"xml\";
        static XMLTools()
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }
       

        #region SaveLoadWithXMLSerializer
        public static void SaveListToXMLSerializer<T>(List<T> list, string filePath)
        {
            try
            {
                FileStream file = new FileStream(dir + filePath, FileMode.Create);
                XmlSerializer x = new XmlSerializer(list.GetType());
                x.Serialize(file, list);
                file.Close();
            }
            catch (Exception ex)
            {
                //throw new DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex);
            }
        }
        public static List<T> LoadListFromXMLSerializer<T>(string filePath)
        {
            try
            {
                if (File.Exists(dir + filePath))
                {
                    List<T> list;
                    XmlSerializer x = new XmlSerializer(typeof(List<T>));
                    FileStream file = new FileStream(dir + filePath, FileMode.Open);
                    list = (List<T>)x.Deserialize(file);
                    file.Close();
                    return list;
                }
                else 
                    return new List<T>();
            }
            catch (Exception ex)
            {
                throw new Exception(); // DO.XMLFileLoadCreateException(filePath, $"fail to load xml file: {filePath}", ex);
            }
        }
        #endregion
    }
}

//we can run a code for the create of xml file from the list in datasource just to start with something
 */