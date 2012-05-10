using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaNet
{
    public class AsanaStory : IAsanaObject
    {
        public Int64 ID { get; private set; }
        public string Type { get; private set; }
        public string Text { get; private set; }
        public AsanaUser CreatedBy { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string Source { get; private set; }
        public AsanaTask Target { get; private set; }

        public bool Intact { get { return Target != null; } }

        public void Parse(Dictionary<string, object> data)
        {
            ID = Utils.SafeAssign<Int64>(data, "id");
            Type = Utils.SafeAssignString(data, "type");
            Text = Utils.SafeAssignString(data, "text");
            CreatedBy = Utils.SafeAssign<AsanaUser>(data, "created_by");
            CreatedAt = Utils.SafeAssign<DateTime>(data, "created_at");
            Source = Utils.SafeAssignString(data, "source");
            Target = Utils.SafeAssign<AsanaTask>(data, "target");
        }
    }
}
