
namespace BO;

public class ProductForList
{
    /// <summary>
    ///  product's unique id
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// the product's name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// price of product
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// category of product
    /// </summary>
    public BO.Category Category { get; set; }

    /// <summary>
    /// Product's color
    /// </summary>
    public BO.Color Color { get; set; }

    /// <summary>
    /// the list of products print method
    /// </summary>
    /// <returns>the way the product's list data is printed</returns>
    public override string ToString() => $@"
        Product barcode: {ID}, {Name}
        Category: {Category}
        Color: {Color}
     	Price: {Price}";
}
