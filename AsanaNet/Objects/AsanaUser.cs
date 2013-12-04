using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaNet
{
    [Serializable]
    public class AsanaUser : AsanaObject, IAsanaData
    {
        [AsanaDataAttribute("name")]
        public string           Name            { get; private set; }

        [AsanaDataAttribute("email")]
        public string           Email           { get; private set; }

        [AsanaDataAttribute("workspaces")]
        public AsanaWorkspace[] Workspaces      { get; private set; }

        // ------------------------------------------------------

        public bool IsObjectLocal { get { return ID == 0; } }

        public void Complete()
        {
            throw new NotImplementedException();
        }
    }
}
