using static DO.Enums;
namespace DO;
/// <summary>
/// a struct for products
/// </summary>
public struct Product
{
    /// <summary>
    /// product's unique barcode
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// product's name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// product's price
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// product's Category
    /// </summary>
    public category Category { get; set; }

    /// <summary>
    /// product's amount in stock
    /// </summary>
    public int InStock { get; set; }

    /// <summary>
    /// Product's color
    /// </summary>
    public color Color { get; set; }

    /// <summary>
    /// the product's print method
    /// </summary>
    /// <returns>the way the product is printed</returns>
    public override string ToString() => $@"
        Product barcode: {ID}, {Name}
        Category: {Category}
        Color: {Color}
    	Price: {Price}
    	Amount in stock: {InStock}";

}
