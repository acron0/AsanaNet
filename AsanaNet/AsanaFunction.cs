using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaNet
{
    public partial class AsanaFunction
    {
        private static Dictionary<Function, AsanaFunction> Functions = new Dictionary<Function, AsanaFunction>();
        private static Dictionary<Type, AsanaFunctionAssociation> Associations = new Dictionary<Type, AsanaFunctionAssociation>();

        public string Url { get; private set; }
        public string Method { get; private set; }

        public AsanaFunction(string url, string methd)
        {
            Url = url;
            Method = methd;
        }        

        static public AsanaFunction GetFunction(Function en)
        {
            return Functions[en];
        }

        static public AsanaFunctionAssociation GetFunctionAssociation(Type t)
        {
            return Associations[t];
        }
    }

    public class AsanaFunctionAssociation
    {
        public AsanaFunction Create { get; private set; }
        public AsanaFunction Update { get; private set; }
        public AsanaFunction Delete { get; private set; }

        public AsanaFunctionAssociation(AsanaFunction create, AsanaFunction update, AsanaFunction delete)
        {
            Create = create;
            Update = update;
            Delete = delete;
        }
    }
}
