using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enums;

namespace BO;

internal class OrderTracking
{
    public int ID { get; set; }
    public OrderStatus Status { get; set; }

    public override string ToString() => $@"
        ID  :  {ID}
        Status: {Status}     	 
       
";
}
