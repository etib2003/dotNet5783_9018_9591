namespace DO;

public struct OrderItem
{
    public int seqNum { get; set; }
    public int OrderID { get; set; }
    public int ProductID { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }

    public override string ToString() => $@"
        Identification number={seqNum} 
        Order ID: {OrderID}
        Product ID: {ProductID}
    	Price: {Price}
    	Amount: {Amount}
";

}
