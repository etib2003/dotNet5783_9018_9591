namespace BlImplementation
{
    internal interface ICart
    {
        BO.Cart UpdateAmountOfProduct(Cart cart, int productId, int amount);
    }
}