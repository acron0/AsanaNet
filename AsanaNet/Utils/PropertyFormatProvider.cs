using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Reflection;

namespace AsanaNet
{
    public class PropertyFormatProvider : IFormatProvider, ICustomFormatter
    {
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
                return this;
            else
                return null;
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (string.IsNullOrWhiteSpace(format))
                return arg.ToString();

            var pInternal = arg.GetType().GetProperty(format);
            if (pInternal == null)
                throw new CustomAttributeFormatException(string.Format("An AsanaFunction tried to format a Property ('{0}') that couldn't be found. ", format));

            object value = pInternal.GetValue(arg, new object[] { });
            return value.ToString();
        }

        private string HandleOtherFormats(string format, object arg)
        {
            if (arg is IFormattable)
                return ((IFormattable)arg).ToString(format, CultureInfo.CurrentCulture);
            else if (arg != null)
                return arg.ToString();
            else
                return String.Empty;
        }
    }
}
