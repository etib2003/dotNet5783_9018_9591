

using OtherFunctionDal;
using OtherFunctions;

namespace BO;

public class ProductItem
{
    /// <summary>
    /// product's id
    /// </summary>
    public int ID { get; set; }

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
    /// Product's color
    /// </summary>
    public BO.Color? Color { get; set; }

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
    //    Product barcode: {ID}, {Name}
    //    Price: {Price}
    //    Category: {Category}
    //    Color: {Color}
    //    Amount: {Amount}   	
    //	In stock? {InStock}";

}
