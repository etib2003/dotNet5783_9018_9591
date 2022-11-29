using BO;
using DO;
using OtherFunctions;

internal class Cart : BlApi.ICart
{
    private DalApi.IDal _dal = new Dal.DalList();


    public BO.Cart AddProductToCart(BO.Cart cart, int productId)
    {
        try
        {
            DO.Product product = _dal.Product.RequestById(productId); //get the right product using its id
            BO.OrderItem orderItem = new BO.OrderItem();
            if (cart.Items != null)
            {
                orderItem = (from OrderItem in cart.Items
                             where OrderItem.ProductID == productId
                             select OrderItem).FirstOrDefault();
            }

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
                product.InStock.notInStock(); //exception

            cart.TotalPrice += product.Price; // update the total price anyway

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
            DO.Product doProduct = _dal.Product.RequestById(productId);//get the right product using its id

            BO.OrderItem orderItem = (from OrderItem in cart.Items
                                      where OrderItem.ProductID == productId
                                      select OrderItem).First() ?? throw new BO.BoDoesNoExistException("Data exception:");//

            if (newAmount == 0) //remove the product's order from the cart
            {
                cart.TotalPrice -= orderItem.TotalPrice;
                cart.Items.Remove(orderItem);
            }
            else if(orderItem.Amount > newAmount) //in case the new amount is smaller- remove products from the cart
            {
                //int amountToRemove = orderItem.Amount - newAmount;
                orderItem.Amount -= newAmount;
                orderItem.TotalPrice -= orderItem.Price * newAmount;
                cart.TotalPrice -= orderItem.Price * newAmount;
            }
            else if (orderItem.Amount < newAmount)//in case the new amount is bigger- add products to the cart
            {
                if (doProduct.InStock > 0) // the products requested are in stock
                {
                    //int amountToAdd = newAmount - orderItem.Amount;
                    orderItem.Amount += newAmount;
                    orderItem.TotalPrice += orderItem.Price * newAmount;
                    cart.TotalPrice += orderItem.Price * newAmount;
                }
                else
                    doProduct.InStock.negativeNumber();//exception
            }
            
            return cart;

        }
        catch (DalApi.DalDoesNoExistException ex) //catches the exception from the data layer
        {
            throw new BO.BoDoesNoExistException("Data exception:", ex);
        }
    }

    public void CommitOrder(BO.Cart cart)
    {
        try
        {
            //exceptions
            cart.CustomerEmail.notValidEmail();
            cart.CustomerName.notValidName();
            cart.CustomerAddress.notValidName();

            foreach (BO.OrderItem orderItem in cart.Items)
            {
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
                CustomerAdress = cart.CustomerAddress,
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
            throw new BO.BoDoesNoExistException("Data exception:", ex);
        }

    }
}

