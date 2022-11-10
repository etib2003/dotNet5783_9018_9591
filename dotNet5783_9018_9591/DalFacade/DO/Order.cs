namespace DO;
/// <summary>
/// a struct for the orders that contains all of the order's parameters
/// </summary>
public struct Order
{
    public int seqNum { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAdress { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime ShipDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public override string ToString() => $@"
        Customer seqNum={seqNum}: {CustomerName}, 
        Email: {CustomerEmail}
        Adress: {CustomerAdress}
        Order date: {OrderDate}
        Ship date: {ShipDate}    	
        Delivery date: {DeliveryDate} 
";
}
 