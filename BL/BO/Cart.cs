namespace BO;
public class Cart
{
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAdress { get; set; }
    public IEnumerable <Do.OrderItem> Items { get; set; } //לשנות לlist
    public double TotalPrice { get; set; }

    public override string ToString() {
        string s = $@"
        CustomerName  :  {CustomerName}
        CustomerEmail: {CustomerEmail}
     	CustomerAdress: {CustomerAdress}
     	TotalPrice: {TotalPrice}
        Items: ";
        foreach (var item in Items)
        { s = s + item+ "       \n"; };
        return s;

        //items: {string.Join(", ", Items)}
    }
}
