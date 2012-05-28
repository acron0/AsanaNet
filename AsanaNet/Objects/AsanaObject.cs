using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaNet
{
    public interface IAsanaObject
    {
        bool Intact { get; }
    }

    public interface IAsanaObjectCollection : IList<IAsanaObject>
    {
    }

    public class AsanaObjectCollection : List<IAsanaObject>, IAsanaObjectCollection
    {
    }

    static public class AsanaObjectExtensions
    {
        static public void Save(this IAsanaObject obj, Asana host)
        {
            host.Save(obj);
        }
    }
}
