using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsanaNet
{
    /// <summary>
    /// Creation only object
    /// </summary>
    [Serializable]
    public class AsanaSubtask : AsanaTask
    {
        internal AsanaSubtask()
        {
        }

        public AsanaSubtask(AsanaWorkspace workspace, AsanaTask parentTask) 
        {
            Workspace = workspace;
            Parent = parentTask;
        }

        public AsanaSubtask(AsanaWorkspace workspace, AsanaTask parentTask, Int64 id) 
        {
            ID = id;
            Workspace = workspace;
            Parent = parentTask;

            // cache current state
            SetAsReferenceObject();
            //SavingCallback(Parsing.Serialize(this, false, true));
        }
    }
}
