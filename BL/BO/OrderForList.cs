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
    public string? CustomerName { get; set; }

    /// <summary>
    /// order's status date
    /// </summary>
    public BO.OrderStatus? Status { get; set; }

    /// <summary>
    /// the amount of the items in the order's list
    /// </summary>
    public int AmountOfItems { get; set; }

    /// <summary>
    /// total price of the orders in list
    /// </summary>
    public double TotalPrice { get; set; }

    /// <summary>
    /// the list of orders print method
    /// </summary>
    /// <returns>the way the order's list data is printed</returns>
    public override string ToString() => $@"
            Customer Id= {ID}: {CustomerName}, 
            Status: {Status}
            Amount of items: {AmountOfItems}
            Total price: {TotalPrice}";
}
