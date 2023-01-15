using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO;

public interface IDal
{
    IOrder Order { get; }
    IProduct Product { get; }
    IOrderItem OrderItem { get; }
}
