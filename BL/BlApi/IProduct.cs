using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  BlApi;

public interface IProduct
{
    public IEnumerable<BO.ProductForList> GetListProductForManagerAndCatalog();
    public BO.Product GetProductDetailsForManager(int productId);
    public int AddProduct(BO.Product p);
    public void UpdateProduct(BO.Product p);
    public void DeleteProduct(BO.Product p);
    public BO.Product GetProductDetailsForCustomer(int productId);


}
