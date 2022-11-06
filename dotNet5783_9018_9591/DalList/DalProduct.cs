using DO;

namespace Dal;

public class DalProduct
{

    //public void Create(Product p)
    //{
    //}

    //public Product[] Request()
    //{

    //}

    public void Add(Product product)
    {
        if (!DataSource.Products.Exists(x => x.ID == product.ID))
            DataSource.Products.Add(product);
    }

    internal struct Config
    { 

    }

}