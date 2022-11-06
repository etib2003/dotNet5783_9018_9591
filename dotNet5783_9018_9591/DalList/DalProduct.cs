using DO;
namespace Dal;

public class DalProduct
{

    //CRUD for Products

    public int Create(Product product)
    {
        if (!DataSource.Products.Exists(x => x.ID == product.ID))
        {
            DataSource.Products.Add(product);
            return product.ID;
        }
        else
            throw new Exception("cannot create a product,that is already exists");
    }

    public List<Product> RequestAll()
    {
        List<Product> listToReturn = DataSource.Products;
        return listToReturn;
    }

    public Product RequestById(int id)
    {
        if (!DataSource.Products.Exists(x => x.ID == id))
            throw new Exception("the product is not exist");

        return DataSource.Products.Find(i => i.ID == id);
    }

    public void Update(Product product)
    {
        //if product does not exist throw exception 
        if (!DataSource.Products.Exists(i => i.ID == product.ID))
            throw new Exception("cannot update a product, that is not exists");
        Product PdctToRemove = DataSource.Products.Find(i => i.ID == product.ID); //מחזיר את האובייקט
        int index = DataSource.Products.IndexOf(PdctToRemove);//מחזיר אינדקס לאובייקט ברשימה
        DataSource.Products.Remove(PdctToRemove);//מסיר את האובייקט
        DataSource.Products.Insert(index, product);//שם את המעודכן שמקום של האינדקס
    }
    public void Delete(int id)
    {
        //if product does not exist throw exception 
        if (!DataSource.Products.Exists(i => i.ID == id))
            throw new Exception("cannot delete a product,that is not exists");
        Product PdctToRemove = DataSource.Products.Find(i => i.ID == id);
        DataSource.Products.Remove(PdctToRemove);
    }

}