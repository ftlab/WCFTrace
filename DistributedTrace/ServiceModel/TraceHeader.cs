using DistributedTrace.Core;
using System.Runtime.Serialization;

namespace DistributedTrace.ServiceModel
{
    /// <summary>
    /// Заголовок результата трассировки
    /// </summary>
    [DataContract(Name = HeaderName, Namespace = Namespace)]
    public class TraceHeader
    {
        public const string HeaderName = "Trace";
        public const string Namespace = "http://fintech.ru/distributedtrace";

        /// <summary>
        /// Событие трассировки
        /// </summary>
        [DataMember]
        public TraceEvent Event { get; set; }
    }
}
