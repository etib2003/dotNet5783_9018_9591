using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IProduct
{
    /// <summary>
    /// Get a list of products for the manager and the catalog
    /// </summary>
    /// <returns>list of products</returns>
    public IEnumerable<BO.ProductForList?> GetListProductForManagerAndCatalog();

    public IEnumerable<BO.ProductItem> GetListProductForCatalog(BO.Cart cart);

    /// <summary>
    /// Get a list of products for the manager and the catalog by condition
    /// </summary>
    /// <param name="cond">the condition according to which a list is returned</param>
    /// <returns>a list by condition</returns>
    public IEnumerable<BO.ProductForList> GetListProductForManagerAndCatalogByCond(Func<ProductForList?, bool>? cond);


    /// <summary>
    /// Gets a product's details for the manager
    /// </summary>
    /// <param name="productId">the product's id</param>
    /// <returns>product</returns>
    /// <exception cref="the product does not exist"></exception>
    public BO.Product GetProductDetailsForManager(int productId);

    /// <summary>
    ///  Gets a product's details for the customer
    /// </summary>
    /// <param name="productId">the product's id</param>
    /// <param name="cart">the customer's cart of products</param>
    /// <returns>product item</returns>
    /// <exception cref="the product does not exist"></exception>
    public BO.ProductItem GetProductDetailsForCustomer(int productId, BO.Cart cart);

    /// <summary>
    /// add a product
    /// </summary>
    /// <param name="product">the product you want to add</param>
    /// <returns>product id</returns>
    public int AddProduct(BO.Product p);

    /// <summary>
    /// update a product
    /// </summary>
    /// <param name="product">the product you want to update</param>
    public void UpdateProduct(BO.Product p);

    /// <summary>
    /// delete a product
    /// </summary>
    /// <param name="productId"> the product's id that you want to delete</param>
    /// <exception cref="product Already In Order Prosses"></exception>
    public void DeleteProduct(int productId);
}