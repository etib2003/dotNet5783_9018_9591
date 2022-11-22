namespace BO;

public class OrderTracking
{
    public int ID { get; set; }
    public BO.OrderStatus Status { get; set; }

    //רשימה של צמדים
    public List<Tuple<DateTime, BO.OrderStatus>> OrderProgress { get; set; }

    public override string ToString() => $@"
        ID  :  {ID}
        Status: {Status}
        OrderProgress:
"; //לטפל בהדפסה

}
