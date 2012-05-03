using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaNet
{
    public class AsanaTask : IAsanaObject
    {
        public string           Name            { get; private set; }
        public Int64            ID              { get; private set; }
        public AsanaUser        Assignee        { get; private set; }
        public string           AssigneeStatus  { get; private set; }
        public DateTime         CreatedAt       { get; private set; }
        public bool             Completed       { get; private set; }
        public DateTime         CompletedAt     { get; private set; }
        public DateTime         DueOn           { get; private set; }
        public AsanaUser[]      Followers       { get; private set; }
        public DateTime         ModifiedAt      { get; private set; }
        public string           Notes           { get; private set; }
        //public AsanaProject[]   Projects        { get; private set; }
        public AsanaWorkspace   Workspace       { get; private set; }

        public void Parse(Dictionary<string, object> data)
        {
            Name            = Utils.SafeAssignString(data, "name");
            ID              = Utils.SafeAssign<Int64>(data, "id");

            Assignee        = Utils.SafeAssign<AsanaUser>(data, "assignee");
            AssigneeStatus  = Utils.SafeAssignString(data, "assignee_status");
            CreatedAt       = Utils.SafeAssign<DateTime>(data, "created_at");
            Completed       = Utils.SafeAssign<bool>(data, "completed");
            CompletedAt     = Utils.SafeAssign<DateTime>(data, "completed_at");
            DueOn           = Utils.SafeAssign<DateTime>(data, "due_on");
            ModifiedAt      = Utils.SafeAssign<DateTime>(data, "modified_at");
            Notes           = Utils.SafeAssignString(data, "notes");
            Workspace       = Utils.SafeAssign<AsanaWorkspace>(data, "workspace");
        }
    }
}
