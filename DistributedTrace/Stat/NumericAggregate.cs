using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistributedTrace.Stat
{
    /// <summary>
    /// Числовой аггрегат
    /// </summary>
    /// <typeparam name="T">тип числа</typeparam>
    public class NumericAggregate<T> : IAggregate<T>
    {
        /// <summary>
        /// Числовой аггрегат
        /// </summary>
        /// <param name="type"></param>
        public NumericAggregate(AggregateType type)
        {
            Type = type;
        }

        /// <summary>
        /// Пустой аггрегат
        /// </summary>
        public virtual bool IsEmpty { get { return Count < 1; } }

        /// <summary>
        /// Кол-во
        /// </summary>
        public virtual int Count { get; private set; }

        /// <summary>
        /// Тип аггрегата
        /// </summary>
        public AggregateType Type { get; private set; }

        /// <summary>
        /// Значение аггрегата
        /// </summary>
        public T Value { get; private set; }

        /// <summary>
        /// Добавить
        /// </summary>
        /// <param name="v"></param>
        public void Add(T v)
        {
            if (v == null) return;

            if (IsEmpty)
            {
                Value = v;
            }
            else
            {
                switch (Type)
                {
                    case AggregateType.First:
                        break;
                    case AggregateType.Last:
                        Value = v;
                        break;
                    case AggregateType.Sum:
                        Value = NumericOperators<T>.Add(Value, v);
                        break;
                    case AggregateType.Max:
                        if (NumericOperators<T>.Greater(v, Value))
                        {
                            Value = v;
                        }
                        break;
                    case AggregateType.Min:
                        if (NumericOperators<T>.Less(v, Value))
                        {
                            Value = v;
                        }
                        break;
                    case AggregateType.Avg:
                        // s[n] = s[n-1] + (v - s[n-1]) / N
                        var dt = NumericOperators<T>.Add(v, NumericOperators<T>.Negate(Value));
                        var p = NumericOperators<T>.Cast(Count + 1);
                        Value = NumericOperators<T>.Add(Value, NumericOperators<T>.Divide(dt, p));
                        break;
                    default:
                        throw new NotSupportedException(Type.ToString());
                }
            }
            Count++;
        }

        /// <summary>
        /// Сброс аггрегата
        /// </summary>
        public void Reset()
        {
            Count = 0;
            Value = default(T);
        }

        /// <summary>
        /// Отображаемое значение
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} = {1}", Type, Value);
        }
    }
}
