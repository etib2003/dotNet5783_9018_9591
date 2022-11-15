using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;

internal interface ICrud <T>
{
    public int Create(T Or);
    public IEnumerable<T> RequestAll();
    public T RequestById(int id);
    public void Update(T Or);
    public void Delete(int id);


}
