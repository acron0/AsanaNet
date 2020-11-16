using System;

namespace AsanaNet.Objects
{
    [Serializable]
    public class AsanaScheduleDates : AsanaObject
    {
        [AsanaDataAttribute("due_on", SerializationFlags.Optional)] //
        public DateTime DueOn { get; set; }

        [AsanaDataAttribute("should_skip_weekends", SerializationFlags.Optional)] //
        public bool SkipWeekends { get; set; }
    }
}
