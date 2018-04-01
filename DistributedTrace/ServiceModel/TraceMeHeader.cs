using DistributedTrace.Core;
using System.Runtime.Serialization;

namespace DistributedTrace.ServiceModel
{
    /// <summary>
    /// Заголовок о необходимости трассировать удаленный вызов
    /// </summary>
    [DataContract(Name = HeaderName, Namespace = Namespace)]
    public class TraceMeHeader
    {
        public const string HeaderName = "TraceMe";
        public const string Namespace = "http://fintech.ru/distributedtrace";

        /// <summary>
        /// Идентификатор трассировки
        /// </summary>
        [DataMember]
        public TraceId Id { get; set; }
    }
}
