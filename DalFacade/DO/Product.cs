using OtherFunctionDal;

namespace DO;
/// <summary>
/// a struct for products
/// </summary>
public struct Product
{
    /// <summary>
    /// product's unique barcode
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// product's name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// product's price
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// product's Category
    /// </summary>
    public Category? Category { get; set; }

    /// <summary>
    /// product's amount in stock
    /// </summary>
    public int InStock { get; set; }

    
    /// <summary>
    /// the product's print method
    /// </summary>
    /// <returns>the way the product is printed</returns>
    public override string ToString()
    {
        return this.ToStringProperty();
    }
    //=> $@"
    //    Product barcode: {Id}, {Name}
    //    Category: {Category}
    //	Price: {Price}
    //	Amount in stock: {InStock}";

}
