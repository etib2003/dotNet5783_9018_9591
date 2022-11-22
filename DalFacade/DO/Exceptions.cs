using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;

public class DalDoesNoExistException : Exception
{
    public DalDoesNoExistException(string type) : base($"{type} was not found") { }
}


public class UpdateException : Exception
{
    public UpdateException(string type) : base($"{type} already exists") { }
}


public class DalAlreadyExistsException : Exception
{
    public DalAlreadyExistsException(string type) : base($"{type} already exists") { }
}