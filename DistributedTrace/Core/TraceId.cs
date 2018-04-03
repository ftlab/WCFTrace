﻿using System;
using System.Runtime.Serialization;

namespace DistributedTrace.Core
{
    /// <summary>
    /// Идентификатор трассировки
    /// </summary>
    [DataContract(Name = "traceid", Namespace = Namespace.Value)]
    public class TraceId
    {
        private TraceId() { }

        /// <summary>
        /// Уникальный номер
        /// </summary>
        [DataMember(Name = "id", Order = 0)]
        public string Id { get; private set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [DataMember(Name = "name", Order = 1)]
        public string Name { get; private set; }

        [DataMember(Name = "ts", Order = 2)]
        public DateTime Timestamp { get; private set; }

        /// <summary>
        /// Проверка на равенство
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(TraceId other)
        {
            if (other == null) return false;

            return other.Id == Id
                && other.Name == Name
                && other.Timestamp == Timestamp;
        }

        /// <summary>
        /// Проверка на равентство
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as TraceId);
        }

        /// <summary>
        /// Вычисление хэша
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        /// <summary>
        /// Создание идентификатора трассировки
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static TraceId Create(string id, string name)
        {
            return new TraceId()
            {
                Id = id ?? Guid.NewGuid().ToString(),
                Name = name,
                Timestamp = DateTime.UtcNow,
            };
        }

        /// <summary>
        /// Создание идентификатора трассировки
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static TraceId Create(string name)
        {
            return Create(null, name);
        }

        /// <summary>
        /// Отображаемое значение
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("[{0}, {1}, {2: yyyy-MM-dd HH:mm:ss}]", Name, Id, Timestamp.ToLocalTime());
        }
    }
}
