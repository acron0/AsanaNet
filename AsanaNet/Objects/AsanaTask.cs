using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaNet
{
    public enum AssigneeStatus
    {
        inbox,      //	In the inbox.
        later,      //	Scheduled for later.
        today,      //	Scheduled for today.
        upcoming        //	Marked as upcoming.
    }

    public class AsanaTask : AsanaObject, IAsanaData
    {
        [AsanaDataAttribute     ("name",            SerializationFlags.Required)]
        public string           Name                { get; set; }

        [AsanaDataAttribute     ("assignee",        SerializationFlags.Optional, "ID")]
        public AsanaUser        Assignee            { get; set; }

        [AsanaDataAttribute     ("assignee_status", SerializationFlags.Omit)]
        public AssigneeStatus   AssigneeStatus      { get; set; }

        [AsanaDataAttribute     ("created_at",      SerializationFlags.Omit)]
        public AsanaDateTime    CreatedAt           { get; private set; }

        [AsanaDataAttribute     ("completed",       SerializationFlags.Omit)]
        public bool             Completed           { get; set; }

        [AsanaDataAttribute     ("completed_at",    SerializationFlags.Omit)]
        public AsanaDateTime    CompletedAt         { get; private set; }

        [AsanaDataAttribute     ("due_on",          SerializationFlags.Optional)]
        public AsanaDateTime    DueOn               { get; set; }

        [AsanaDataAttribute     ("followers",       SerializationFlags.Optional)]
        public AsanaUser[]      Followers           { get; private set; }

        [AsanaDataAttribute     ("modified_at",     SerializationFlags.Omit)]
        public AsanaDateTime    ModifiedAt          { get; private set; }

        [AsanaDataAttribute     ("notes",           SerializationFlags.Optional)]
        public string           Notes               { get; set; }

        [AsanaDataAttribute     ("projects",        SerializationFlags.Optional, "ID")]
        public AsanaProject[]   Projects            { get; private set; }

        [AsanaDataAttribute     ("tags",            SerializationFlags.Optional, "ID")]
        public AsanaTag[]       Tags                { get; private set; }

        [AsanaDataAttribute     ("workspace",       SerializationFlags.Required, "ID")]
        public AsanaWorkspace   Workspace           { get; private set; }

        // ------------------------------------------------------

        public bool IsObjectLocal { get { return ID == 0; } }

        public void Complete()
        {
            throw new NotImplementedException();
        }

        // ------------------------------------------------------

        internal AsanaTask()
        {
            
        }

        public AsanaTask(AsanaWorkspace workspace) 
        {
            Workspace = workspace;
        }

        public void AddProject(AsanaProject proj, Asana host)
        {
            Dictionary<string, object> project = new Dictionary<string, object>();
            project.Add("project", proj.ID);
            AsanaResponseEventHandler savedCallback = null;
            savedCallback = (s) =>
            {
                // add it manually
                if (Projects == null)
                    Projects = new AsanaProject[1];
                else
                {
                    AsanaProject[] lProjects = Projects;
                    Array.Resize(ref lProjects, Projects.Length + 1);
                    Projects = lProjects;
                }

                Projects[Projects.Length - 1] = proj;
                Saving -= savedCallback;
            };
            Saving += savedCallback;
            host.Save(this, AsanaFunction.GetFunction(Function.AddProjectToTask), project);
        }

        public void AddProject(AsanaProject proj)
        {
            if (Host == null)
                throw new NullReferenceException("This AsanaObject does not have a host associated with it so you must specify one when saving.");
            AddProject(proj, Host);
        }

        public void RemoveProject(AsanaProject proj, Asana host)
        {
            Dictionary<string, object> project = new Dictionary<string, object>();
            project.Add("project", proj.ID);
            AsanaResponseEventHandler savedCallback = null;
            savedCallback = (s) =>
            {
                // add it manually
                int index = Array.IndexOf(Projects, proj);
                AsanaProject[] lProjects = new AsanaProject[Projects.Length - 1];
                if(index != 0)
                    Array.Copy(Projects, lProjects, index);
                Array.Copy(Projects, index+1, lProjects, index, lProjects.Length - index);

                Projects = lProjects;
                Saving -= savedCallback;
            };
            Saving += savedCallback;
            host.Save(this, AsanaFunction.GetFunction(Function.RemoveProjectFromTask), project);
        }

        public void RemoveProject(AsanaProject proj)
        {
            if (Host == null)
                throw new NullReferenceException("This AsanaObject does not have a host associated with it so you must specify one when saving.");
            RemoveProject(proj, Host);
        }
    }
}
