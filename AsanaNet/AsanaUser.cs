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
        public bool             DataComplete    { get; private set; }

        public void Parse(Dictionary<string, object> data)
        {
            DataComplete = true;

            Name    = data["name"].ToString();
            ID      = Int64.Parse(data["id"].ToString());

            if (data.ContainsKey("email"))
            {
                Email = data["email"].ToString();
            }
            else
                DataComplete = false;

            if (data.ContainsKey("workspaces"))
            {
                var workspaces = data["workspaces"] as List<object>;

                Workspaces = new AsanaWorkspace[workspaces.Count];
                for (int i = 0; i < workspaces.Count; ++i)
                {
                    AsanaWorkspace aw = new AsanaWorkspace();
                    aw.Parse(workspaces[i] as Dictionary<string, object>);
                    Workspaces[i] = aw;
                }
            }
            else
                DataComplete = false;
        }
    }
}
