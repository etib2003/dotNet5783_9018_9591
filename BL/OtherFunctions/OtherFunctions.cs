

using BO;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace OtherFunctions
{
    internal static class OtherFunctions
    {
        internal static void negativeNumber(this int number)
        {
            if (number <= 0)
                throw new BO.NegativeNumberException("");

        }

        internal static void wrongLengthNumber(this int number, int length)
        {
            if (number.ToString().Length < length)
                throw new BO.WrongLengthException("");


        }
        internal static void wrongLengthName(this string name)
        {
            Regex regex = new Regex("^[A-Za-z]+$");
            if(!regex.IsMatch(name))           
                throw new BO.wrongLengthNameException("");
        }
        internal static void negativeDoubleNumber(this double number)
        {
            if (number <= 0)
                throw new BO.negativeDoubleNumberException("");
        }
        internal static void notValidEmail(this string email)
        {
            if (!new EmailAddressAttribute().IsValid(email))
                throw new BO.notValidEmailException("");
        }
    

    }
}
