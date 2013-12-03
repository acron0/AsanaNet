using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsanaNet
{
    public enum AssigneeStatus
    {
        inbox,      //	In the inbox.
        later,      //	Scheduled for later.
        today,      //	Scheduled for today.
        upcoming        //	Marked as upcoming.
    }

    [Serializable]
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

        public AsanaTask(AsanaWorkspace workspace, Int64 id = 0) 
        {
            ID = id;
            Workspace = workspace;
        }

        public Task AddProject(AsanaProject proj, Asana host)
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
            return host.Save(this, AsanaFunction.GetFunction(Function.AddProjectToTask), project);
        }

        public Task AddProject(AsanaProject proj)
        {
            if (Host == null)
                throw new NullReferenceException("This AsanaObject does not have a host associated with it so you must specify one when saving.");
            return AddProject(proj, Host);
        }

        public Task RemoveProject(AsanaProject proj, Asana host)
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
            return host.Save(this, AsanaFunction.GetFunction(Function.RemoveProjectFromTask), project);
        }

        public Task RemoveProject(AsanaProject proj)
        {
            if (Host == null)
                throw new NullReferenceException("This AsanaObject does not have a host associated with it so you must specify one when saving.");
            return RemoveProject(proj, Host);
        }

        public Task AddTag(AsanaTag proj, Asana host)
        {
            Dictionary<string, object> Tag = new Dictionary<string, object>();
            Tag.Add("tag", proj.ID);
            AsanaResponseEventHandler savedCallback = null;
            savedCallback = (s) =>
            {
                // add it manually
                if (Tags == null)
                    Tags = new AsanaTag[1];
                else
                {
                    AsanaTag[] lTags = Tags;
                    Array.Resize(ref lTags, Tags.Length + 1);
                    Tags = lTags;
                }

                Tags[Tags.Length - 1] = proj;
                Saving -= savedCallback;
            };
            Saving += savedCallback;
            return host.Save(this, AsanaFunction.GetFunction(Function.AddTagToTask), Tag);
        }

        public Task AddTag(AsanaTag proj)
        {
            if (Host == null)
                throw new NullReferenceException("This AsanaObject does not have a host associated with it so you must specify one when saving.");
            return AddTag(proj, Host);
        }

        public Task RemoveTag(AsanaTag proj, Asana host)
        {
            Dictionary<string, object> Tag = new Dictionary<string, object>();
            Tag.Add("tag", proj.ID);
            AsanaResponseEventHandler savedCallback = null;
            savedCallback = (s) =>
            {
                // add it manually
                int index = Array.IndexOf(Tags, proj);
                AsanaTag[] lTags = new AsanaTag[Tags.Length - 1];
                if (index != 0)
                    Array.Copy(Tags, lTags, index);
                Array.Copy(Tags, index + 1, lTags, index, lTags.Length - index);

                Tags = lTags;
                Saving -= savedCallback;
            };
            Saving += savedCallback;
            return host.Save(this, AsanaFunction.GetFunction(Function.RemoveTagFromTask), Tag);
        }

        public Task RemoveTag(AsanaTag proj)
        {
            if (Host == null)
                throw new NullReferenceException("This AsanaObject does not have a host associated with it so you must specify one when saving.");
            return RemoveTag(proj, Host);
        }
    }
}
