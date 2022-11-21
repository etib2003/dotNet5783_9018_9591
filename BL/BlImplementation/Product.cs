using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;


namespace BlImplementation;

internal class Product : IProduct
{
    private IDal dal = new DalList();
    public void AddProduct(BO.Product p)
    {
        throw new NotImplementedException();
    }

    public void DeleteProduct(BO.Product p)
    {
        throw new NotImplementedException();
    }

    public BO.Product GetProductDetailsForCustomer(int productId)
    {
        throw new NotImplementedException();
    }

    public BO.Product GetProductDetailsForManager(int productId)
    {
        throw new NotImplementedException();
    }

    public BO.ProductForList GetProductList()
    {
        throw new NotImplementedException();
    }

    public BO.ProductItem RequestCatalog()
    {
        throw new NotImplementedException();
    }

    public void UpdateProduct(BO.Product p)
    {
        throw new NotImplementedException();
    }
}
