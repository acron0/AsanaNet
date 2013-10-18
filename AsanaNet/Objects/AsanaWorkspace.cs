using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaNet
{
    public class AsanaWorkspace : AsanaObject, IAsanaData
    {
        [AsanaDataAttribute("name")]
        public string Name  { get; private set; }

        [AsanaDataAttribute("is_organization")]
        public bool IsOrganization { get; private set; }

        // ------------------------------------------------------

        public bool IsObjectLocal { get { return true; } }

        public void Complete()
        {
            throw new NotImplementedException();
        }
    }
}
