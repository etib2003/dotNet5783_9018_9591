namespace BO;
public class Cart
{
    /// <summary>
    /// the customer's name
    /// </summary>
    public string? CustomerName { get; set; }
    /// <summary>
    /// the customer's email
    /// </summary>
    public string? CustomerEmail { get; set; }
    /// <summary>
    /// the customer's address
    /// </summary>
    public string? CustomerAddress { get; set; }
    /// <summary>
    /// list of order items
    /// </summary>
    public List <BO.OrderItem?>? Items { get; set; }
    /// <summary>
    /// cart's total price
    /// </summary>
    public double TotalPrice { get; set; }

    /// <summary>
    /// the cart's print method
    /// </summary>
    /// <returns>the way the cart's data is printed</returns>
    public override string ToString() {
        return $@"
            Customer Name: {CustomerName}
            Customer Email: {CustomerEmail}
     	    Customer Address: {CustomerAddress}

            Items: {string.Join("\n", Items)}

     	    Cart's Total Price: {TotalPrice}";
    }
}
