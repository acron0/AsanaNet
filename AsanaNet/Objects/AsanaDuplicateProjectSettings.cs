using System;

namespace AsanaNet.Objects
{
    [Serializable]
    public class AsanaDuplicateProjectSettings : AsanaObject
    {
        [AsanaDataAttribute("name", SerializationFlags.Required)] //
        public string Name { get; set; }

        [AsanaDataAttribute("include", SerializationFlags.Optional)] //
        public string Include { get; set; }

        [AsanaDataAttribute("schedule_dates", SerializationFlags.Optional)] //
        public AsanaScheduleDates ScheduleDates { get; set; }
    }
}
