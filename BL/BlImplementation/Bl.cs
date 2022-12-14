using BlApi;

namespace BlImplementation;

sealed internal class Bl : IBl
{
    public IOrder Order { get; }
    public IProduct Product { get; }
    public ICart Cart { get; }
    internal Bl()
    {
        Order = new Order();
        Product = new Product();
        Cart = new Cart();
    }
}
