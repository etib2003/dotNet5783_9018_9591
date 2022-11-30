using DalApi;

using DO;
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
        if (dataSource._products.Exists(x => x.ID == product.ID))
            throw new DalAlreadyExistsException("Product");

        dataSource._products.Add(product);
        return product.ID;  
    }

    /// <summary>
    /// the function returns the products' list
    /// </summary>
    /// <returns>the products' list</returns >
    public IEnumerable<Product> RequestAll(Func<Product?, bool>? cond)
    {
        List<Product> listToReturn = new List<Product>();
        for (int i = 0; i < dataSource._products.Count; i++)
            listToReturn.Add(dataSource._products[i]);
        return listToReturn;
    }

    /// <summary>
    /// the function returns the product of the given id
    /// </summary>
    /// <param name="id"> the produc's id</param>
    /// <returns> a list of all the products with the given id</returns>
    /// <exception cref="the product does not exist"></exception >
    public Product RequestById(int id)
    {
        if (!dataSource._products.Exists(x => x.ID == id))
            throw new DalDoesNoExistException("Product");

        return dataSource._products.Find(x => x.ID == id);
    }

    /// <summary>
    /// the function updates a certain product with the given one
    /// </summary>
    /// <param name="product"> the new product you want to put instead of the old one</param >
    /// <exception cref="the product you want to update does not exist"></exception >
    public void Update(Product product)
    {
        //if product does not exist throw exception 
        if (!dataSource._products.Exists(x => x.ID == product.ID))
            throw new DalDoesNoExistException("Product");
        Product PdctToRemove = dataSource._products.Find(x => x.ID == product.ID); 
        dataSource._products.Remove(PdctToRemove);
        dataSource._products.Add(product);
    }

    /// <summary>
    /// the function deletes the product with the given id
    /// </summary>
    /// <param name="id">the id of the product you want to delete</param  >
    /// <exception cref="the product does not exist"></exception  >
    public void Delete(int id)
    {
        //if product does not exist throw exception 
        if (!dataSource._products.Exists(x => x.ID == id))
            throw new DalDoesNoExistException("Product");
        Product PdctToRemove = dataSource._products.Find(x => x.ID == id);
        dataSource._products.Remove(PdctToRemove);
    }

}