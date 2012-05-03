using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaNet
{
    public class AsanaUser : IAsanaObject
    {
        public string           Name            { get; private set; }
        public string           Email           { get; private set; }
        public Int64            ID              { get; private set; }
        public AsanaWorkspace[] Workspaces      { get; private set; }

        public void Parse(Dictionary<string, object> data)
        {
            Name        = Utils.SafeAssignString(data, "name");
            ID          = Utils.SafeAssign<Int64>(data, "id");
            Email       = Utils.SafeAssignString(data, "email");
            Workspaces  = Utils.SafeAssignArray<AsanaWorkspace>(data, "workspaces");
        }
    }
}
