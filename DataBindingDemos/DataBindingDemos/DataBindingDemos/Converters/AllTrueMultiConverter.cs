﻿using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace DataBindingDemos
{
    public class AllTrueMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || !targetType.IsAssignableFrom(typeof(bool)))
            {
                // Return UnsetValue to use the binding FallbackValue
                return BindableProperty.UnsetValue;
            }

            foreach (var value in values)
            {
                if (!(value is bool b))
                {
                    return BindableProperty.UnsetValue;
                }
                else if (!b)
                {
                    return false;
                }
            }
            return true;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if (!(value is bool b) || targetTypes.Any(t => !t.IsAssignableFrom(typeof(bool))))
            {
                // Return null to indicate conversion back is not possible
                return null;
            }

            if (b)
            {
                return targetTypes.Select(t => (object)true).ToArray();
            }
            else
            {
                // Can't convert back from false because of ambiguity
                return null;
            }
        }
    }
}