using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaNet
{
    public class AsanaWorkspace : IAsanaObject
    {
        [AsanaDataAttribute("name")]
        public string Name  { get; private set; }

        [AsanaDataAttribute("id")]
        public Int64  ID    { get; private set; }

        // ------------------------------------------------------

        public bool Intact { get { return true; } }
    }
}
