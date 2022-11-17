﻿namespace DO;
/// <summary>
/// a struct for the orders that contains all of the order's parameters
/// </summary>
public struct Order
{
    /// <summary>
    /// order's unique id
    /// </summary>
    public int seqNum { get; set; }

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
    /// order's order date
    /// </summary>
    public DateTime OrderDate { get; set; }

    /// <summary>
    /// order's ship date
    /// </summary>
    public DateTime ShipDate { get; set; }

    /// <summary>
    /// order's delivery date
    /// </summary>
    public DateTime DeliveryDate { get; set; }

    /// <summary>
    /// the order's print method
    /// </summary>
    /// <returns>the way the order is printed</returns>
    public override string ToString() => $@"
        Customer seqNum= {seqNum}: {CustomerName}, 
        Email: {CustomerEmail}
        Adress: {CustomerAdress}
        Order date: {OrderDate}
        Ship date: {ShipDate}    	
        Delivery date: {DeliveryDate} 
";
}
 