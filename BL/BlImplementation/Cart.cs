using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;


namespace BlImplementation;

internal class Cart:ICart
{
    private DalApi.IDal Dal = new Dal.DalList();

    public Do.Cart AddProductToCart(Do.Cart cart, int productId)
    {
        DO.Product DOproduct = Dal.Product.RequestById(productId);
        //איך יודעים אם מוצר קיים בעגלה?
        throw new NotImplementedException();//למחוק


    }

    public Do.Cart UpdateAmountOfProduct(Do.Cart cart, int productId, int newAmount) //לעשות
    {
        throw new NotImplementedException();
        DO.Product DOproduct = Dal.Product.RequestById(productId);
       // if (DOproduct.ID==0)
            //throw new CreateException("Invalid product id"); //לשנות את החריגה
        //if(newAmount==0)

    }

    public void CommitOrder(Do.Cart cart, string customerName, string customerEmail, string customerAdress)
    {
        throw new NotImplementedException();
    }
}
