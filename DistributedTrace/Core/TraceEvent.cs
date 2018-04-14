using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;

namespace DistributedTrace.Core
{
    /// <summary>
    /// Событие трассировки
    /// </summary>
    [DataContract(Name = "ev", Namespace = Namespace.Main)]
    public partial class TraceEvent
    {
        /// <summary>
        /// Имя события
        /// </summary>
        [DataMember(Name = "n", Order = 0)]
        private string _name;

        /// <summary>
        /// Время начала события в мс
        /// </summary>
        [DataMember(Name = "b", Order = 1)]
        private int _begin;

        /// <summary>
        /// Время окончания события в мс
        /// </summary>
        [DataMember(Name = "e", Order = 2, EmitDefaultValue = false)]
        private int? _end;

        /// <summary>
        /// Вложенные события
        /// </summary>
        [DataMember(Name = "es", Order = 5, EmitDefaultValue = false)]
        private List<TraceEvent> _events;


        /// <summary>
        /// Свойства события
        /// </summary>
        [DataMember(Name = "props", Order = 6, EmitDefaultValue = false)]
        private TraceEventProperties _properties;

        /// <summary>
        /// Имя события
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Отображаемый текст события
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}{1}"
                , Name
                , _properties == null ? null
                    : "(" + _properties.ToString() + ")");
        }

        /// <summary>
        /// Создание события
        /// </summary>
        /// <param name="id">идентификатор трассировки</param>
        /// <param name="name">имя события</param>
        /// <returns></returns>
        public static TraceEvent Create(
            TraceId id
            , string name)
        {
            var @event = new TraceEvent()
            {
                BeginTime = (DateTime.UtcNow - id.Timestamp),
                Name = name,
            };

            return @event;
        }
    }
}
