using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  BlApi;

public interface IProduct
{
    public List<ProductForList> GetListProductForManager();
    public BO.Product GetProductDetailsForManager(int productId);
    public int AddProduct(BO.Product p);
    public void UpdateProduct(BO.Product p);
    public void DeleteProduct(BO.Product p);
    public List<BO.ProductItem> RequestCatalog();
    public BO.Product GetProductDetailsForCustomer(int productId);


}
