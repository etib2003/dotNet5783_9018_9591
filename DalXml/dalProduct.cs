using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DalApi;
using DO;
namespace Dal;

internal class dalProduct : IProduct
{
    public int Create(Product Or)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Product Get(Func<Product?, bool>? cond)
    {
        throw new NotImplementedException();
    }

    public Product GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Product?> RequestAll(Func<Product?, bool>? cond = null)
    {
        throw new NotImplementedException();
    }

    public void Update(Product Or)
    {
        throw new NotImplementedException();
    }
}

