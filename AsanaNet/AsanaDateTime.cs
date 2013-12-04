using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Globalization;

namespace AsanaNet
{
    [Serializable]
    [TypeConverter(typeof(AsanaDateTimeConverter))]
    public class AsanaDateTime
    {
        public DateTime DateTime { get; set; }

        public AsanaDateTime()
        {
        
        }

        public AsanaDateTime(DateTime dt)
        {
            DateTime = dt;
        }

        public override string ToString()
        {
            return DateTime.ToString("yyyy-MM-dd");;
        }

        public static implicit operator AsanaDateTime(DateTime dt)
        {
            return new AsanaDateTime(dt);
        }

        public static bool operator ==(AsanaDateTime a, DateTime b)
        {
            if (object.ReferenceEquals(a, null))
            {
                return object.ReferenceEquals(b, null);
            }
            if (object.ReferenceEquals(b, null))
            {
                return object.ReferenceEquals(a, null);
            }
            return a.DateTime == b;
        }

        public static bool operator ==(AsanaDateTime a, AsanaDateTime b)
        {
            if (object.ReferenceEquals(a, null))
            {
                return object.ReferenceEquals(b, null);
            }
            if (object.ReferenceEquals(b, null))
            {
                return object.ReferenceEquals(a, null);
            }
            return a.DateTime == b.DateTime;
        }

        public static bool operator !=(AsanaDateTime a, DateTime b)
        {
            if (object.ReferenceEquals(a, null))
            {
                return !object.ReferenceEquals(b, null);
            }
            if (object.ReferenceEquals(b, null))
            {
                return !object.ReferenceEquals(a, null);
            }
            return a.DateTime != b;
        }

        public static bool operator !=(AsanaDateTime a, AsanaDateTime b)
        {
            if (object.ReferenceEquals(a, null))
            {
                return !object.ReferenceEquals(b, null);
            }
            if (object.ReferenceEquals(b, null))
            {
                return !object.ReferenceEquals(a, null);
            }
            return a.DateTime != b.DateTime;
        }

        public override bool Equals(object obj)
        {
            if (obj is DateTime)
            {
                return this.DateTime.Equals((DateTime)obj);
            }
            if (obj is AsanaDateTime)
            {
                return this == (obj as AsanaDateTime);
            }

            return false;
            
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ this.DateTime.GetHashCode();
        }

        public bool Equals(AsanaDateTime a)
        {
            return this.DateTime.Equals(a.DateTime);
        }

    }

    public class AsanaDateTimeConverter : TypeConverter
    {
        TypeConverter _converter;

        public AsanaDateTimeConverter()
        {
            _converter = TypeDescriptor.GetConverter(typeof(DateTime));
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return _converter.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                DateTime dt = (DateTime)_converter.ConvertFromString(value as string);
                return new AsanaDateTime(dt);
            }

            return null;
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return value.ToString();
            }

            return null;
        }
    }
}
