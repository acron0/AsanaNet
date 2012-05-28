using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaNet
{
    public class AsanaProject : IAsanaObject
    {
        [AsanaDataAttribute("name")]
        public string Name { get; private set; }

        [AsanaDataAttribute("id")]
        public Int64 ID { get; private set; }

        [AsanaDataAttribute("created_at")]
        public DateTime CreatedAt { get; private set; }

        [AsanaDataAttribute("modified_at")]
        public DateTime ModifiedAt { get; private set; }

        [AsanaDataAttribute("notes")]
        public string Notes { get; private set; }

        [AsanaDataAttribute("archived")]
        public bool Archived { get; private set; }

        [AsanaDataAttribute("workspace")]
        public AsanaWorkspace Workspace { get; private set; }

        [AsanaDataAttribute("followers")]
        public AsanaUser[] Followers { get; private set; }

        // ------------------------------------------------------

        public bool Intact { get { return true; } }

    }
}
