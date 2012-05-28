using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace AsanaNet
{
    static class Utils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        static public string SafeAssignString(Dictionary<string, object> source, string name)
        {
            if (source.ContainsKey(name))
            {
                return source[name].ToString();
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        static public T SafeAssign<T>(Dictionary<string, object> source, string name) where T : new()
        {
            if (typeof(IAsanaObject).IsAssignableFrom(typeof(T)))
                return SafeAssignAsanaObject<T>(source, name);

            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            T value = default(T);  

            if (source.ContainsKey(name) && source[name] != null)
            {   
                if (converter.CanConvertFrom(typeof(string)) && !string.IsNullOrWhiteSpace(source[name].ToString()))
                {
                    value =  (T)converter.ConvertFromString(source[name].ToString());
                }
            }

            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="name"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        static internal T SafeAssignAsanaObject<T>(Dictionary<string, object> source, string name) where T : new()
        {
            T value = default(T);

            if (source.ContainsKey(name))
            {
                var obj = source[name] as Dictionary<string, object>;
                value = new T();
                Utils.Deserialize(obj, (value as IAsanaObject));
            }

            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="name"></param>
        /// <returns></returns>
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
                    Utils.Deserialize(list[i] as Dictionary<string, object>, (newObj as IAsanaObject));
                    value[i] = newObj;
                }
            }

            return value;
        }

        /// <summary>
        /// Deserializes a dictionary based on AsanaDataAttributes
        /// </summary>
        /// <param name="data"></param>
        /// <param name="obj"></param>
        static internal void Deserialize(Dictionary<string, object> data, IAsanaObject obj)
        {
            foreach(var p in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                try
                {
                    AsanaDataAttribute ca = p.GetCustomAttributes(typeof(AsanaDataAttribute), false)[0] as AsanaDataAttribute;
                    if(!data.ContainsKey(ca.Name))
                        continue;

                    if (p.PropertyType.GUID == typeof(string).GUID)
                    {
                        p.SetValue(obj, SafeAssignString(data, ca.Name), null);
                    }
                    else
                    {
                        Type t = p.PropertyType.IsArray ? p.PropertyType.GetElementType() : p.PropertyType;
                        var method = typeof(Utils).GetMethod(p.PropertyType.IsArray ? "SafeAssignArray" : "SafeAssign").MakeGenericMethod(new Type[] { t });
                        var methodResult = method.Invoke(null, new object[] { data, ca.Name });
                        p.SetValue(obj, methodResult, null);
                    }
                }
                catch 
                { 
                }
            }
        }

        /// <summary>
        /// Deserializes a dictionary based on AsanaDataAttributes
        /// </summary>
        /// <param name="data"></param>
        /// <param name="obj"></param>
        static internal Dictionary<string, object> Serialize(IAsanaObject obj)
        {
            var dict = new Dictionary<string, object>();

            // TODO
            /*foreach (var p in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                try
                {
                    AsanaDataAttribute ca = p.GetCustomAttributes(typeof(AsanaDataAttribute), false)[0] as AsanaDataAttribute;

                    if (p.PropertyType.GUID == typeof(string).GUID)
                    {
                        p.SetValue(obj, SafeAssignString(data, ca.Name), null);
                    }
                    else
                    {
                        Type t = p.PropertyType.IsArray ? p.PropertyType.GetElementType() : p.PropertyType;
                        var method = typeof(Utils).GetMethod(p.PropertyType.IsArray ? "SafeAssignArray" : "SafeAssign").MakeGenericMethod(new Type[] { t });
                        var methodResult = method.Invoke(null, new object[] { data, ca.Name });
                        p.SetValue(obj, methodResult, null);
                    }
                }
                catch
                {
                }
            }*/

            return dict;
        }
    }
}
