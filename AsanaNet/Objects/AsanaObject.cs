using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;

namespace AsanaNet
{
    /// <summary>
    /// This interface allows objects to specify whether they are 'in tact' and provides the interface for fetching them.
    /// </summary>
    public interface IAsanaData
    {
        bool IsObjectLocal { get; }
        //void Complete();
    }

    [Serializable]
    public abstract class AsanaObject
    {
        [AsanaDataAttribute("id", SerializationFlags.Omit)]
        public Int64 ID { get; protected set; }

        public Asana Host { get; protected set; }

        /// <summary>
        /// A positive response has been received but any object updating has yet to be performed.
        /// </summary>
        public event AsanaResponseEventHandler Saving;

        /// <summary>
        /// The object was saved successfully and changes should be reflected.
        /// </summary>
        public event AsanaResponseEventHandler Saved;

        // memento
        private Dictionary<string, object> _lastSave;

        internal bool IsDirty(string key, object value)
        {
            object lvalue = null;
            if(_lastSave.TryGetValue(key, out lvalue))
            {
                return !value.Equals(lvalue);
            }

            return true;
        }

        internal void SavingCallback(Dictionary<string, object> saved)
        {
            _lastSave = saved;

            if (Saving != null)
                Saving(this);
        }

        internal void SavedCallback()
        {
            if (Saved != null)
                Saved(this);
        }   
     
        /// <summary>
        /// Creates a new T without requiring a public constructor
        /// </summary>
        /// <param name="t"></param>
        internal static AsanaObject Create(Type t)
        {
            try
            {
                AsanaObject o = (AsanaObject)Activator.CreateInstance(t, true);
                return o;
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Creates a new T without requiring a public constructor
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        internal static AsanaObject Create(Type t, Int64 ID)
        {
            AsanaObject o = Create(t);
            o.ID = ID;
            return o;
        }

        /// <summary>
        /// Parameterless contructor
        /// </summary>
        internal AsanaObject()
        {
            
        }

        /// <summary>
        /// Overrides the ToString method.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ID.ToString();
        }

        public static bool operator ==(AsanaObject a, Int64 id)
        {
            return a.ID == id;
        }

        public static bool operator !=(AsanaObject a, Int64 id)
        {
            return a.ID != id;
        }

        public override bool Equals(object obj)
        {
            if (obj is AsanaObject)
            {
                return this.ID == (obj as AsanaObject).ID;
            }
            if (obj is Int64)
            {
                return this.ID == (Int64)obj;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public virtual Task Refresh()
        {
            throw new NotImplementedException();
        }
    }

    public interface IAsanaObjectCollection : IList<AsanaObject>
    {
    }

    [Serializable]
    public class AsanaObjectCollection : List<AsanaObject>, IAsanaObjectCollection
    {
    }

    static public class IAsanaObjectCollectionExtensions
    {
        static public List<Task> RefreshAll<T>(this IAsanaObjectCollection objects) where T : AsanaObject
        {
            List<Task> workers = new List<Task>();
            foreach (T o in objects)
            {
                if (o.Host == null)
                    throw new NullReferenceException("This AsanaObject does not have a host associated with it so you must specify one when saving.");
                workers.Add(o.Refresh());
            }
            return workers;
        }
    }

    static public class AsanaObjectExtensions
    {
        static public Task Save(this AsanaObject obj, Asana host, AsanaFunction function)
        {
            return host.Save(obj, function);
        }

        static public Task Save(this AsanaObject obj, Asana host)
        {
            return host.Save(obj, null);
        }

        static public Task Save(this AsanaObject obj, AsanaFunction function)
        {
            if (obj.Host == null)
                throw new NullReferenceException("This AsanaObject does not have a host associated with it so you must specify one when saving.");
            return obj.Host.Save(obj, function);
        }

        static public Task Save(this AsanaObject obj)
        {
            if (obj.Host == null)
                throw new NullReferenceException("This AsanaObject does not have a host associated with it so you must specify one when saving.");
            return obj.Host.Save(obj, null);
        }

        static public Task Delete(this AsanaObject obj, Asana host)
        {
            return host.Delete(obj);
        }

        static public Task Delete(this AsanaObject obj)
        {
            if (obj.Host == null)
                throw new NullReferenceException("This AsanaObject does not have a host associated with it so you must specify one when saving.");
            return obj.Host.Delete(obj);
        }
    }
}
