using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaNet
{
    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = false)]
    internal class AsanaDataAttribute : Attribute
    {
        public string Name { get; private set; }

        public AsanaDataAttribute(string name)
        {
            Name = name;
        }
    }
}
