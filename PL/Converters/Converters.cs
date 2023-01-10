using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Converters
{
    public class VisibiltyAndShipDate : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values[0] is null && (bool)values[1] == false ? Visibility.Visible : Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class VisibiltyAndDeliveryDate : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values[0] is not null && values[1] is null && (bool)values[2] == false ? Visibility.Visible : Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IsEmptyTotalPriceConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (double)value==0 ? false : true;

        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ExplainCvvConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (bool)value ? Visibility.Visible : Visibility.Collapsed;
 
        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class FormatValidityConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (bool)value ? Visibility.Visible : Visibility.Collapsed;

        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IsValidTBConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => ((string)value).Length==0 || ((string)value)=="0" ? Visibility.Visible : Visibility.Collapsed;

        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IsUpdateCondConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (string)value == "Update" ? Visibility.Visible :Visibility.Collapsed;
        }

        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IsValidProdcutConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.All(t => !string.IsNullOrWhiteSpace(t as string)) && (string)values[0]!="0" && (string)values[2]!="0";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IsValidDetailsConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.All(t => !string.IsNullOrWhiteSpace(t as string));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IsValidCTBConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => ((string)value).Length == 0 ? Visibility.Visible : Visibility.Collapsed;

        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IdBoxIsReadOnlyConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => $"{value}" == "Update";

        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


}
