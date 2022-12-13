using OtherFunctions;
using System.ComponentModel.DataAnnotations;


internal class Cart : BlApi.ICart
{
    private DalApi.IDal? _dal = DalApi.Factory.Get();

    public void CheckFormat(BO.Cart cart)
    {
        cart.CustomerName!.notValidName();
        cart.CustomerEmail!.notValidEmail();
        cart.CustomerAddress!.notValidName();
    }

    public BO.Cart AddProductToCart(BO.Cart cart, int productId)
    {
        try
        {
            DO.Product product = _dal.Product.GetById(productId); //get the right product using its id
            BO.OrderItem? orderItem = (from OrderItem in cart.Items
                                       where OrderItem.ProductID == productId
                                       select OrderItem).FirstOrDefault(); //            if (cart.Items != null)

            if (orderItem is null)
            {
                if (product.InStock > 0) // the stock is not empty
                {
                    cart.Items.Add(new BO.OrderItem { Name = product.Name, ProductID = product.Id, Amount = 1, Price = product.Price, TotalPrice = product.Price });
                    cart.TotalPrice += product.Price;
                }
                else
                    product.InStock.notInStock(); //exception
            }
            else
            {
                if (product.InStock >= orderItem.Amount + 1)
                {
                    orderItem.Amount++;
                    orderItem.TotalPrice += orderItem.Price;//update the total price
                    cart.TotalPrice += product.Price;
                }
                else
                    throw new BO.NotInStockException("Not In Stock");//exception
            }

            return cart;
        }
        catch (DalApi.DalDoesNoExistException ex) //catches the exception from the data layer
        {
            throw new BO.BoDoesNoExistException("Data exception:", ex);
        }
    }

    public BO.Cart UpdateAmountOfProduct(BO.Cart cart, int productId, int newAmount)
    {
        try
        {
            DO.Product doProduct = _dal.Product.GetById(productId);//get the right product using its id

            BO.OrderItem? orderItem = (from OrderItem in cart.Items
                                      where OrderItem.ProductID == productId
                                      select OrderItem).First() ?? throw new BO.NotExistInCartException("Not exist in cart");

            if (newAmount == 0) //remove the product's order from the cart
            {
                cart.TotalPrice -= orderItem.TotalPrice;
                cart.Items.Remove(orderItem);
            }
            else if (orderItem.Amount > newAmount) //in case the new amount is smaller- remove products from the cart
            {
                cart.TotalPrice -= orderItem.Price * (orderItem.Amount - newAmount);
                orderItem.TotalPrice -= orderItem.Price * (orderItem.Amount - newAmount);
                orderItem.Amount = newAmount;
            }
            else if (orderItem.Amount < newAmount)//in case the new amount is bigger- add products to the cart
            {
                if (doProduct.InStock >= newAmount) // the products requested are in stock
                {
                    cart.TotalPrice += orderItem.Price * (newAmount - orderItem.Amount);
                    orderItem.TotalPrice += orderItem.Price * (newAmount - orderItem.Amount);
                    orderItem.Amount = newAmount;
                }
                else
                {
                    throw new BO.NotInStockException("Not In Stock");
                }
            }

            return cart;

        }
        catch (DalApi.DalDoesNoExistException ex) //catches the exception from the data layer
        {
            throw new BO.BoDoesNoExistException("Data exception:", ex);
        }
    }

    public BO.Order CommitOrder(BO.Cart cart)
    {
        try
        {
            //exceptions
            cart.CustomerEmail!.notValidEmail();
            cart.CustomerName!.notValidName();
            cart.CustomerAddress!.notValidName();

            foreach (BO.OrderItem? orderItem in cart.Items!)
            {
                orderItem!.Amount.negativeNumber();//exception

                DO.Product doProduct = _dal.Product.GetById(orderItem.ProductID);//get the right product using its id

                if (orderItem.Amount > doProduct.InStock)
                    throw new BO.NotValidAmountException("not Valid Amount");//exception

            }

            //if everything is ok
            int orderId = _dal.Order.Create(new DO.Order()  //create a new order
            {
                CustomerName = cart.CustomerName,
                CustomerEmail = cart.CustomerEmail,
                CustomerAdress = cart.CustomerAddress,
                OrderDate = DateTime.Now,
                ShipDate = null,
                DeliveryDate = null
            });

            foreach (BO.OrderItem? boOrderItem in cart.Items)
            {
                DO.OrderItem doOrderItem = new DO.OrderItem()
                {
                    OrderID = orderId,
                    ProductID = boOrderItem!.ProductID,
                    Amount = boOrderItem.Amount,
                    Price = boOrderItem.Price
                };
                boOrderItem.Id = _dal.OrderItem.Create(doOrderItem);
                DO.Product product = _dal.Product.GetById(doOrderItem.ProductID);//get the right product using its id
                product.InStock -= doOrderItem.Amount; //delete from the stock
                _dal.Product.Update(product);
            }

            BO.Order boOrder = new BO.Order()
            {
                Id = orderId,
                CustomerName = cart.CustomerName,
                CustomerEmail = cart.CustomerEmail,
                CustomerAdress = cart.CustomerAddress,
                OrderDate = DateTime.Now,
                ShipDate = null,
                DeliveryDate = null,
                TotalPrice = cart.TotalPrice,
                Status = BO.OrderStatus.confirmed,
                OrderItems = cart.Items
            };
            return boOrder;

        }
        catch (DalApi.DalDoesNoExistException ex)//catches the exception from the data layer
        {
            throw new BO.BoDoesNoExistException("Data exception:", ex);
        }

    }
}

