using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;

namespace BlImplementation;

internal class Cart:ICart
{
    public BO.Cart AddProductToCart(Cart cart, int productId)
    {

    }
    public BO.Cart UpdateAmountOfProduct(Cart cart, int productId, int amount)
    {

    }
    public void CommitOrder(Cart cart, string customerName, string customerEmail, string customerAdress)
    {

    }
}
