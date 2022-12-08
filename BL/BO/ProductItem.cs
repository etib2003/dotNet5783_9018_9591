

using OtherFunctionDal;
using OtherFunctions;

namespace BO;

public class ProductItem
{
    /// <summary>
    /// product's id
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
    /// product's category
    /// </summary>
    public BO.Category? Category { get; set; }

    /// <summary>
    /// check if the product is in stock
    /// </summary>
    public bool InStock { get; set; }

    /// <summary>
    /// amount of the product
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// the product item print method
    /// </summary>
    /// <returns>the way the product item is printed</returns>
    public override string ToString()
    {
        return this.ToStringProperty();
    }
    //=> $@"
    //    Product barcode: {Id}, {Name}
    //    Price: {Price}
    //    Category: {Category}
    //    Amount: {Amount}   	
    //	In stock? {InStock}";

}
