﻿namespace DO;

public struct Order
{
    public int ID { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAdress { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime ShipDate { get; set; }
    public DateTime DeliveryDate { get; set; }

    public override string ToString() => $@"
        Customer ID={ID}: {CustomerName}, 
        Email: {CustomerEmail}
        Adress: {CustomerAdress}
        Order date: {OrderDate}
        Ship date: {ShipDate}    	
        Delivery date: {DeliveryDate} 
";
}
 