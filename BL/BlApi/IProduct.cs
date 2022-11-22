using Do;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  BlApi;

public interface IProduct
{
    public IEnumerable<Do.ProductForList> GetListProductForManagerAndCatalog();
    public Do.Product GetProductDetailsForManager(int productId);
    public Do.ProductItem GetProductDetailsForCustomer(int productId, Cart cart);

    public int AddProduct(Do.Product p);
    public void UpdateProduct(Do.Product p);
    public void DeleteProduct(int productId);


}
