using DistributedTrace.Core;
using System.Runtime.Serialization;

namespace DistributedTrace.ServiceModel
{
    /// <summary>
    /// Заголовок результата трассировки
    /// </summary>
    [DataContract(Name = HeaderName, Namespace = Namespace.Main)]
    public class TraceHeader
    {
        /// <summary>
        /// Имя заголовка
        /// </summary>
        public const string HeaderName = "trace";
        /// <summary>
        /// Идентификатор трассировки
        /// </summary>
        [DataMember(Name = "traceid", Order = 0)]
        public TraceId TraceId { get; set; }
        /// <summary>
        /// Событие трассировки
        /// </summary>
        [DataMember(Name = "root", Order = 1)]
        public TraceEvent Root { get; set; }
    }
}
