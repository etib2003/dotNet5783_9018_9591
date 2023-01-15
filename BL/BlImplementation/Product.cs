using BO;
using OtherFunctions;
using System.Runtime.Serialization;

internal class Product : BlApi.IProduct
{
    private DO.IDal? dal = DO.Factory.Get();

    public IEnumerable<BO.ProductForList> GetListProductForManagerAndCatalog(Func<DO.Product?, bool>? cond)
    {
        try
        {
            IEnumerable<DO.Product?> doProductList = dal?.Product.RequestAll(p => cond is null ? true : cond!(p))!;//gets the products from the data layer
            return doProductList.CopyPropToList<DO.Product?, BO.ProductForList>();
        }
        catch (DO.DalDoesNoExistException ex)//catches the exception from the data layer
        {
            throw new BO.BoDoesNoExistException("Data exception:", ex);
        }
    }

    public IEnumerable<BO.ProductItem> GetListProductForCatalogView(BO.Cart cart, IEnumerable<ProductItem> productItems, Func<BO.ProductItem?, bool>? cond)
    {
        try
        {
            IEnumerable<BO.ProductItem> ProductItemList = from productItem in productItems.Where(p => cond is null ? true : cond!(p))
                                                          let id = (int)productItem?.Id!
                                                          select GetProductDetailsForCustomer(id, cart);
            return ProductItemList;
        }
        catch (DO.DalDoesNoExistException ex)//catches the exception from the data layer
        {
            throw new BO.BoDoesNoExistException("Data exception:", ex);
        }
    }

    public IEnumerable<BO.ProductItem> GetListProductForCatalog(BO.Cart cart, Func<DO.Product?, bool>? cond)
    {
        try
        {
            IEnumerable<DO.Product?> doProductList = dal?.Product.RequestAll()!;//gets the products from the data layer
            IEnumerable<BO.ProductItem> doProductItemList = from product in doProductList.Where(p => cond is null ? true : cond!(p))
                                                            let id = (int)product?.Id!
                                                            select GetProductDetailsForCustomer(id, cart);
            return doProductItemList;
        }
        catch (DO.DalDoesNoExistException ex)//catches the exception from the data layer
        {
            throw new BO.BoDoesNoExistException("Data exception:", ex);
        }
    }

    public BO.Product GetProductDetailsForManager(int productId)
    {
        try
        {
            //exceptions
            productId.negativeNumber();
            productId.wrongLengthNumber(6);

            DO.Product? doProduct = dal?.Product.GetById(productId);//gets a product using it's id
            BO.Product boProduct = doProduct.CopyPropTo(new BO.Product());//create a new logical layer product
            boProduct.Category = (Category?)(doProduct?.Category);
            return boProduct;
        }
        catch (DO.DalDoesNoExistException ex) //catches the exception from the data layer
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

            DO.Product doProduct = dal?.Product.GetById(productId) ?? default;//gets the right product using its id

            BO.ProductItem boProductItem = doProduct.CopyPropTo(new BO.ProductItem());
            boProductItem.Category = (Category?)doProduct.Category;
            boProductItem.InStock = doProduct.InStock > 0;
            boProductItem.Amount = (from orderItem in cart.Items
                                    where orderItem.ProductID == productId
                                    select orderItem.Amount).FirstOrDefault(0);
            return boProductItem;

        }
        catch (DO.DalDoesNoExistException ex)//catches the exception from the data layer
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

            DO.Product doProduct = product.CopyPropToStruct(new DO.Product());//create a new logical layer product
            doProduct.Category = (DO.Category?)product.Category;
            return dal?.Product.Create(doProduct) ?? default;
        }
        catch (DO.DalAlreadyExistsException ex)//catches the exception from the data layer
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

            DO.Product doProduct = product.CopyPropToStruct(new DO.Product());//create a new logical layer product          

            dal?.Product.Update(doProduct);//send the product to the data layer function that updates it
        }
        catch (DO.DalDoesNoExistException ex)//catches the exception from the data layer
        {
            throw new BO.BoDoesNoExistException("Data exception:", ex);
        }
    }

    public void DeleteProduct(int productId)
    {
        //find the product to delete
        IEnumerable<DO.OrderItem?> doOrderItemList = dal?.OrderItem.RequestAll(orderItem => orderItem?.ProductID == productId);//gets the products from the data layer

        if (!doOrderItemList.Any())
            dal?.Product.Delete(productId);
        else
            throw new BO.NotValidDeleteException("product Already In Order Prosses");//exception
    }

    public IEnumerable<ProductForList> GetProductForListByCond(IEnumerable<ProductForList> productForLists, Func<BO.ProductForList, bool>? cond)
    {
        IEnumerable<ProductForList> ProductForList = from productForList in productForLists.Where(p => cond is null ? true : cond!(p))
                                                     select productForList;
        return ProductForList;
    }

    public ProductForList GetProductForList(int productId)
    {
        try
        {
            return dal?.Product.GetById(productId).CopyPropTo(new ProductForList());
        }
        catch (DO.DalDoesNoExistException ex)//catches the exception from the data layer
        {
            throw new BO.BoDoesNoExistException("Data exception:", ex);
        }
    }
}

