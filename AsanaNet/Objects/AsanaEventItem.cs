using System;

namespace AsanaNet
{
    [Serializable]
    public class AsanaEvent : AsanaObject
    {
        /// <summary>
        /// Events occurring between the last token generated and the query being executed 
        /// </summary>
        [AsanaDataAttribute("data", SerializationFlags.Omit)]
        public AsanaEventItem[] Data { get; private set; }
        
        /// <summary>
        /// Token for next event query
        /// </summary>
        [AsanaDataAttribute("sync", SerializationFlags.Omit)]
        public string Sync { get; private set; }
        
        #region Nested class
        
        [Serializable]
        public class AsanaEventItem : AsanaObject, IAsanaData
        {
            [AsanaDataAttribute("user", SerializationFlags.Optional)]
            public AsanaUser User { get; private set; }
        
            [AsanaDataAttribute("created_at", SerializationFlags.Omit)]
            public AsanaDateTime CreatedAt { get; private set; }
        
            [AsanaDataAttribute("type", SerializationFlags.Omit)]
            public string Type { get; private set; }
        
            [AsanaDataAttribute("action", SerializationFlags.Omit)]
            public string Action { get; private set; }
        
            [AsanaDataAttribute("resource", SerializationFlags.Omit)]
            public AsanaEventResource Resource { get; private set; }
        
            [AsanaDataAttribute("parent", SerializationFlags.Omit)]
            public AsanaEventParent Parent { get; private set; }
        
            // ------------------------------------------------------
        
            public bool IsObjectLocal => false;        
        }
        
        [Serializable]
        public class AsanaEventResource : AsanaObject, IAsanaData
        {
            [AsanaDataAttribute("resource_type", SerializationFlags.Omit)]
            public string ResourceType { get; private set; }
            
            [AsanaDataAttribute("name", SerializationFlags.Omit)]
            public string Name { get; private set; }
            
            [AsanaDataAttribute("resource_subtype", SerializationFlags.Omit)]
            public string ResourceSubtype { get; private set; }
            
            [AsanaDataAttribute("created_at", SerializationFlags.Omit)]
            public AsanaDateTime CreatedAt { get; private set; }
            
            [AsanaDataAttribute("created_by", SerializationFlags.Omit)]
            public AsanaUser CreatedBy { get; private set; }
            
            [AsanaDataAttribute("type", SerializationFlags.Omit)]
            public string Type { get; private set; }
            
            [AsanaDataAttribute("text", SerializationFlags.Omit)]
            public string Text { get; private set; }
            
            // ------------------------------------------------------
            
            public bool IsObjectLocal => false;
        }

        [Serializable]
        public class AsanaEventParent : AsanaObject, IAsanaData
        {
            [AsanaDataAttribute("resource_type", SerializationFlags.Omit)]
            public string ResourceType { get; private set; }
            
            [AsanaDataAttribute("name", SerializationFlags.Omit)]
            public string Name { get; private set; }
            
            [AsanaDataAttribute("resource_subtype", SerializationFlags.Omit)]
            public string ResourceSubtype { get; private set; }
            
            // ------------------------------------------------------
            
            public bool IsObjectLocal => false;
        }
        
        #endregion
    }    
}