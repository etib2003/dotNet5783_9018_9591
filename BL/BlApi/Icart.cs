using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface ICart
{
    /// <summary>
    /// Add a product to the cart
    /// </summary>
    /// <param name="cart">the customer's cart</param>
    /// <param name="productId">the barcode of the product you want to add to the cart</param>
    /// <returns>cart</returns>
    /// <exception cref="product does not exist"></exception> 
    BO.Cart AddProductToCart(BO.Cart cart, int productId);

    /// <summary>
    /// Update the amount of the product in the cart
    /// </summary>
    /// <param name="cart">the customer's cart</param>
    /// <param name="productId">the product's barcode</param>
    /// <param name="newAmount">the new amount of the product that you want</param>
    /// <returns>cart</returns>
    /// <exception cref="the orderItem does not exist"></exception>
    /// <exception cref="product does not exist"></exception>
    BO.Cart UpdateAmountOfProduct(BO.Cart cart, int productId, int newAmount);

    /// <summary>
    /// place an order
    /// </summary>
    /// <param name="cart">the customer's cart</param>
    /// <exception cref="not Valid Amount"></exception>
    /// <exception cref="product does not exist"></exception>
    BO.Order CommitOrder(BO.Cart cart);
    void CheckFormat(BO.Cart cart);
}
