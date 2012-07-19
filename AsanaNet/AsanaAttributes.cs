using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaNet
{
    [FlagsAttribute]
    public enum SerializationFlags
    {
        Optional,
        Required,
        Omit
    }

    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = false)]
    internal class AsanaDataAttribute : Attribute
    {
        public string Name { get; private set; }
        public SerializationFlags Flags { get; private set; }
        public string[] Fields { get; private set; }

        public AsanaDataAttribute(string name, SerializationFlags flags, params string[] fieldsToSerialize)
        {
            Name = name;
            Flags = flags;
            Fields = fieldsToSerialize;
        }

        public AsanaDataAttribute(string name)
            : this(name, SerializationFlags.Optional)
        {

        }
    }
}
