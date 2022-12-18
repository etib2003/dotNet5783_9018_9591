using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DalApi;
using DO;
using DocumentFormat.OpenXml.Drawing.Charts;

namespace Dal;

internal class dalProduct : IProduct
{
    string path = "products.xml";

    public int Create(Product product)
    {
        List<Product> prodLst = XmlTools.LoadListFromXMLSerializer<Product>(path);

        if (prodLst.Exists(x => x.Id == product.Id))
            throw new DalAlreadyExistsException("Product");

        prodLst.Add(product);
        XmlTools.SaveListToXMLSerializer(prodLst, path);
        return product.Id;
    }

    public void Delete(int id)
    {
        List<Product> prodLst = XmlTools.LoadListFromXMLSerializer<Product>(path);
        prodLst.Remove(GetById(id));
        XmlTools.SaveListToXMLSerializer(prodLst, path);
    }

    public Product Get(Func<Product?, bool>? cond)
    {
        return XmlTools.LoadListFromXMLSerializer<DO.Product?>(path).FirstOrDefault(cond!)
             ?? throw new DalDoesNoExistException("Product");
    }

    public Product GetById(int id)
    {
        return Get(x => x?.Id == id);
    }

    public IEnumerable<Product?> RequestAll(Func<Product?, bool>? cond = null)
    {
        List<DO.Product?> prodList = XmlTools.LoadListFromXMLSerializer<DO.Product?>(path);

        if (cond == null)
            return prodList.AsEnumerable();

        return prodList.Where(cond);
    }

    public void Update(Product product)
    {
        List<Product> prodLst = XmlTools.LoadListFromXMLSerializer<Product>(path);
        Delete(product.Id);
        prodLst.Add(product);
        XmlTools.SaveListToXMLSerializer(prodLst, path);

    }
}

