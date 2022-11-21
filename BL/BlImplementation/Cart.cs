using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;


namespace BlImplementation;

internal class Cart:ICart
{
     IDal dalList = new DalList();

    public void CommitOrder(Cart cart, string customerName, string customerEmail, string customerAdress)
    {

    }

    public BO.Cart AddProductToCart(BO.Cart cart, int productId)
    {
        throw new NotImplementedException();
    }

    public BO.Cart UpdateAmountOfProduct(BO.Cart cart, int productId, int amount)
    {
        throw new NotImplementedException();
    }

    public void CommitOrder(BO.Cart cart, string customerName, string customerEmail, string customerAdress)
    {
        throw new NotImplementedException();
    }
}
