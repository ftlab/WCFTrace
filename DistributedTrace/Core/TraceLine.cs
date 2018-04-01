using System;
using System.Collections.Generic;

namespace DistributedTrace.Core
{
    public class TraceLine
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Error { get; set; }
        public List<TraceLine> SubTraces { get; set; }

        internal void AddTrace(object trace)
        {
            throw new NotImplementedException();
        }
    }
}
