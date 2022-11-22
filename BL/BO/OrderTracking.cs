namespace BO;

public class OrderTracking
{
    public int ID { get; set; }
    public Do.OrderStatus Status { get; set; }

    //רשימה של צמדים
    public List<Tuple<DateTime, Do.OrderStatus>> OrderProgress { get; set; }

    public override string ToString() => $@"
        ID  :  {ID}
        Status: {Status}
        OrderProgress:
"; //לטפל בהדפסה

}
