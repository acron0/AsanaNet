using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaNet
{
    public class AsanaWorkspace : IAsanaObject
    {
        public string Name  { get; private set; }
        public Int64  ID    { get; private set; }

        public void Parse(Dictionary<string, object> data)
        {
            Name    = Utils.SafeAssignString(data, "name");
            ID      = Utils.SafeAssign<Int64>(data, "id");
        }
    }
}
