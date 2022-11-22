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
    public BO.ProductItem GetProductDetailsForCustomer(int productId, BO.Cart cart);

    public int AddProduct(BO.Product p);
    public void UpdateProduct(BO.Product p);
    public void DeleteProduct(int productId);


}
