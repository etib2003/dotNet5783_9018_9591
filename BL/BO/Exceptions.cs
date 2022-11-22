﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Do;

[Serializable]
public class DalAlreadyExistsException : Exception
{
    public DalAlreadyExistsException() : base() { }
    public DalAlreadyExistsException(string message) : base(message) { }
    public DalAlreadyExistsException(string message, Exception inner) : base(message, inner) { }

    override public string ToString() => "Already exists!";
}

[Serializable]
public class RequestException : Exception
{
    public RequestException() : base() { }
    public RequestException(string message) : base(message) { }
    public RequestException(string message, Exception inner) : base(message, inner) { }
    protected RequestException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    override public string ToString() => "Does'nt exists!";
}

[Serializable]
public class UpdateException : Exception
{
    public UpdateException() : base() { }
    public UpdateException(string message) : base(message) { }
    public UpdateException(string message, Exception inner) : base(message, inner) { }
    protected UpdateException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    override public string ToString() => "Does'nt exist!";
}



[Serializable]
public class DeleteException : Exception
{
    public DeleteException() : base() { }
    public DeleteException(string message) : base(message) { }
    public DeleteException(string message, Exception inner) : base(message, inner) { }
    protected DeleteException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    override public string ToString() => "Does'nt exist!";
}

