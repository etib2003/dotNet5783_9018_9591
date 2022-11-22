using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Cart
{
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAdress { get; set; }
    public IEnumerable <BO.OrderItem> Items { get; set; } //לשנות לlist
    public double TotalPrice { get; set; }

    public override string ToString() {
        string s = $@"
        CustomerName  :  {CustomerName}
        CustomerEmail: {CustomerEmail}
     	CustomerAdress: {CustomerAdress}
     	TotalPrice: {TotalPrice}
        Items: ";
        foreach (var item in Items)
        { s = s + item+ "       \n"; };
        return s;

        //items: {string.Join(", ", Items)}
    }
}
