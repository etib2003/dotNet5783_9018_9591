﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  BO;

public class OrderItem
{
    /// <summary>
    /// orderItem's unique id
    /// </summary>
   // public List<int> ID = new List<int>();
    public int  ID { get; set; }
     
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
        Identification number= {ID}, {Name}
        Product ID: {ProductID}
    	Price: {Price}
    	Amount: {Amount}
        Total price: {TotalPrice}
";
}
