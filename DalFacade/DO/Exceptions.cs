using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DO;

/// <summary>
///An exception for not found
/// </summary>
public class DalDoesNoExistException : Exception
{
    public DalDoesNoExistException(string type) : base($"{type} was not found") { }
}
/// <summary>
///An exception for found
/// </summary>
public class DalAlreadyExistsException : Exception
{
    public DalAlreadyExistsException(string type) : base($"{type} already exists") { }
}

[Serializable]
public class DalConfigException : Exception
{
    public DalConfigException(string msg) : base(msg) { }
    public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
}

[Serializable]
public class XMLFileLoadCreateException : Exception
{
    private string filePath;
    private string v;
    Exception ex;
    public XMLFileLoadCreateException(string msg) : base(msg) { }
    public XMLFileLoadCreateException(string filePath, string v, Exception ex)
    {
        this.filePath = filePath;
        this.v = v;
        this.ex = ex;
    }
}