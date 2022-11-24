using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BO;

[Serializable]
internal class WrongLengthException : Exception
{
    public WrongLengthException() { }

    public WrongLengthException(string? message) : base(message) { }
    

    public WrongLengthException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected WrongLengthException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}


    [Serializable]
    internal class NegativeNumberException : Exception
    {
        public NegativeNumberException()
        {
        }

        public NegativeNumberException(string? message) : base(message)
        {
        }

        public NegativeNumberException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NegativeNumberException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }


[Serializable]
internal class BoDoesNoExistException : Exception
{
    public BoDoesNoExistException()
    {
    }

    public BoDoesNoExistException(string? message) : base(message)
    {
    }

    public BoDoesNoExistException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected BoDoesNoExistException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
//[Serializable]
//public class DalAlreadyExistsException : Exception
//{
//    public DalAlreadyExistsException() : base() { }
//    public DalAlreadyExistsException(string message) : base(message) { }
//    public DalAlreadyExistsException(string message, Exception inner) : base(message, inner) { }

//    override public string ToString() => "Already exists!";
//}

//[Serializable]
//public class RequestException : Exception
//{
//    public RequestException() : base() { }
//    public RequestException(string message) : base(message) { }
//    public RequestException(string message, Exception inner) : base(message, inner) { }
//    protected RequestException(SerializationInfo info, StreamingContext context) : base(info, context) { }

//    override public string ToString() => "Does'nt exists!";
//}

//[Serializable]
//public class UpdateException : Exception
//{
//    public UpdateException() : base() { }
//    public UpdateException(string message) : base(message) { }
//    public UpdateException(string message, Exception inner) : base(message, inner) { }
//    protected UpdateException(SerializationInfo info, StreamingContext context) : base(info, context) { }

//    override public string ToString() => "Does'nt exist!";
//}



//[Serializable]
//public class DeleteException : Exception
//{
//    public DeleteException() : base() { }
//    public DeleteException(string message) : base(message) { }
//    public DeleteException(string message, Exception inner) : base(message, inner) { }
//    protected DeleteException(SerializationInfo info, StreamingContext context) : base(info, context) { }

//    override public string ToString() => "Does'nt exist!";
//}

