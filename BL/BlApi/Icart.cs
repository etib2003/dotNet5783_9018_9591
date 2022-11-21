using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface ICart
{
    public Cart AddProductToCart(Cart cart, int productId);
    public Cart UpdateAmountOfProduct(Cart cart, int productId,int amount);
    public void CommitOrder(Cart cart, string customerName, string customerEmail, string customerAdress);
}
