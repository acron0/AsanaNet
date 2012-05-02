using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaNet
{
    class AsanaFunction
    {
        public string Url { get; set; }
        public string Method { get; set; }
        public AsanaFunction(string url, string method)
        {
            Url = url;
            Method = method;
        }
    }
}
