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
        public string Id { get; set; }

        /// <summary>
        /// Наименование трассировки
        /// </summary>
        [DataMember]
        public string Name { get; set; }

    }
}
