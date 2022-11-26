using BO;
using OtherFunctions;
using System.Runtime.Serialization;

internal class Product : BlApi.IProduct
{
    private DalApi.IDal _dal = new Dal.DalList();

    public IEnumerable<BO.ProductForList> GetListProductForManagerAndCatalog()
    {
        IEnumerable<DO.Product> doProductList = _dal.Product.RequestAll();
        IEnumerable<BO.ProductForList> productForLists = from product in doProductList
                                                     select new BO.ProductForList
                                                     {
                                                         ID = product.ID,
                                                         Name = product.Name,
                                                         Price = product.Price,
                                                         Category = (BO.Category)product.Category,
                                                         Color = (BO.Color)product.Color
                                                     };
        return productForLists;
    }

    public BO.Product GetProductDetailsForManager(int productId)
    {
        try
        {
            productId.negativeNumber();

            productId.wrongLengthNumber(6);
            
            DO.Product doProduct = _dal.Product.RequestById(productId);

            BO.Product boProduct = new BO.Product
            {
                ID = doProduct.ID,
                Name = doProduct.Name,
                Price = doProduct.Price,
                Category = (BO.Category)doProduct.Category,
                Color = (BO.Color)doProduct.Color,
                InStock = doProduct.InStock
            };

            return boProduct;
        }
        catch (DalApi.DalDoesNoExistException ex) 
        {
            throw new BO.BoDoesNoExistException("the product does not exist", ex); 
           
        }


        //catch(BO.BoDoesNoExistException ex)//main
        //{
        //    Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
        //}
    }

    public BO.ProductItem GetProductDetailsForCustomer(int productId, BO.Cart cart)
    {
        try
        {
            productId.negativeNumber();

            productId.wrongLengthNumber(6);

            DO.Product doProduct = _dal.Product.RequestById(productId);
     
            return new BO.ProductItem
            {
                ID = doProduct.ID,
                Name = doProduct.Name,
                Price = doProduct.Price,
                Category = (BO.Category)doProduct.Category,
                Color = (BO.Color)doProduct.Color,
                InStock = doProduct.InStock > 0,
                Amount = (from orderItem in cart.Items
                          where orderItem.ProductID == productId
                          select orderItem.Amount).FirstOrDefault(0)
            };
        }
        catch (DalApi.DalDoesNoExistException ex)
        {
            throw new BO.BoDoesNoExistException("the product does not exist", ex); 

        }
    
    }


    public int AddProduct(BO.Product product)
    {

        product.ID.negativeNumber();
        product.ID.wrongLengthNumber(6);
        product.Name.wrongLengthName();
        product.Price.negativeDoubleNumber();
        product.InStock.negativeNumber();


        DO.Product doProduct = new DO.Product
        {
            ID = product.ID,
            Name = product.Name,
            Price = product.Price,
            Category = (DO.Category)product.Category,
            Color = (DO.Color)product.Color,
            InStock = product.InStock
        };
        return _dal.Product.Create(doProduct);
    }

    public void UpdateProduct(BO.Product product)
    {

        product.ID.negativeNumber();
        product.ID.wrongLengthNumber(6);
        product.Name.wrongLengthName();
        product.Price.negativeDoubleNumber();
        product.InStock.negativeNumber();
       
        DO.Product doProduct = new DO.Product
        {
            ID = product.ID,
            Name = product.Name,
            Price = product.Price,
            Category = (DO.Category)product.Category,
            Color = (DO.Color)product.Color,
            InStock = product.InStock
        };
        _dal.Product.Update(doProduct);
    }
    public void DeleteProduct(int productId)
    {

        IEnumerable<DO.OrderItem> orderItemList = from orderItem in _dal.OrderItem.RequestAll()
                                                  where orderItem.ProductID == productId
                                                  select orderItem;
        if (!orderItemList.Any())
            _dal.Product.Delete(productId);

        else
            throw new productAlreadyInOrderProssesException("product Already In Order Prosses");
        

         
    }
}

