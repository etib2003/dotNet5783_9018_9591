using BlApi;
using DalApi;

namespace BlImplementation;

internal class Product : BlApi.IProduct
{
    private DalApi.IDal Dal = new Dal.DalList();

    public IEnumerable<Do.ProductForList> GetListProductForManagerAndCatalog()
    {
        IEnumerable<DO.Product> list = Dal.Product.RequestAll();
        IEnumerable<Do.ProductForList> productList = from product in list
                                                     select new Do.ProductForList
                                                     {
                                                         ID = product.ID,
                                                         Name = product.Name,
                                                         Price = product.Price,
                                                         Category = (Do.Category)product.Category,
                                                         Color = (Do.Color)product.Color
                                                     };
        return productList;
    }

    public Do.Product GetProductDetailsForManager(int productId)
    {
        try
        {
            if (productId < 100000) //
                throw new Exception(); //
            DO.Product DOproduct = Dal.Product.RequestById(productId);

            Do.Product BOproduct = new Do.Product
            {
                ID = DOproduct.ID,
                Name = DOproduct.Name,
                Price = DOproduct.Price,
                InStock = DOproduct.InStock,
                Category = (Do.Category)DOproduct.Category,
                Color = (Do.Color)DOproduct.Color
            };
            return BOproduct;
        }
        catch (DalDoesNoExistException e) //
        {
            throw new Exception("Invalid product id", e);//change
        }
    }

    public Do.ProductItem GetProductDetailsForCustomer(int productId, Cart cart)
    {
        //try
        //{
        if (productId < 100000) //
            throw new Exception(); //

        DO.Product DOproduct = Dal.Product.RequestById(productId);

        Do.ProductItem BOproduct = new Do.ProductItem
        {
            ID = DOproduct.ID,
            Name = DOproduct.Name,
            Price = DOproduct.Price,
            Category = (Do.Category)DOproduct.Category,
            Color = (Do.Color)DOproduct.Color,
            Amount = DOproduct.InStock,
            InStock = DOproduct.InStock > 0
        };

        return BOproduct;

        //catch (DalDoesNoExistException e) //
        //{
        //    throw new Exception("Invalid product id", e);//change
        //}
    }


    public int AddProduct(Do.Product p)
    {
        //if (p.ID < 100000 || p.Name.Length < 1 || p.Price <= 0 || p.InStock < 0)
        //   throw new DalAlreadyExistsException("Invalid product data");

        //צריך להוסיף זריקת חריגה אם לא הצליח להוסיף-עקב מזהה זהה וכדומה
        DO.Product DOproduct = new DO.Product
        {
            ID = p.ID,
            Name = p.Name,
            Price = p.Price,
            Category = (DO.Category)p.Category,
            Color = (DO.Color)p.Color,
            InStock = p.InStock
        };
        return Dal.Product.Create(DOproduct);
    }

    public void UpdateProduct(Do.Product p)
    {
        //if (p.ID < 100000 || p.Name.Length < 1 || p.Price <= 0 || p.InStock < 0)
        //    throw new CreateException("Invalid product data");
        //צריך להוסיף זריקת חריגה אם לא הצליח לעדכן-עקב מזהה זהה וכדומה

        DO.Product DOproduct = new DO.Product
        {
            ID = p.ID,
            Name = p.Name,
            Price = p.Price,
            Category = (DO.Category)p.Category,
            Color = (DO.Color)p.Color,
            InStock = p.InStock
        };
        Dal.Product.Update(DOproduct);
    }
    public void DeleteProduct(int productId)
    {
        //חריגה-או ששייך להזמנה או שלא קיים מוצר כזה

        IEnumerable<DO.OrderItem> orderItemList = from orderItem in Dal.OrderItem.RequestAll()
                                                  where orderItem.ProductID == productId
                                                  select orderItem;
        if (!orderItemList.Any())
            Dal.Product.Delete(productId);

        throw new NotImplementedException(); //למחוקקקק
    }
}
