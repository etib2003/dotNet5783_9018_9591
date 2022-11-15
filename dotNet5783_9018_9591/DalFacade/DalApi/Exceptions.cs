using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;

[Serializable]
public class ExistException : Exception
{
    int ID;
    public ExistException() : base() { }
    public ExistException(string message) : base(message) { }
    public ExistException(string message, Exception inner) : base(message, inner) { }
    protected ExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    override public string ToString() => "Already exists!";

}


[Serializable]
public class NotExistException : Exception
{
    int ID;
    public NotExistException() : base() { }
    public NotExistException(string message) : base(message) { }
    public NotExistException(string message, Exception inner) : base(message, inner) { }
    protected NotExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    override public string ToString() => "Does'nt exists!";

}
