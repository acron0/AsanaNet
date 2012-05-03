using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace AsanaNet
{
    static class Utils
    {
        static public string SafeAssignString(Dictionary<string, object> source, string name)
        {
            if (source.ContainsKey(name))
            {
                return source[name].ToString();
            }

            return null;
        }

        static public T SafeAssign<T>(Dictionary<string, object> source, string name) where T : new()
        {
            if (typeof(IAsanaObject).IsAssignableFrom(typeof(T)))
                return SafeAssignAsanaObject<T>(source, name);

            T value = default(T);

            if (source.ContainsKey(name) && source[name] != null)
            {   
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
                if (converter.CanConvertFrom(typeof(string)) && !string.IsNullOrWhiteSpace(source[name].ToString()))
                {
                    value =  (T)converter.ConvertFromString(source[name].ToString());
                }
            }

            return value;
        }

        static public T[] SafeAssignArray<T>(Dictionary<string, object> source, string name) where T : new()
        {
            if (typeof(IAsanaObject).IsAssignableFrom(typeof(T)))
                return SafeAssignAsanaObjectArray<T>(source, name);

            T[] value = null;

            if (source.ContainsKey(name))
            {
            }

            return value;
        }

        static internal T SafeAssignAsanaObject<T>(Dictionary<string, object> source, string name) where T : new()
        {
            T value = default(T);

            if (source.ContainsKey(name))
            {
                var obj = source[name] as Dictionary<string, object>;
                value = new T();
                (value as IAsanaObject).Parse(obj);
            }

            return value;
        }

        static internal T[] SafeAssignAsanaObjectArray<T>(Dictionary<string, object> source, string name) where T : new()
        {
            T[] value = null;

            if (source.ContainsKey(name))
            {
                var list = source[name] as List<object>;

                value = new T[list.Count];
                for (int i = 0; i < list.Count; ++i)
                {
                    T newObj = new T();
                    (newObj as IAsanaObject).Parse(list[i] as Dictionary<string, object>);
                    value[i] = newObj;
                }
            }

            return value;
        }
    }
}
