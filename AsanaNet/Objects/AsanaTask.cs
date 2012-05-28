using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaNet
{
    public class AsanaTask : IAsanaObject
    {
        [AsanaDataAttribute("name")]
        public string           Name            { get; private set; }

        [AsanaDataAttribute("id")]
        public Int64            ID              { get; private set; }

        [AsanaDataAttribute("assignee")]
        public AsanaUser        Assignee        { get; private set; }

        [AsanaDataAttribute("assignee_status")]
        public string           AssigneeStatus  { get; private set; }

        [AsanaDataAttribute("created_at")]
        public DateTime         CreatedAt       { get; private set; }

        [AsanaDataAttribute("completed")]
        public bool             Completed       { get; private set; }

        [AsanaDataAttribute("name")]
        public DateTime         CompletedAt     { get; private set; }

        [AsanaDataAttribute("due_on")]
        public DateTime         DueOn           { get; private set; }

        [AsanaDataAttribute("followers")]
        public AsanaUser[]      Followers       { get; private set; }

        [AsanaDataAttribute("modified_at")]
        public DateTime         ModifiedAt      { get; private set; }

        [AsanaDataAttribute("notes")]
        public string           Notes           { get; private set; }

        [AsanaDataAttribute("projects")]
        public AsanaProject[]   Projects        { get; private set; }

        [AsanaDataAttribute("workspace")]
        public AsanaWorkspace   Workspace       { get; private set; }

        // ------------------------------------------------------

        public bool Intact { get { return Workspace != null; } }

        // ------------------------------------------------------

        public AsanaTask()
        {
        }

        public AsanaTask(string name, AsanaWorkspace workspace) 
        {
            Workspace = workspace;
            Name = name;
        }
    }
}
