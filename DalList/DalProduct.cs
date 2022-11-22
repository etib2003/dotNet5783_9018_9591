using DalApi;

using DO;
namespace Dal;
/// <summary>
/// The class of products
/// </summary>
internal class DalProduct : IProduct
{

    //CRUD for Products:

    /// <summary>
    /// the function adds a new product to the orders' list
    /// </summary>
    /// <param name="product"> the product you want to add</param>
    /// <returns>the added product id/barcode</returns>
    /// <exception cref="cannot create a product,that already exists"></exception>
    public int Create(Product product)
    {
        if (DataSource.Products.Exists(x => x.ID == product.ID))
            throw new DalAlreadyExistsException("Product");

        DataSource.Products.Add(product);
        return product.ID;  
    }

    /// <summary>
    /// the function returns the products' list
    /// </summary>
    /// <returns>the products' list</returns >
    public IEnumerable<Product> RequestAll()
    {
        List<Product> listToReturn = new List<Product>();
        for (int i = 0; i < DataSource.Products.Count; i++)
            listToReturn.Add(DataSource.Products[i]);
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
        if (!DataSource.Products.Exists(x => x.ID == id))
            throw new DalDoesNoExistException("Product");

        return DataSource.Products.Find(x => x.ID == id);
    }

    /// <summary>
    /// the function updates a certain product with the given one
    /// </summary>
    /// <param name="product"> the new product you want to put instead of the old one</param >
    /// <exception cref="the product you want to update does not exist"></exception >
    public void Update(Product product)
    {
        //if product does not exist throw exception 
        if (!DataSource.Products.Exists(x => x.ID == product.ID))
            throw new DalDoesNoExistException("Product");
        Product PdctToRemove = DataSource.Products.Find(x => x.ID == product.ID); 
        DataSource.Products.Remove(PdctToRemove);
        DataSource.Products.Add(product);
    }

    /// <summary>
    /// the function deletes the product with the given id
    /// </summary>
    /// <param name="id">the id of the product you want to delete</param  >
    /// <exception cref="the product does not exist"></exception  >
    public void Delete(int id)
    {
        //if product does not exist throw exception 
        if (!DataSource.Products.Exists(x => x.ID == id))
            throw new DalDoesNoExistException("Product");
        Product PdctToRemove = DataSource.Products.Find(x => x.ID == id);
        DataSource.Products.Remove(PdctToRemove);
    }

}