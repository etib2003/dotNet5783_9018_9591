using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface ICart
{
    BO.Cart AddProductToCart(BO.Cart cart, int productId);
    BO.Cart UpdateAmountOfProduct(BO.Cart cart, int productId, int newAmount);
    void CommitOrder(BO.Cart cart);

}
