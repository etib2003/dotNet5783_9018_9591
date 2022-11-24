using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OtherFunctions
{
    internal static class OtherFunctions
    {
        internal static void negativeNumber(this int number)
        {
            if (number < 0)
                throw new BO.NegativeNumberException("");

        }

        internal static void wrongLengthNumber(this int number, int length)
        {
            if (number.ToString().Length < length)
                throw new BO.WrongLengthException("");

        }
    }
}
