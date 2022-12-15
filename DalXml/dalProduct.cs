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
    string path = "products.xml";

    public int Create(Product Or)
    {
        List<Product> prodLst = XmlTools.LoadListFromXMLSerializer<Product>(path);

        if (prodLst.Exists(x => x.Id == Or.Id))
            throw new DalAlreadyExistsException("Product");

        prodLst.Add(Or);

        XmlTools.SaveListToXMLSerializer(prodLst, path);

        return Or.Id;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Product Get(Func<Product?, bool>? cond)
    {
        return DataSource._products.FirstOrDefault(cond!) ?? throw new DalDoesNoExistException("Product");
    }

    public Product GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Product?> RequestAll(Func<Product?, bool>? cond = null)
    {
        List<DO.Product?> prodList = XmlTools.LoadListFromXMLSerializer<DO.Product?>(path);

        if (cond == null)
            return prodList.AsEnumerable();

        return prodList.Where(cond);
    }

    public void Update(Product Or)
    {
        throw new NotImplementedException();
    }
}

