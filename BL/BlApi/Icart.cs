using Do;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface ICart
{
    public Do.Cart AddProductToCart(Do.Cart cart, int productId);
    public Do.Cart UpdateAmountOfProduct(Do.Cart cart, int productId,int newAmount);
    public void CommitOrder(Do.Cart cart, string customerName, string customerEmail, string customerAdress);
}
