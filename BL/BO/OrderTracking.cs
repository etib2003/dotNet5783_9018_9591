using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enums;

namespace BO;

public class OrderTracking
{
    public int ID { get; set; }
    public OrderStatus Status { get; set; }
    //רשימה של צמדים
    public List<Tuple<DateTime, OrderStatus>> orderProgress { get; set; }

    public override string ToString() => $@"
        ID  :  {ID}
        Status: {Status}     	 
";
}
