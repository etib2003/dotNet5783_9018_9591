using DO;

namespace BO;

public class OrderForList
{
    /// <summary>
    /// order's unique id
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// the customer name
    /// </summary>
    public string CustomerName { get; set; }

    /// <summary>
    /// order's status date
    /// </summary>
    public BO.OrderStatus Status { get; set; }

    public int AmountOfItems { get; set; }

    public double TotalPrice { get; set; }

    public override string ToString() => $@"
            Customer ID= {ID}: {CustomerName}, 
            Status: {Status}
            Amount of items: {AmountOfItems}
            Total price: {TotalPrice}
    ";
}
