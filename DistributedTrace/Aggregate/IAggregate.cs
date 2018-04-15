using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistributedTrace.Stat
{
    /// <summary>
    /// Интерфейс аггрегируемого значения
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAggregate<T>
    {
        /// <summary>
        /// Пустой аггрегат
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        /// Кол-во значений
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Тип аггрегата
        /// </summary>
        AggregateType Type { get; }

        /// <summary>
        /// Значение
        /// </summary>
        T Value { get; }

        /// <summary>
        /// Добавить
        /// </summary>
        /// <param name="value">значение</param>
        void Add(T value);

        /// <summary>
        /// Сбросить
        /// </summary>
        void Reset();
    }
}
