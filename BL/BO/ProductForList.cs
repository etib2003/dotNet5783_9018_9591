
namespace BO;

public class ProductForList
{
    public int ID { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public BO.Category Category { get; set; }

    /// <summary>
    /// Product's color
    /// </summary>
    public BO.Color Color { get; set; }

    public override string ToString() => $@"
        Product barcode: {ID}, {Name}
        Category: {Category}
        Color: {Color}
     	Price: {Price}";
}
