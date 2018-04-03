using DistributedTrace.Core;
using System.Runtime.Serialization;

namespace DistributedTrace.ServiceModel
{
    /// <summary>
    /// Заголовок о необходимости трассировать удаленный вызов
    /// </summary>
    [DataContract(Name = HeaderName, Namespace = Namespace.Value)]
    public class TraceMeHeader
    {
        public const string HeaderName = "traceme";
        /// <summary>
        /// Идентификатор трассировки
        /// </summary>
        [DataMember]
        public TraceId Id { get; set; }
    }
}
