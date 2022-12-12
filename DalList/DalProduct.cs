using DalApi;

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