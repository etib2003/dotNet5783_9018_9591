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

    public BO.Cart AddProductToCart(BO.Cart cart, int productId)
    {
        try
        {
            DO.Product product = _dal.Product.RequestById(productId);

            BO.OrderItem orderItem = (from OrderItem in cart.Items
                                      where OrderItem.ProductID == productId
                                      select OrderItem).First();

            if (product.InStock > 0)
            {
                if (orderItem is null)
                    cart.Items.Add(new BO.OrderItem { Name = product.Name, ProductID = product.ID, Amount = 1, Price = product.Price, TotalPrice = product.Price });
                else
                {
                    orderItem.Amount++;
                    orderItem.TotalPrice += orderItem.Price;
                }
            }
            else
                product.InStock.negativeNumber();


            cart.TotalPrice += orderItem.Price;

            return cart;
        }
        catch (DalApi.DalDoesNoExistException ex)
        {

            throw new BO.BoDoesNoExistException("product does not exist", ex);
        }
        
    }

    public BO.Cart UpdateAmountOfProduct(BO.Cart cart, int productId, int newAmount)  
    {
        try
        {
            DO.Product doProduct = _dal.Product.RequestById(productId);

            BO.OrderItem orderItem = (from OrderItem in cart.Items
                                      where OrderItem.ProductID == productId
                                      select OrderItem).First() ?? throw new Exception();

            if (orderItem.Amount > newAmount)
            {
                int amountToRemove = orderItem.Amount - newAmount;
                orderItem.Amount -= amountToRemove;
                orderItem.TotalPrice -= orderItem.Price * amountToRemove;
                cart.TotalPrice -= orderItem.Price * amountToRemove;
            }
            else if (orderItem.Amount < newAmount)
            {
                if (doProduct.InStock > 0)
                {
                    int amountToAdd = newAmount - orderItem.Amount;
                    orderItem.Amount += amountToAdd;
                    orderItem.TotalPrice += orderItem.Price * amountToAdd;
                    cart.TotalPrice += orderItem.Price * amountToAdd;
                }
                else
                    doProduct.InStock.negativeNumber();
            }
            else if (newAmount == 0)
            {
                cart.TotalPrice -= orderItem.TotalPrice;
                cart.Items.Remove(orderItem);
            }
            return cart;

        }
        catch (DalApi.DalDoesNoExistException ex)
        {

            throw new BO.BoDoesNoExistException("product does not exist", ex);
        }
    }

    public void CommitOrder(BO.Cart cart)
    {
        try
        {
            
            cart.CustomerEmail.notValidEmail();
            cart.CustomerName.wrongLengthName();
            cart.CustomerAdress.wrongLengthName();     
                
            foreach (BO.OrderItem orderItem in cart.Items) //לזרוק חריגה אם העגלה ריקה
            {

                if (orderItem.Amount < 1)
                    orderItem.Amount.negativeNumber();

                DO.Product doProduct = _dal.Product.RequestById(orderItem.ProductID);
                
                if (orderItem.Amount > doProduct.InStock)//לבדוק
                    throw new NotValidAmountException("not Valid Amount");

            }

            //if everything is ok
            int orderId = _dal.Order.Create(new DO.Order()
            {
                CustomerName = cart.CustomerName,
                CustomerEmail = cart.CustomerEmail,
                CustomerAdress = cart.CustomerAdress,
                OrderDate = DateTime.Now,
                ShipDate = null,
                DeliveryDate = null
            });

            (from orderItem in cart.Items
             select new DO.OrderItem()
             {
                 OrderID = orderId,
                 ProductID = orderItem.ProductID,
                 Amount = orderItem.Amount,
                 Price = orderItem.Price
             }).ToList().ForEach(orderItem =>
             {
                 _dal.OrderItem.Create(orderItem);
                 DO.Product product = _dal.Product.RequestById(orderItem.ProductID);
                 product.InStock -= orderItem.Amount;
                 _dal.Product.Update(product);
             });
        }
        catch (DalApi.DalDoesNoExistException ex)
        {

            throw new BO.BoDoesNoExistException("product does not exist", ex);
        }

    }
}

