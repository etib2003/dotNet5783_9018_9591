using BO;
using DalApi;
using DO;
using OtherFunctions;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

internal class Cart : BlApi.ICart
{
    private DalApi.IDal _dal = new Dal.DalList();
    /// <summary>
    /// Add a product to the cart
    /// </summary>
    /// <param name="cart">the customer's cart</param>
    /// <param name="productId">the barcode of the product you want to add to the cart</param>
    /// <returns></returns>
    /// <exception cref="BO.BoDoesNoExistException">product does not exist</exception>

    public BO.Cart AddProductToCart(BO.Cart cart, int productId)
    {
        try
        {
            DO.Product product = _dal.Product.RequestById(productId); //get the right product using its id

            BO.OrderItem orderItem = (from OrderItem in cart.Items
                                      where OrderItem.ProductID == productId
                                      select OrderItem).First();

            if (product.InStock > 0) // the stock is not empty
            {
                if (orderItem is null)
                    cart.Items.Add(new BO.OrderItem { Name = product.Name, ProductID = product.ID, Amount = 1, Price = product.Price, TotalPrice = product.Price });
                else //add another one of the product
                {
                    orderItem.Amount++;
                    orderItem.TotalPrice += orderItem.Price;//update the total price
                }
            }
            else
                product.InStock.negativeNumber(); //exception


            cart.TotalPrice += orderItem.Price; // update the total price anyway

            return cart;
        }
        catch (DalApi.DalDoesNoExistException ex) //catches the exception from the data layer
        {

            throw new BO.BoDoesNoExistException("product does not exist", ex);
        }
        
    }
    /// <summary>
    /// Update the amount of the product in the cart
    /// </summary>
    /// <param name="cart">the customer's cart</param>
    /// <param name="productId">the product's barcode</param>
    /// <param name="newAmount">the new amount of the product that you want</param>
    /// <returns></returns>
    /// <exception cref="Exception">the orderItem does not exist</exception>
    /// <exception cref="BO.BoDoesNoExistException">product does not exist</exception>
    public BO.Cart UpdateAmountOfProduct(BO.Cart cart, int productId, int newAmount)
    {
        try
        {
            DO.Product doProduct = _dal.Product.RequestById(productId);//get the right product using its id

            BO.OrderItem orderItem = (from OrderItem in cart.Items
                                      where OrderItem.ProductID == productId
                                      select OrderItem).First() ?? throw new BO.BoDoesNoExistException("the orderItem does not exist");

            if (orderItem.Amount > newAmount) //in case the new amount is smaller- remove products from the cart
            {
                int amountToRemove = orderItem.Amount - newAmount;
                orderItem.Amount -= amountToRemove;
                orderItem.TotalPrice -= orderItem.Price * amountToRemove;
                cart.TotalPrice -= orderItem.Price * amountToRemove;
            }
            else if (orderItem.Amount < newAmount)//in case the new amount is bigger- add products to the cart
            {
                if (doProduct.InStock > 0) // the products requested are in stock
                {
                    int amountToAdd = newAmount - orderItem.Amount;
                    orderItem.Amount += amountToAdd;
                    orderItem.TotalPrice += orderItem.Price * amountToAdd;
                    cart.TotalPrice += orderItem.Price * amountToAdd;
                }
                else
                    doProduct.InStock.negativeNumber();//exception
            }
            else if (newAmount == 0) //remove the product's order from the cart
            {
                cart.TotalPrice -= orderItem.TotalPrice;
                cart.Items.Remove(orderItem);
            }
            return cart;

        }
        catch (DalApi.DalDoesNoExistException ex) //catches the exception from the data layer
        {

            throw new BO.BoDoesNoExistException("product does not exist", ex);
        }
    }
    /// <summary>
    /// place an order
    /// </summary>
    /// <param name="cart">the customer's cart</param>
    /// <exception cref="NotValidAmountException">not Valid Amount</exception>
    /// <exception cref="BO.BoDoesNoExistException">product does not exist</exception>
    public void CommitOrder(BO.Cart cart)//place the order
    {
        try
        {
            //exceptions
            cart.CustomerEmail.notValidEmail();
            cart.CustomerName.wrongLengthName();
            cart.CustomerAdress.wrongLengthName();     
                
            foreach (BO.OrderItem orderItem in cart.Items) 
            {

                if (orderItem.Amount < 1)
                    orderItem.Amount.negativeNumber();//exception

                DO.Product doProduct = _dal.Product.RequestById(orderItem.ProductID);//get the right product using its id

                if (orderItem.Amount > doProduct.InStock)
                    throw new NotValidAmountException("not Valid Amount");//exception

            }

            //if everything is ok
            int orderId = _dal.Order.Create(new DO.Order()  //create a new order
            {
                CustomerName = cart.CustomerName,
                CustomerEmail = cart.CustomerEmail,
                CustomerAdress = cart.CustomerAdress,
                OrderDate = DateTime.Now,
                ShipDate = null,
                DeliveryDate = null
            });

            (from orderItem in cart.Items //go over the orders in the cart
             select new DO.OrderItem()
             {
                 OrderID = orderId,
                 ProductID = orderItem.ProductID,
                 Amount = orderItem.Amount,
                 Price = orderItem.Price
             }).ToList().ForEach(orderItem =>
             {
                 _dal.OrderItem.Create(orderItem);
                 DO.Product product = _dal.Product.RequestById(orderItem.ProductID);//get the right product using its id
                 product.InStock -= orderItem.Amount; //delete from the stock
                 _dal.Product.Update(product);
             });
        }
        catch (DalApi.DalDoesNoExistException ex)//catches the exception from the data layer
        {

            throw new BO.BoDoesNoExistException("product does not exist", ex);
        }

    }
}

