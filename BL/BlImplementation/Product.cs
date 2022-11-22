using BlApi;
using BO;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Drawing.Diagrams;

namespace BlImplementation;

internal class Product : IProduct
{
    private DalApi.IDal Dal = new Dal.DalList();

    public List<ProductForList> GetListProductForManager()
    {
        IEnumerable<DO.Product> list = Dal.Product.RequestAll();
        List<BO.ProductForList> productList = (List<BO.ProductForList>)
                        (from product in list
                        select new
                        { ID=product.ID ,
                          Name=product.Name,
                          price=product.Price,
                          Category= product.Category });
        return productList;
    }

    public BO.Product GetProductDetailsForManager(int productId)
    {
        DO.Product DOproduct=Dal.Product.RequestById(productId);
        if (DOproduct.ID==0)
            throw new CreateException("Invalid product id"); //לשנות את החריגה

        BO.Product BOproduct = new BO.Product
        {
            ID = DOproduct.ID,
            Name = DOproduct.Name,
            Price = DOproduct.Price,
            InStock = DOproduct.InStock,
            Category = (BO.Category)DOproduct.Category,
            Color = (BO.Color)DOproduct.Color
        };

        return BOproduct;
    }

    public int AddProduct(BO.Product p)
    {
        if (p.ID < 100000 || p.Name.Length < 1 || p.Price <= 0 || p.InStock < 0)
            throw new CreateException("Invalid product data");

        //צריך להוסיף זריקת חריגה אם לא הצליח להוסיף-עקב מזהה זהה וכדומה
        return Dal.Product.Create(new DO.Product
        { ID = p.ID, Name = p.Name, Price = p.Price, Category = (DO.Category)p.Category, Color = (DO.Color)p.Color, InStock = p.InStock });
    }

    public void UpdateProduct(BO.Product p)
    {
        if (p.ID < 100000 || p.Name.Length < 1 || p.Price <= 0 || p.InStock < 0)
            throw new CreateException("Invalid product data");

        //צריך להוסיף זריקת חריגה אם לא הצליח לעדכן-עקב מזהה זהה וכדומה
        Dal.Product.Update(new DO.Product
        { ID = p.ID, Name = p.Name, Price = p.Price, Category = (DO.Category)p.Category, Color = (DO.Color)p.Color, InStock = p.InStock });
    }
    public void DeleteProduct(BO.Product p)
    {
        IEnumerable<DO.Order> list = Dal.Order.RequestAll();
        //להמשיך

        throw new NotImplementedException();
    }

    public List<BO.ProductItem> RequestCatalog()
    {
        IEnumerable<DO.Product> list = Dal.Product.RequestAll();
        List<BO.ProductItem> _ProductItem =
                        (List<BO.ProductItem>)(from productItem in list
                        select new
                        {
                            ID = productItem.ID,
                            Name = productItem.Name,
                            price = productItem.Price,
                            Category = productItem.Category,
                            Color = productItem.Color,
                            Amount = productItem.InStock,
                            InStock = productItem.InStock > 0
                        });
        return _ProductItem;
    }


    public BO.Product GetProductDetailsForCustomer(int productId)
    {
        DO.Product DOproduct=Dal.Product.RequestById(productId);
        if (DOproduct.ID==0)
            throw new CreateException("Invalid product id"); //לשנות את החריגה

        //לא יודעת
        //BO.ProductItem BOproduct = new BO.ProductItem
        //{
        //    ID = DOproduct.ID,
        //    Name = DOproduct.Name,
        //    Price = DOproduct.Price,
        //    InStock = DOproduct.InStock,
        //    Category = (Enums.category)DOproduct.Category,
        //    Color = (Enums.color)DOproduct.Color
        //};

        //return BOproduct;

    }

}
