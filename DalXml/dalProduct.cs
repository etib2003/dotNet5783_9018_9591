
using DO;
using DO;
namespace Dal;

internal class dalProduct : IProduct
{
    //Access to the xml files
    string path = @"../xml/products.xml";

    /// <summary>
    /// add a new product to the file
    /// </summary>
    /// <param name="product">the product you want to add</param>
    /// <returns>the added product id</returns>
    /// <exception cref="DalAlreadyExistsException"></exception>
    public int Create(Product product)
    {
        List<Product> prodLst = XmlTools.LoadListFromXMLSerializer<Product>(path);//load all the products from the file

        if (prodLst.Exists(x => x.Id == product.Id))
            throw new DalAlreadyExistsException("Product");

        prodLst.Add(product);//add the product
        XmlTools.SaveListToXMLSerializer(prodLst, path);//save the changes to the file
        return product.Id;
    }

    /// <summary>
    /// delete a product
    /// </summary>
    /// <param name="id">the product id</param>
    public void Delete(int id)
    {
        List<Product> prodLst = XmlTools.LoadListFromXMLSerializer<Product>(path);
        prodLst.Remove(GetById(id));
        XmlTools.SaveListToXMLSerializer(prodLst, path);
    }

    /// <summary>
    /// gets a product that matches the condition
    /// </summary>
    /// <param name="cond">condition</param>
    /// <returns>product</returns>
    /// <exception cref="DalDoesNoExistException"></exception>
    public Product Get(Func<Product?, bool>? cond)
    {
        return XmlTools.LoadListFromXMLSerializer<DO.Product?>(path).FirstOrDefault(cond!)
             ?? throw new DalDoesNoExistException("Product");
    }

    /// <summary>
    /// get the right product
    /// </summary>
    /// <param name="id">the product id</param>
    /// <returns>the product</returns>
    public Product GetById(int id)
    {
        return Get(x => x?.Id == id);
    }

    /// <summary>
    /// gets a list of all the products that matches the given condition
    /// </summary>
    /// <param name="cond">condition</param>
    /// <returns>a list of products</returns>
    public IEnumerable<Product?> RequestAll(Func<Product?, bool>? cond = null)
    {
        List<DO.Product?> prodList = XmlTools.LoadListFromXMLSerializer<DO.Product?>(path);

        if (cond == null)
            return prodList.AsEnumerable();

        return prodList.Where(cond);
    }

    /// <summary>
    /// update a product
    /// </summary>
    /// <param name="product">the new product</param>
    public void Update(Product product)
    { 
        Delete(product.Id);
        List<Product> prodLst = XmlTools.LoadListFromXMLSerializer<Product>(path);
        prodLst.Add(product);
        XmlTools.SaveListToXMLSerializer(prodLst, path);//save the changes
    }
}

