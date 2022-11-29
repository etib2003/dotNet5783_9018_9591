using DocumentFormat.OpenXml.Spreadsheet;
using System.ComponentModel.DataAnnotations;

namespace OtherFunctions
{
    internal static class OtherFunctions
    {
        /// <summary>
        /// Check if the number is negative
        /// </summary>
        /// <param name="number">this number</param>
        /// <exception cref="Negative number"></exception>
        internal static void negativeNumber(this int number)
        {
            if (number <= 0)
                throw new BO.NegativeNumberException("Negative number");

        }
        /// <summary>
        /// Check if the number's length is shorter than the allowed length
        /// </summary>
        /// <param name="number">this number</param>
        /// <param name="length">allowed length</param>
        /// <exception cref="Wrong length number"></exception>
        internal static void wrongLengthNumber(this int number, int length)
        {
            if (number.ToString().Length < length)
                throw new BO.WrongLengthException("Wrong length number");


        }
        /// <summary>
        /// check if the name's format valid
        /// </summary>
        /// <param name="name">this name</param>
        /// <exception cref="Not valid name"></exception>
        internal static void notValidName(this string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new BO.NotValidFormatNameException("Not valid name");
        }
        /// <summary>
        /// Check if the number is negative
        /// </summary>
        /// <param name="number">this number</param>
        /// <exception cref="Negative double number"></exception>
        internal static void negativeDoubleNumber(this double number)
        {
            if (number <= 0)
                throw new BO.NegativeDoubleNumberException("Negative double number");
        }
        /// <summary>
        /// Check if the email is valid
        /// </summary>
        /// <param name="email">this email</param>
        /// <exception cref="Not valid email"></exception>
        internal static void notValidEmail(this string email)
        {
            if (!new EmailAddressAttribute().IsValid(email))
                throw new BO.NotValidEmailException("Not valid email");
        }
    }
}
