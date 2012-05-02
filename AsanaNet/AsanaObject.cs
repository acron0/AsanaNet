using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaNet
{
    public interface IAsanaObject
    {
        void Parse(Dictionary<string, object> data);
    }

    public interface IAsanaObjectCollection : IList<IAsanaObject>
    {
    }

    public class AsanaObjectCollection : List<IAsanaObject>, IAsanaObjectCollection
    {
    }
}
