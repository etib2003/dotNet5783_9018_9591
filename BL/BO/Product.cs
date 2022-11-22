using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  BO;

public class Product
{

    /// <summary>
    /// product's unique barcode
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// product's name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// product's price
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// product's Category
    /// </summary>
    public BO.Category Category { get; set; }

    /// <summary>
    /// Product's Color
    /// </summary>
    public BO.Color Color { get; set; }

    /// <summary>
    /// product's amount in stock
    /// </summary>
    public int InStock { get; set; }


    /// <summary>
    /// the product's print method
    /// </summary>
    /// <returns>the way the product is printed</returns>
    public override string ToString() => $@"
        Product barcode: {ID}, {Name}
        Category: {Category}
        Color: {Color}
    	Price: {Price}
    	Amount in stock: {InStock}";

}
