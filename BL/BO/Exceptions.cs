﻿using System;
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

 

 
    [Serializable]
    internal class wrongLengthNameException : Exception
    {
        public wrongLengthNameException()
        {
        }

        public wrongLengthNameException(string? message) : base(message)
        {
        }

        public wrongLengthNameException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected wrongLengthNameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    
}

[Serializable]
internal class negativeDoubleNumberException : Exception
{
    public negativeDoubleNumberException()
    {
    }

    public negativeDoubleNumberException(string? message) : base(message)
    {
    }

    public negativeDoubleNumberException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected negativeDoubleNumberException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
[Serializable]
internal class DateAlreadyUpdatedException : Exception
{
    public DateAlreadyUpdatedException()
    {
    }

    public DateAlreadyUpdatedException(string? message) : base(message)
    {
    }

    public DateAlreadyUpdatedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected DateAlreadyUpdatedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}

[Serializable]
internal class notValidEmailException : Exception
{
    public notValidEmailException()
    {
    }

    public notValidEmailException(string? message) : base(message)
    {
    }

    public notValidEmailException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected notValidEmailException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}

[Serializable]
internal class notValidAmountException : Exception
{
    public notValidAmountException()
    {
    }

    public notValidAmountException(string? message) : base(message)
    {
    }

    public notValidAmountException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected notValidAmountException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}

[Serializable]
internal class productAlreadyInOrderProssesException : Exception
{
    public productAlreadyInOrderProssesException()
    {
    }

    public productAlreadyInOrderProssesException(string? message) : base(message)
    {
    }

    public productAlreadyInOrderProssesException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected productAlreadyInOrderProssesException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
