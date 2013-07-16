using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaNet
{
    public class AsanaProject : AsanaObject, IAsanaData
    {
        [AsanaDataAttribute("name")]
        public string Name { get; private set; }

        [AsanaDataAttribute("created_at")]
        public AsanaDateTime CreatedAt { get; private set; }

        [AsanaDataAttribute("modified_at")]
        public AsanaDateTime ModifiedAt { get; private set; }

        [AsanaDataAttribute("notes")]
        public string Notes { get; private set; }

        [AsanaDataAttribute("archived")]
        public bool Archived { get; private set; }

        [AsanaDataAttribute("workspace")]
        public AsanaWorkspace Workspace { get; private set; }

        [AsanaDataAttribute("followers")]
        public AsanaUser[] Followers { get; private set; }

        [AsanaDataAttribute("team")]
        public AsanaTeam Team { get; private set; }

        // ------------------------------------------------------

        public bool IsObjectLocal { get { return true; } }

        public void Complete()
        {
            throw new NotImplementedException();
        }
    }
}
