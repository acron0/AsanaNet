using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace AsanaNet
{
    [Serializable]
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

        [AsanaDataAttribute("team", SerializationFlags.Optional, "ID")] //
        public AsanaTeam Team { get; private set; }

        [AsanaDataAttribute("color", SerializationFlags.Omit)] //
        public string Color { get; private set; }

        // ------------------------------------------------------

        //public bool IsObjectLocal { get { return true; } }
        public bool IsObjectLocal { get { return ID == 0; } }

        public void Complete()
        {
            throw new NotImplementedException();
        }

        internal AsanaProject()
        {
        }

        static public implicit operator AsanaProject(Int64 ID)
        {
            return Create(typeof(AsanaProject), ID) as AsanaProject;
        }

        public AsanaProject(Int64 id = 0)
        {
            ID = id;
        }

        public AsanaProject(AsanaWorkspace workspace, Int64 id = 0) 
        {
            ID = id;
            Workspace = workspace;
        }

        public AsanaProject(AsanaWorkspace workspace, AsanaTeam team, Int64 id = 0)
        {
            ID = id;
            Workspace = workspace;
            Team = team;
        }

        public override async Task RefreshAsync(Asana host = null)
        {
            CheckHost(host);
            var project = await (host ?? Host).GetProjectByIdAsync(ID);
            Name = project.Name;
            CreatedAt = project.CreatedAt;
            ModifiedAt = project.ModifiedAt;
            Notes = project.Notes;
            Archived = project.Archived;
            Workspace = project.Workspace;
            Followers = project.Followers;
            Team = project.Team;
            Color = project.Color;
        }
    }
}
