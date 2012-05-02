using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaNet
{
    public class AsanaWorkspace : IAsanaObject
    {
        public string Name { get; private set; }
        public string ID { get; private set; }

        public void Parse(Dictionary<string, object> data)
        {
            Name    = data["name"].ToString();
            ID      = data["id"].ToString();
        }
    }
}
