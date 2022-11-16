using DO;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enums;

namespace BO;

internal class OrderForList
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
    /// order's status date
    /// </summary>
    public OrderStatus Status { get; set; }

    public int AmountOfItems { get; set; }

    public double TotalPrice { get; set; }

    public override string ToString() => $@"
            Customer ID= {ID}: {CustomerName}, 
            Status: {Status}
            Amount of items: {AmountOfItems}
            Total price: {TotalPrice}
    ";
}
