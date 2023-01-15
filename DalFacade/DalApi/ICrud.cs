using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO;

public interface ICrud <T> where T : struct
{
    public int Create(T Or);
    public IEnumerable<T?> RequestAll(Func<T?, bool>? cond = null);
    public T Get (Func<T?, bool>? cond );
    public T GetById(int id);
    public void Update(T Or);
    public void Delete(int id);

}
