using BO;
using OtherFunctions;
using System.Runtime.Serialization;

internal class Product : BlApi.IProduct
{
    private DalApi.IDal? _dal =DalApi.Factory.Get();

    public IEnumerable<BO.ProductForList> GetListProductForManagerAndCatalog()
    {
        IEnumerable<DO.Product?> doProductList = _dal.Product.RequestAll();//gets the products from the data layer
        //return DataSource._products.Where(product => cond is null ? true : cond!(product));

        IEnumerable<BO.ProductForList> productForLists = from product in doProductList
                                                         select new BO.ProductForList
                                                         {
                                                             //Initializes the data for each product
                                                             Id = product?.Id??0,
                                                             Name = product?.Name,
                                                             Price = product?.Price??0,
                                                             Category = (BO.Category)product?.Category!,
                                                         };
        return productForLists;
    }

    public IEnumerable<BO.ProductForList> GetListProductForManagerAndCatalogByCond(Func<ProductForList?, bool>? cond)
    {
        return GetListProductForManagerAndCatalog().Where(cond!);
    }

    public BO.Product GetProductDetailsForManager(int productId)
    {
        try
        {
            //exceptions
            productId.negativeNumber();
            productId.wrongLengthNumber(6);

            DO.Product doProduct = _dal.Product.GetById(productId);//gets a product using its id

            BO.Product boProduct = new BO.Product//create a new logical layer product
            {
                //Initializes the data of the product
                Id = doProduct.Id,
                Name = doProduct.Name,
                Price = doProduct.Price,
                Category = (BO.Category)doProduct.Category,
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

            DO.Product doProduct = _dal.Product.GetById(productId);//gets the right product using its id

            return new BO.ProductItem
            {
                //Initializes the data  
                Id = doProduct.Id,
                Name = doProduct.Name,
                Price = doProduct.Price,
                Category = (BO.Category)doProduct.Category,
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
        try
        {
            //exceptions
            product.Id.negativeNumber();
            product.Id.wrongLengthNumber(6);
            product.Name!.notValidName();
            product.Price.negativeDoubleNumber();
            product.InStock.negativeNumber();

            DO.Product doProduct = new DO.Product//create a new data layer product
            {
                //Initializes the data of the product
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Category = (DO.Category)product.Category!,
                InStock = product.InStock
            };
            return _dal.Product.Create(doProduct);
        }
        catch (DalApi.DalAlreadyExistsException ex)//catches the exception from the data layer
        {
            throw new BO.BoAlreadyExistsException("Data exception:", ex);
        }
    }

    public void UpdateProduct(BO.Product product)
    {
        try
        {
            //exceptions
            product.Id.negativeNumber();
            product.Id.wrongLengthNumber(6);
            product.Name!.notValidName();
            product.Price.negativeDoubleNumber();
            product.InStock.negativeNumber();

            DO.Product doProduct = new DO.Product//create a new data layer product object
            {
                //Initializes the data of the product
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Category = (DO.Category)product.Category,
                InStock = product.InStock
            };
            _dal?.Product.Update(doProduct);//send the product to the data layer function that updates it
        }
        catch (DalApi.DalDoesNoExistException ex)//catches the exception from the data layer
        {
            throw new BO.BoDoesNoExistException("Data exception:", ex);
        }
    }

    public void DeleteProduct(int productId)
    {
        //find the product to delete
        IEnumerable<DO.OrderItem?> doOrderItemList = _dal?.OrderItem.RequestAll(orderItem => orderItem?.ProductID == productId);//gets the products from the data layer

        if (!doOrderItemList.Any())
            _dal.Product.Delete(productId);
        else
            throw new NotValidDeleteException("product Already In Order Prosses");//exception
    }
}

