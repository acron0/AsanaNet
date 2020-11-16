using System;

namespace AsanaNet.Objects
{
    [Serializable]
    public class AsanaJob : AsanaObject
    {
        [AsanaDataAttribute("status", SerializationFlags.Required)] //
        public string Status { get; set; }
    }
}
