
namespace BO;

public class OrderItem
{
    /// <summary>
    /// orderItem's unique id
    /// </summary>
    public int  Id { get; set; }
     
    /// <summary>
    /// product's name
    /// </summary>
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
            Identification number= {Id}, {Name}
            Product Id: {ProductID}
    	    Price: {Price}
    	    Amount: {Amount}
            Total price: {TotalPrice}";

}
