using BO;
using OtherFunctions;
using System.Runtime.Serialization;

internal class Product : BlApi.IProduct
{
    private DalApi.IDal _dal = new Dal.DalList();

    public IEnumerable<BO.ProductForList> GetListProductForManagerAndCatalog()
    {
        IEnumerable<DO.Product> doProductList = _dal.Product.RequestAll();//gets the products from the data layer
        IEnumerable<BO.ProductForList> productForLists = from product in doProductList
                                                         select new BO.ProductForList
                                                         {
                                                             //Initializes the data for each product
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
            //exceptions
            productId.negativeNumber();
            productId.wrongLengthNumber(6);

            DO.Product doProduct = _dal.Product.RequestById(productId);//gets a product using its id

            BO.Product boProduct = new BO.Product//create a new logical layer product
            {
                //Initializes the data of the product
                ID = doProduct.ID,
                Name = doProduct.Name,
                Price = doProduct.Price,
                Category = (BO.Category)doProduct.Category,
                Color = (BO.Color)doProduct.Color,
                InStock = doProduct.InStock
            };

            return boProduct;
        }
        catch (DalApi.DalDoesNoExistException ex) //catches the exception from the data layer
        {
            throw new BO.BoDoesNoExistException("Data exception:", ex);
        }
    }


    public BO.ProductItem GetProductDetailsForCustomer(int productId, BO.Cart cart)
    {
        try
        {
            //exceptions
            productId.negativeNumber();
            productId.wrongLengthNumber(6);

            DO.Product doProduct = _dal.Product.RequestById(productId);//gets the right product using its id

            return new BO.ProductItem
            {
                //Initializes the data  
                ID = doProduct.ID,
                Name = doProduct.Name,
                Price = doProduct.Price,
                Category = (BO.Category)doProduct.Category,
                Color = (BO.Color)doProduct.Color,
                InStock = doProduct.InStock > 0,
                //gets the amount of the product
                Amount = (from orderItem in cart.Items
                          where orderItem.ProductID == productId
                          select orderItem.Amount).FirstOrDefault(0)
            };
        }
        catch (DalApi.DalDoesNoExistException ex)//catches the exception from the data layer
        {
            throw new BO.BoDoesNoExistException("Data exception:", ex);
        }
    }


    public int AddProduct(BO.Product product)
    {

        //exceptions
        product.ID.negativeNumber();
        product.ID.wrongLengthNumber(6);
        product.Name.notValidName();
        product.Price.negativeDoubleNumber();
        product.InStock.negativeNumber();

        DO.Product doProduct = new DO.Product//create a new data layer product
        {
            //Initializes the data of the product
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
        //exceptions
        product.ID.negativeNumber();
        product.ID.wrongLengthNumber(6);
        product.Name.notValidName();
        product.Price.negativeDoubleNumber();
        product.InStock.negativeNumber();

        DO.Product doProduct = new DO.Product//create a new data layer product object
        {
            //Initializes the data of the product
            ID = product.ID,
            Name = product.Name,
            Price = product.Price,
            Category = (DO.Category)product.Category,
            Color = (DO.Color)product.Color,
            InStock = product.InStock
        };
        _dal.Product.Update(doProduct);//send the product to the data layer function that updates it
    }

    public void DeleteProduct(int productId)
    {
        //find the product to delete
        IEnumerable<DO.OrderItem> doOrderItemList = _dal.OrderItem.RequestAll();//gets the products from the data layer
        IEnumerable<DO.OrderItem> orderItemList = from orderItem in doOrderItemList
                                                  where orderItem.ProductID == productId
                                                  select orderItem;
        if (!orderItemList.Any())
            _dal.Product.Delete(productId);
        else
            throw new NotValidDeleteException("product Already In Order Prosses");//exception
    }
}

