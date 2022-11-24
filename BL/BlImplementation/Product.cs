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
            throw new BO.BoDoesNoExistException(string.Empty, ex);//change
           
        }


        //catch(BO.BoDoesNoExistException ex)
        //{
        //    Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
        //}
    }

    public BO.ProductItem GetProductDetailsForCustomer(int productId, BO.Cart cart)
    {
        //try
        //{
        if (productId < 100000) //
            throw new Exception(); //

        DO.Product doProduct = _dal.Product.RequestById(productId);

        BO.ProductItem boProduct = new BO.ProductItem
        {
            ID = doProduct.ID,
            Name = doProduct.Name,
            Price = doProduct.Price,
            Category = (BO.Category)doProduct.Category,
            Color = (BO.Color)doProduct.Color,
            InStock = doProduct.InStock > 0,
            Amount = doProduct.InStock
        };

        return boProduct;

        //catch (DalDoesNoExistException e) //
        //{
        //    throw new Exception("Invalid product id", e);//change
        //}
    }


    public int AddProduct(BO.Product product)
    {
        if (product.ID < 100000 || product.Name.Length < 1 || product.Price <= 0 || product.InStock < 0)//
            throw new Exception("Invalid product data");//

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
        if (product.ID < 100000 || product.Name.Length < 1 || product.Price <= 0 || product.InStock < 0)//
            throw new Exception("Invalid product data");//
        //צריך להוסיף זריקת חריגה אם לא הצליח לעדכן-עקב מזהה זהה וכדומה

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
        //חריגה-או ששייך להזמנה או שלא קיים מוצר כזה

        IEnumerable<DO.OrderItem> orderItemList = from orderItem in _dal.OrderItem.RequestAll()
                                                  where orderItem.ProductID == productId
                                                  select orderItem;
        if (!orderItemList.Any())
            _dal.Product.Delete(productId);

        throw new NotImplementedException(); //למחוקקקק
    }
}

