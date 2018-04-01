using DistributedTrace.Core;
using System.Runtime.Serialization;

namespace DistributedTrace.ServiceModel
{
    [DataContract]
    public class TraceHeader
    {
        public const string HeaderName = "Trace";
        public const string Namespace = "http://fintech.ru/distributedtrace";

        [DataMember]
        public TraceLine Trace { get; set; }
    }
}
