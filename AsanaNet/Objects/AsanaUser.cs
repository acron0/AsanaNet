using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaNet
{
    public class AsanaUser : IAsanaObject
    {
        [AsanaDataAttribute("name")]
        public string           Name            { get; private set; }

        [AsanaDataAttribute("email")]
        public string           Email           { get; private set; }

        [AsanaDataAttribute("id")]
        public Int64            ID              { get; private set; }

        [AsanaDataAttribute("workspaces")]
        public AsanaWorkspace[] Workspaces      { get; private set; }

        // ------------------------------------------------------

        public bool Intact { get { return Workspaces != null; } }
    }
}
