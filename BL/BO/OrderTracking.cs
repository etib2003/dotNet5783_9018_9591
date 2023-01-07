using OtherFunctionDal;
using OtherFunctions;

namespace BO;

public class OrderTracking
{
    /// <summary>
    /// order tracking id in order to track your order
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// the order status
    /// </summary>
    public BO.OrderStatus? Status { get; set; }

    /// <summary>
    /// list of pairs
    /// </summary>
    public List<Tuple<DateTime?, string>?>? OrderProgress { get; set; }

    /// <summary>
    /// the tracking order's print method
    /// </summary>
    /// <returns>the way the tracking order is printed</returns>
    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
