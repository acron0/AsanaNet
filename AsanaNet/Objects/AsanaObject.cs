using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

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

    public abstract class AsanaObject
    {
        [AsanaDataAttribute("id", SerializationFlags.Omit)]
        public Int64 ID { get; private set; }

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
    }

    public interface IAsanaObjectCollection : IList<AsanaObject>
    {
    }

    public class AsanaObjectCollection : List<AsanaObject>, IAsanaObjectCollection
    {
    }

    static public class AsanaObjectExtensions
    {
        static public void Save(this AsanaObject obj, Asana host, AsanaFunction function)
        {
            host.Save(obj, function);
        }

        static public void Save(this AsanaObject obj, Asana host)
        {
            host.Save(obj, null);
        }

        static public void Save(this AsanaObject obj, AsanaFunction function)
        {
            if (obj.Host == null)
                throw new NullReferenceException("This AsanaObject does not have a host associated with it so you must specify one when saving.");
            obj.Host.Save(obj, function);
        }

        static public void Save(this AsanaObject obj)
        {
            if (obj.Host == null)
                throw new NullReferenceException("This AsanaObject does not have a host associated with it so you must specify one when saving.");
            obj.Host.Save(obj, null);
        }
    }
}
