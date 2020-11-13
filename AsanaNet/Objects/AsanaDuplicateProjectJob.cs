using System;

namespace AsanaNet.Objects
{
    [Serializable]
    public class AsanaDuplicateProjectJob : AsanaJob
    {
        [AsanaDataAttribute("new_project", SerializationFlags.Required)] //
        public AsanaProject NewProject { get; set; }
    }
}
