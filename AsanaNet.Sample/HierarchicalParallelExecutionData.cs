using System;
using System.Collections.Generic;

namespace AsanaNet.Sample
{
    class HierarchicalParallelExecutionData
    {
        public string Info { get; set; }
        public AsanaObject Object { get; set; }
        public List<HierarchicalParallelExecutionData> Items { get; set; } = new List<HierarchicalParallelExecutionData>();

        public void WriteToConsole()
        {
            Console.WriteLine(Info);
            foreach (var item in Items)
                item.WriteToConsole();
        }
    }
}