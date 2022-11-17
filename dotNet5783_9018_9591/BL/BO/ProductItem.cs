using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enums;

namespace BO;

public class ProductItem
{
    public int ID { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public category category { get; set; }
    public int  Amount { get; set; }
    public bool InStock { get; set; }

    public override string ToString() => $@"
        Product barcode: {ID}, {Name}
        Price: {Price}
        Category: {category}
        Amount: {Amount}   	
    	Amount in stock: {InStock}";

}
