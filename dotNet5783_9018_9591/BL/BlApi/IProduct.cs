using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  BlApi;

public interface IProduct
{
    public ProductForList GetProductList();
    public Product GetProductDetailsForManager(int productId);
    public void AddProduct(Product p);
    public void UpdateProduct(Product p);
    public void DeleteProduct(Product p);
    public ProductItem RequestCatalog();
    public Product GetProductDetailsForCustomer(int productId);



}
