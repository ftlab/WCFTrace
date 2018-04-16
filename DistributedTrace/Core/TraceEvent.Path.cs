using DistributedTrace.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistributedTrace.Core
{
    public partial class TraceEvent
    {
        public IEnumerable<TraceEvent> ByProperty(string name, string value)
        {
            foreach (var node in Flatten())
            {
                if (node.Value[name] == value)
                    yield return node.Value;
            }
        }

        public IEnumerable<TraceEvent> ByName(string name)
        {
            foreach (var node in Flatten())
            {
                if (node.Value.Name == name)
                    yield return node.Value;
            }
        }

        public IEnumerable<TraceEvent> ByPath(string path)
        {
            var tracePath = new TreePath();
            int prevLevel = -1;
            foreach (var node in Flatten())
            {
                if (node.Level > prevLevel)
                    tracePath.Push(node.Value.Name);
                else if (node.Level < prevLevel)
                    tracePath.Pop(prevLevel - node.Level);

                if (tracePath.Value == path)
                    yield return node.Value;

                prevLevel = node.Level;
            }
        }

        public IEnumerable<TraceEvent> ByProperty(string name)
        {
            foreach (var node in Flatten())
            {
                if (node.Value.ContainsProperty(name))
                    yield return node.Value;
            }
        }
    }
}
