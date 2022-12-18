using DocumentFormat.OpenXml.Spreadsheet;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace OtherFunctions
{
    public static class OtherFunctions
    {
        /// <summary>
        /// Check if the number is negative
        /// </summary>
        /// <param name="number">this number</param>
        /// <exception cref="Negative number"></exception>
        public static void negativeNumber(this int number)
        {
            if (number < 0)
                throw new BO.NegativeNumberException("Negative number");
        }

        /// <summary>
        /// Check if the number's length is shorter than the allowed length
        /// </summary>
        /// <param name="number">this number</param>
        /// <param name="length">allowed length</param>
        /// <exception cref="Wrong length number"></exception>
        public static void wrongLengthNumber(this int number, int length)
        {
            if (number.ToString().Length < length || (length == 6 && number.ToString().Length != 6))
                throw new BO.WrongLengthException("Wrong length number");


        }
        /// <summary>
        /// check if the name's format valid
        /// </summary>
        /// <param name="name">this name</param>
        /// <exception cref="Not valid name"></exception>
        public static void notValidName(this string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new BO.NotValidFormatNameException("Not valid name");
        }
        /// <summary>
        /// Check if the number is negative
        /// </summary>
        /// <param name="number">this number</param>
        /// <exception cref="Negative double number"></exception>
        public static void negativeDoubleNumber(this double number)
        {
            if (number <= 0)
                throw new BO.NegativeDoubleNumberException("Negative double number");
        }
        /// <summary>
        /// Check if the email is valid
        /// </summary>
        /// <param name="email">this email</param>
        /// <exception cref="Not valid email"></exception>
        public static void notValidEmail(this string email)
        {
            if (!new EmailAddressAttribute().IsValid(email))
                throw new BO.NotValidEmailException("Not valid email");
        }

        /// <summary>
        /// Check if the amount is negative
        /// </summary>
        /// <param name="amount">this amount</param>
        /// <exception cref="Not In Stock"></exception>
        public static void notInStock(this int amount)
        {
            if (amount <= 0)
                throw new BO.NotInStockException("Not In Stock");

        }

        internal static Target CopyPropTo<Source, Target>(this Source source, Target target)
        {
            ///getting the target properties
            Dictionary<string, PropertyInfo> propertyInfoTarget = target!.GetType().GetProperties().ToDictionary(p => p.Name, p => p);
            ///getting the source properties
            IEnumerable<PropertyInfo> propertyInfoSource = source!.GetType().GetProperties();

            /// for every property that is in the source
            foreach (var sourcePropertyInfo in propertyInfoSource)
            {
                ///checks if the target contains the property info to reset the property
                if (propertyInfoTarget.ContainsKey(sourcePropertyInfo.Name)
                    && (sourcePropertyInfo.PropertyType == propertyInfoTarget[sourcePropertyInfo.Name].PropertyType
                    && (sourcePropertyInfo.PropertyType == typeof(string)) || !sourcePropertyInfo.PropertyType.IsClass))
                {
                    Type s = Nullable.GetUnderlyingType(sourcePropertyInfo.PropertyType)!;
                    Type t = Nullable.GetUnderlyingType(propertyInfoTarget[sourcePropertyInfo.Name].PropertyType)!;
                    var sourceValue = sourcePropertyInfo.GetValue(source);

                    if (sourceValue is not null)
                    {
                        if (s is not null && t is not null)
                            propertyInfoTarget[sourcePropertyInfo.Name].SetValue(target, Enum.ToObject(t, sourceValue));
                        else
                            propertyInfoTarget[sourcePropertyInfo.Name].SetValue(target, sourceValue);
                    }
                }
            }
            return target;
        }

        internal static Target CopyPropToStruct<Source, Target>(this Source source, Target target) where Target : struct
        {
            object obj = target;

            source.CopyPropTo(obj);

            return (Target)obj;
        }

    }
}
