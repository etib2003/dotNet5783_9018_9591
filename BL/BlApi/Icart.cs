using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface ICart
{
    public BO.Cart AddProductToCart(BO.Cart cart, int productId);
    public BO.Cart UpdateAmountOfProduct(BO.Cart cart, int productId,int newAmount);
    public void CommitOrder(BO.Cart cart, string customerName, string customerEmail, string customerAdress);
}
