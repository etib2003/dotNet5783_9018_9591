using BlApi;

namespace BlImplementation;

sealed internal class Bl : IBl
{
    public IOrder Order => new Order();
    public IProduct Product => new Product();
    public ICart Cart => new Cart();
}
