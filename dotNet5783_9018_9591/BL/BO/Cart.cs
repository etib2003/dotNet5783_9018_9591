using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

internal class Cart
{
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAdress { get; set; }
    public OrderItem Items { get; set; }
    public double TotalPrice { get; set; }

    public override string ToString() => $@"
        CustomerName  :  {CustomerName}
        CustomerEmail: {CustomerEmail}
     	CustomerAdress: {CustomerAdress}
        Items: {Items}
     	TotalPrice: {TotalPrice}
";
}
