using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaNet
{
    public class AsanaProject : IAsanaObject
    {
        public string Name { get; private set; }
        public Int64 ID { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime ModifiedAt { get; private set; }
        public string Notes { get; private set; }
        public bool Archived { get; private set; }
        public AsanaWorkspace Workspace { get; private set; }
        public AsanaUser[] Followers { get; private set; }

        public bool Intact { get { return true; } }

        public void Parse(Dictionary<string, object> data)
        {
            Name = Utils.SafeAssignString(data, "name");
            ID = Utils.SafeAssign<Int64>(data, "id");
            CreatedAt = Utils.SafeAssign<DateTime>(data, "created_at");
            ModifiedAt = Utils.SafeAssign<DateTime>(data, "modified_at");
            Notes = Utils.SafeAssignString(data, "notes");
            Archived = Utils.SafeAssign<bool>(data, "archived");
            Workspace = Utils.SafeAssign<AsanaWorkspace>(data, "workspace");
            Followers = Utils.SafeAssignArray<AsanaUser>(data, "followers");
        }
    }
}
