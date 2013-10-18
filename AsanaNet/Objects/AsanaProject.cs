using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace AsanaNet
{
    public class AsanaProject : AsanaObject, IAsanaData
    {
        [AsanaDataAttribute("name", SerializationFlags.Required)] //
        public string Name { get; set; }

        [AsanaDataAttribute("created_at", SerializationFlags.Omit)] //
        public AsanaDateTime CreatedAt { get; private set; }

        [AsanaDataAttribute("modified_at", SerializationFlags.Omit)] //
        public AsanaDateTime ModifiedAt { get; private set; }

        [AsanaDataAttribute("notes", SerializationFlags.Optional)] //
        public string Notes { get; set; }

        [AsanaDataAttribute("archived", SerializationFlags.Omit)] //
        public bool Archived { get; private set; }

        [AsanaDataAttribute("workspace", SerializationFlags.Optional, "ID")] //
        public AsanaWorkspace Workspace { get; private set; }

        [AsanaDataAttribute("followers", SerializationFlags.Optional)] //
        public AsanaUser[] Followers { get; private set; }

        [AsanaDataAttribute("team", SerializationFlags.Optional)] //
        public AsanaTeam Team { get; private set; }

        // ------------------------------------------------------

        public bool IsObjectLocal { get { return true; } }

        public void Complete()
        {
            throw new NotImplementedException();
        }
        
        public AsanaProject()
        {
        }

        public AsanaProject(AsanaWorkspace workspace) 
        {
            Workspace = workspace;
        }

        public AsanaProject(AsanaWorkspace workspace, AsanaTeam team)
        {
            Workspace = workspace;
            Team = team;
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
