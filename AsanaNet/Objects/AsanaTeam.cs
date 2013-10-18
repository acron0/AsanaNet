using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaNet
{
    public class AsanaTeam : AsanaObject, IAsanaData
    {
        [AsanaDataAttribute("name")]
        public string Name  { get; set; }

        // ------------------------------------------------------

        public bool IsObjectLocal { get { return true; } }

        public void Complete()
        {
            throw new NotImplementedException();
        }
    }
}
