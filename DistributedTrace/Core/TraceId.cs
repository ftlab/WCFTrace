using System;
using System.Runtime.Serialization;

namespace DistributedTrace.Core
{
    /// <summary>
    /// Идентификатор трассировки
    /// </summary>
    [DataContract]
    public class TraceId
    {
        /// <summary>
        /// Уникальный номер
        /// </summary>
        [DataMember]
        public string Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Проверка на равенство
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(TraceId other)
        {
            if (other == null) return false;

            return other.Id == Id
                && other.Name == Name;
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
                Id = id,
                Name = name,
            };
        }

        /// <summary>
        /// Создание идентификатора трассировки
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static TraceId Create(string name)
        {
            return Create(Guid.NewGuid().ToString(), name);
        }

        /// <summary>
        /// Отображаемое значение
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("[{0}, {1}]", Name, Id);
        }
    }
}
