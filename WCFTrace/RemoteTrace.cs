using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCFTrace
{
    public class RemoteTrace
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Error { get; set; }
        public List<RemoteTrace> SubTraces { get; set; }
    }
}
