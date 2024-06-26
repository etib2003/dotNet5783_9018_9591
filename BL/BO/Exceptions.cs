﻿using System.Runtime.Serialization;


namespace BO;

[Serializable]
public class WrongLengthException : Exception
{
    public WrongLengthException() { }
    public WrongLengthException(string? message) : base(message) { }
    public WrongLengthException(string? message, Exception? innerException) : base(message, innerException) { }
    protected WrongLengthException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}


[Serializable]
public class NegativeNumberException : Exception
{
    public NegativeNumberException() { }
    public NegativeNumberException(string? message) : base(message) { }
    public NegativeNumberException(string? message, Exception? innerException) : base(message, innerException) { }
    protected NegativeNumberException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}


[Serializable]
public class BoDoesNoExistException : Exception
{
    public BoDoesNoExistException() { }
    public BoDoesNoExistException(string? message) : base(message) { }
    public BoDoesNoExistException(string? message, Exception? innerException) : base(message, innerException) { }
    protected BoDoesNoExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}

[Serializable]
public class BoAlreadyExistsException : Exception
{
    public BoAlreadyExistsException() { }
    public BoAlreadyExistsException(string? message) : base(message) { }
    public BoAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException) { }
    protected BoAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}

[Serializable]
public class NotValidFormatNameException : Exception
{
    public NotValidFormatNameException() { }
    public NotValidFormatNameException(string? message) : base(message) { }
    public NotValidFormatNameException(string? message, Exception? innerException) : base(message, innerException) { }
    protected NotValidFormatNameException(SerializationInfo info, StreamingContext context) : base(info, context) { }

}

[Serializable]
public class NegativeDoubleNumberException : Exception
{
    public NegativeDoubleNumberException() { }
    public NegativeDoubleNumberException(string? message) : base(message) { }
    public NegativeDoubleNumberException(string? message, Exception? innerException) : base(message, innerException) { }
    protected NegativeDoubleNumberException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
[Serializable]
public class DateAlreadyUpdatedException : Exception
{
    public DateAlreadyUpdatedException() { }
    public DateAlreadyUpdatedException(string? message) : base(message) { }
    public DateAlreadyUpdatedException(string? message, Exception? innerException) : base(message, innerException) { }
    protected DateAlreadyUpdatedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}

[Serializable]
public class NotValidEmailException : Exception
{
    public NotValidEmailException() { }
    public NotValidEmailException(string? message) : base(message) { }
    public NotValidEmailException(string? message, Exception? innerException) : base(message, innerException) { }
    protected NotValidEmailException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}

[Serializable]
public class NotValidAmountException : Exception
{
    public NotValidAmountException() { }
    public NotValidAmountException(string? message) : base(message) { }
    public NotValidAmountException(string? message, Exception? innerException) : base(message, innerException) { }
    protected NotValidAmountException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}

[Serializable]
public class NotValidDeleteException : Exception
{
    public NotValidDeleteException() { }
    public NotValidDeleteException(string? message) : base(message) { }
    public NotValidDeleteException(string? message, Exception? innerException) : base(message, innerException) { }
    protected NotValidDeleteException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}

[Serializable]
public class NotInStockException : Exception
{
    public NotInStockException() { }
    public NotInStockException(string? message) : base(message) { }
    public NotInStockException(string? message, Exception? innerException) : base(message, innerException) { }
    protected NotInStockException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}

[Serializable]
public class NotExistInCartException : Exception
{
    public NotExistInCartException() { }
    public NotExistInCartException(string? message) : base(message) { }
    public NotExistInCartException(string? message, Exception? innerException) : base(message, innerException) { }
    protected NotExistInCartException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}

[Serializable]
public class DateHasNotUpdatedYetException : Exception
{
    public DateHasNotUpdatedYetException() { }
    public DateHasNotUpdatedYetException(string? message) : base(message) { }
    public DateHasNotUpdatedYetException(string? message, Exception? innerException) : base(message, innerException) { }
    protected DateHasNotUpdatedYetException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}