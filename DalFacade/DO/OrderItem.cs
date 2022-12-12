using OtherFunctionDal;

namespace DO;

/// <summary>
/// a struct that links between the order and the product
/// </summary>
public struct OrderItem
{
    /// <summary>
    /// orderItem's unique id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// orderItem's order id
    /// </summary>
    public int OrderID { get; set; }

    /// <summary>
    /// orderItem's product barcode
    /// </summary>
    public int ProductID { get; set; }

    /// <summary>
    /// orderItem's price
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// orderItem's amount to buy
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// the orderItem's print method
    /// </summary>
    /// <returns>the way the orderItem is printed</returns>
    public override string ToString()
    {
        return this.ToStringProperty();
    }

}
