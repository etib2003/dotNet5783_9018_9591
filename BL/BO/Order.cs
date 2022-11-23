using DO;

namespace BO;

public class Order
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
    /// the customer email
    /// </summary>
    public string CustomerEmail { get; set; }

    /// <summary>
    /// the customer adress
    /// </summary>
    public string CustomerAdress { get; set; }

    /// <summary>
    /// order's status date
    /// </summary>
    public BO.OrderStatus Status { get; set; } //לראות מה קורה פה

    /// <summary>
    /// order's order date
    /// </summary>
    public DateTime OrderDate { get; set; }

    ///// <summary>
    ///// order's payment date
    ///// </summary>
    //public DateTime PaymentDate { get; set; }

    /// <summary>
    /// order's ship date
    /// </summary>
    public DateTime ShipDate { get; set; }

    /// <summary>
    /// order's delivery date
    /// </summary>
    public DateTime DeliveryDate { get; set; }

    public List <BO.OrderItem> OrderItems { get; set; }

    public double TotalPrice { get; set; }

    /// <summary>
    /// the order's print method
    /// </summary>
    /// <returns>the way the order is printed</returns>
    public override string ToString() {
            return $@"
            Customer ID= {ID}: {CustomerName}, 
            Email: {CustomerEmail}
            Adress: {CustomerAdress}
            Status: {Status}
            Order date: {OrderDate}
            Ship date: {ShipDate}    	
            Delivery date: {DeliveryDate} 
            Items: {string.Join("", "", OrderItems)}
            Total price: {TotalPrice}";
        //    Items :";
        //    foreach(var item in Items)
        //    { s = s + item+ "       \n"; };
        //    return s;
        ////string.Join(", ", Items)}
    }
}
