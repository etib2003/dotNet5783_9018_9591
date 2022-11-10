using DO;
namespace Dal;

public class DalProduct
{

    //CRUD for Products
    
    public int Create(Product product)
    {
        if (DataSource.Products.Exists(x => x.ID == product.ID))
            throw new Exception("cannot create a product,that is already exists");

        DataSource.Products.Add(product);
        return product.ID;  
    }

    public List<Product> RequestAll()
    {
        List<Product> listToReturn = new List<Product>();
        for (int i = 0; i < DataSource.Products.Count; i++)
            listToReturn.Add(DataSource.Products[i]);
        return listToReturn;
    }

    public Product RequestById(int id)
    {
        if (!DataSource.Products.Exists(x => x.ID == id))
            throw new Exception("the product is not exist");

        return DataSource.Products.Find(x => x.ID == id);
    }

    public void Update(Product product)
    {
        //if product does not exist throw exception 
        if (!DataSource.Products.Exists(x => x.ID == product.ID))
            throw new Exception("cannot update a product, that is not exists");
        Product PdctToRemove = DataSource.Products.Find(x => x.ID == product.ID); //מחזיר את האובייקט
        DataSource.Products.Remove(PdctToRemove);//מסיר את האובייקט
        DataSource.Products.Add(product);//שם את המעודכן שמקום של האינדקס
    }
    public void Delete(int id)
    {
        //if product does not exist throw exception 
        if (!DataSource.Products.Exists(x => x.ID == id))
            throw new Exception("cannot delete a product,that is not exists");
        Product PdctToRemove = DataSource.Products.Find(x => x.ID == id);
        DataSource.Products.Remove(PdctToRemove);
    }

}