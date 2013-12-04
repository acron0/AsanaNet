using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace AsanaNet
{
    [Serializable]
    public class AsanaWorkspace : AsanaObject, IAsanaData
    {
        [AsanaDataAttribute("name")]
        public string Name  { get; private set; }

        [AsanaDataAttribute("is_organization")]
        public bool? IsOrganization { get; private set; }

        // ------------------------------------------------------

        public bool IsObjectLocal { get { return ID == 0; } }

        public void Complete()
        {
            throw new NotImplementedException();
        }

        static public implicit operator AsanaWorkspace(Int64 ID)
        {
            return Create(typeof(AsanaWorkspace), ID) as AsanaWorkspace;
        }

        public override Task Refresh()
        {
            return Host.GetWorkspaceById(ID, workspace =>
            {
                Name = (workspace as AsanaWorkspace).Name;
                IsOrganization = (workspace as AsanaWorkspace).IsOrganization;
            });
        }
    }
}
