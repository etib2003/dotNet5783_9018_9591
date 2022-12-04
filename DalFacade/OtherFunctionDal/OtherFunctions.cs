using DocumentFormat.OpenXml.Spreadsheet;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace OtherFunctionDal
{
    public static class OtherFunctions
    {

        public static string ToStringProperty<T>(this T t)
        {
            string str = "";
            foreach (PropertyInfo item in typeof(T).GetProperties())
            {
                str += "\n" + item.Name + ": ";
                if (item.GetValue(t, null) is IEnumerable<object>)
                {
                    IEnumerable<object> listOfObjects = (IEnumerable<object>)item.GetValue(obj: t, null);
                    str += String.Join(" ", listOfObjects);
                }
                else
                    str += item.GetValue(t, null);
            }
            return str;
        }

    }
}
