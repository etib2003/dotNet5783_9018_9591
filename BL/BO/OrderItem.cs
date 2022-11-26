
namespace BO;

public class OrderItem
{
    /// <summary>
    /// orderItem's unique id
    /// </summary>
   // public List<int> OrderID = new List<int>();
    public int  OrderID { get; set; }
     
    public string Name{ get; set; }
     
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

    public double TotalPrice { get; set; }
    /// <summary>
    /// the orderItem's print method
    /// </summary>
    /// <returns>the way the orderItem is printed</returns>
    public override string ToString() => $@" 
            Identification number= {OrderID}, {Name}
            Product OrderID: {ProductID}
    	    Price: {Price}
    	    Amount: {Amount}
            Total price: {TotalPrice}";

}
