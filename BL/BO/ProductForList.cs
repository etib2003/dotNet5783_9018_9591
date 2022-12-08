
using OtherFunctionDal;
using OtherFunctions;

namespace BO;

public class ProductForList
{
    /// <summary>
    ///  product's unique id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// the product's name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// price of product
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// category of product
    /// </summary>
    public BO.Category? Category { get; set; }

    /// <summary>
    /// the list of products print method
    /// </summary>
    /// <returns>the way the product's list data is printed</returns>
    public override string ToString()
    {
        return this.ToStringProperty();
    }
    //=> $@"
    //    Product barcode: {Id}, {Name}
    //    Category: {Category}
    // 	Price: {Price}";
}
