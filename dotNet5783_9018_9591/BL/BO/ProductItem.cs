using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  BO;

internal class ProductItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public Category category { get; set; }
    public int caAmounttegory { get; set; }
    public bool InStock { get; set; }
}
