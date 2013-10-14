using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

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

        public override Task Refresh()
        {
            return Host.GetProjectById(ID, project =>
            {
                Name = (project as AsanaProject).Name;
                CreatedAt = (project as AsanaProject).CreatedAt;
                ModifiedAt = (project as AsanaProject).ModifiedAt;
                Notes = (project as AsanaProject).Notes;
                Archived = (project as AsanaProject).Archived;
                Workspace = (project as AsanaProject).Workspace;
                Followers = (project as AsanaProject).Followers;
                Team = (project as AsanaProject).Team;
            });
        }
    }
}
