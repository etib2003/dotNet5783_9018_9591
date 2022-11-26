namespace BO;
public class Cart
{
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAdress { get; set; }
    public List <BO.OrderItem> Items { get; set; }
    public double TotalPrice { get; set; }

    public override string ToString() {
        string s = $@"
        CustomerName  :  {CustomerName}
        CustomerEmail: {CustomerEmail}
     	CustomerAddress: {CustomerAdress}
     	TotalPrice: {TotalPrice}
        Items: ";
        foreach (var item in Items)
        { s = s + item+ "       \n"; };
        return s;

        //items: {string.Join(", ", Items)}
    }
}
