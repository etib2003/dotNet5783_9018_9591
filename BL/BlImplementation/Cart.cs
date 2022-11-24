using BO;
using DalApi;
using DO;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.RegularExpressions;

internal class Cart : BlApi.ICart
{
    private DalApi.IDal _dal = new Dal.DalList();

    public BO.Cart AddProductToCart(BO.Cart cart, int productId)
    {
        DO.Product product = _dal.Product.RequestById(productId);

        //איך יודעים אם מוצר קיים בעגלה?
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
            throw new Exception();


        cart.TotalPrice += orderItem.Price;

        return cart;
        //throw new NotImplementedException();//למחוק
    }

    public BO.Cart UpdateAmountOfProduct(BO.Cart cart, int productId, int newAmount) //לעשות
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
                throw new Exception();
        }
        else if (newAmount == 0)
        {
            cart.TotalPrice -= orderItem.TotalPrice;
            cart.Items.Remove(orderItem);
        }
        return cart;

    }

    public void CommitOrder(BO.Cart cart)
    {
        try
        {
            Regex regex = new Regex("^[A-Za-z]+$");

            if (!new EmailAddressAttribute().IsValid(cart.CustomerEmail))
            {

            }

            if (!regex.IsMatch(cart.CustomerName))
            {

            }

            if (cart.CustomerName == "" || cart.CustomerAdress == "" || cart.CustomerEmail == ""
            || cart.CustomerEmail[0] == '@' || cart.CustomerEmail[cart.CustomerEmail.Length - 1] == '@' || !cart.CustomerEmail.Contains('@'))
                throw new Exception("ivalid customer details");
            foreach (BO.OrderItem orderItem in cart.Items)
            {
                if (orderItem.Amount < 1)//זה לא נכון את צריכה לבדוק שזה לא קטן ממספר ההזמנה
                    throw new Exception("  ");

                DO.Product doProduct = _dal.Product.RequestById(orderItem.ProductID);
                //יזרוק חריגה אם איו את המוצר



                if (orderItem.Amount < doProduct.InStock)
                    throw new Exception("  ");

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
        catch (Exception)
        {

            throw;


        }

    }
}