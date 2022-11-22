using DocumentFormat.OpenXml.Drawing.Diagrams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Do;

public class ProductForList
{
    public int ID { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public Do.Category Category { get; set; }

    /// <summary>
    /// Product's color
    /// </summary>
    public Do.Color Color { get; set; }

    public override string ToString() => $@"
        Product barcode: {ID}, {Name}
        Category: {Category}
        Color: {Color}
     	Price: {Price}";
}
