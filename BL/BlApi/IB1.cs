using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IB1
{
    public Icart Cart { get; }
    public IOrder Order { get; }
    public IProduct Product { get; }


}
