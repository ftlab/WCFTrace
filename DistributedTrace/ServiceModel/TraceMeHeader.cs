using DistributedTrace.Core;
using System.Runtime.Serialization;

namespace DistributedTrace.ServiceModel
{
    /// <summary>
    /// Заголовок о необходимости трассировать удаленный вызов
    /// </summary>
    [DataContract(Name = HeaderName, Namespace = Namespace.Main)]
    public class TraceMeHeader
    {
        /// <summary>
        /// Имя заголовка
        /// </summary>
        public const string HeaderName = "traceme";
        /// <summary>
        /// Идентификатор трассировки
        /// </summary>
        [DataMember]
        public TraceId Id { get; set; }
    }
}
