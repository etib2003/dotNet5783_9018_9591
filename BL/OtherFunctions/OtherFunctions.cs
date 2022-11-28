﻿

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
                throw new BO.NegativeNumberException("Negative number");

        }

        internal static void wrongLengthNumber(this int number, int length)
        {
            if (number.ToString().Length < length)
                throw new BO.WrongLengthException("Wrong length number");


        }
        internal static void NotValidName(this string name)
        {
            if(string.IsNullOrWhiteSpace(name))           
                throw new BO.NotValidNameException("Not valid name");
        }
        internal static void negativeDoubleNumber(this double number)
        {
            if (number <= 0)
                throw new BO.NegativeDoubleNumberException("Negative double number");
        }
        internal static void notValidEmail(this string email)
        {
            if (!new EmailAddressAttribute().IsValid(email))
                throw new BO.NotValidEmailException("Not valid email");
        }
    

    }
}
