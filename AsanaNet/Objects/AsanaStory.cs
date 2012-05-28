using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaNet
{
    public class AsanaStory : IAsanaObject
    {
        [AsanaDataAttribute("id")]
        public Int64 ID { get; private set; }

        [AsanaDataAttribute("type")]
        public string Type { get; private set; }

        [AsanaDataAttribute("text")]
        public string Text { get; private set; }

        [AsanaDataAttribute("created_by")]
        public AsanaUser CreatedBy { get; private set; }

        [AsanaDataAttribute("created_at")]
        public DateTime CreatedAt { get; private set; }

        [AsanaDataAttribute("source")]
        public string Source { get; private set; }

        [AsanaDataAttribute("target")]
        public AsanaTask Target { get; private set; }

        // ------------------------------------------------------

        public bool Intact { get { return Target != null; } }
    }
}
