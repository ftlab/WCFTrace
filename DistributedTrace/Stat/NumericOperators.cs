using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace DistributedTrace.Stat
{
    /// <summary>
    /// Числовые операции
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class NumericOperators<T>
    {
        /// <summary>
        /// сумма
        /// </summary>
        private static Func<T, T, T> _add;

        /// <summary>
        /// больше
        /// </summary>
        private static Func<T, T, bool> _greater;

        /// <summary>
        /// меньше
        /// </summary>
        private static Func<T, T, bool> _less;

        /// <summary>
        /// приведение
        /// </summary>
        private static Func<int, T> _cast;

        /// <summary>
        /// отрицание
        /// </summary>
        private static Func<T, T> _negate;

        /// <summary>
        /// деление
        /// </summary>
        private static Func<T, T, T> _divide;

        /// <summary>
        /// Сумма
        /// </summary>
        /// <param name="left">первый операнд</param>
        /// <param name="right">второй операнд</param>
        /// <returns></returns>
        public static T Add(T left, T right)
        {
            if (_add == null)
            {
                var l = Expression.Parameter(typeof(T), "left");
                var r = Expression.Parameter(typeof(T), "right");
                var b = Expression.Add(l, r);

                _add = Expression.Lambda<Func<T, T, T>>(b, l, r).Compile();
            }

            return _add(left, right);
        }

        /// <summary>
        /// Больше
        /// </summary>
        /// <param name="left">первый операнд</param>
        /// <param name="right">второй операнд</param>
        /// <returns></returns>
        public static bool Greater(T left, T right)
        {
            if (_greater == null)
            {
                var l = Expression.Parameter(typeof(T), "left");
                var r = Expression.Parameter(typeof(T), "right");
                var b = Expression.GreaterThan(l, r);

                _greater = Expression.Lambda<Func<T, T, bool>>(b, l, r).Compile();
            }

            return _greater(left, right);
        }

        /// <summary>
        /// Меньше
        /// </summary>
        /// <param name="left">первый операнд</param>
        /// <param name="right">второй операнд</param>
        /// <returns></returns>
        public static bool Less(T left, T right)
        {
            if (_less == null)
            {
                var l = Expression.Parameter(typeof(T), "left");
                var r = Expression.Parameter(typeof(T), "right");
                var b = Expression.LessThan(l, r);

                _less = Expression.Lambda<Func<T, T, bool>>(b, l, r).Compile();
            }

            return _less(left, right);
        }

        /// <summary>
        /// Приведение
        /// </summary>
        /// <param name="value">целое число</param>
        /// <returns></returns>
        public static T Cast(int value)
        {
            if (_cast == null)
            {
                var v = Expression.Parameter(typeof(int), "value");
                var b = Expression.Convert(v, typeof(T));

                _cast = Expression.Lambda<Func<int, T>>(b, v).Compile();
            }

            return _cast(value);
        }

        /// <summary>
        /// Отрицание
        /// </summary>
        /// <param name="value">значение</param>
        /// <returns></returns>
        public static T Negate(T value)
        {
            if (_negate == null)
            {
                var v = Expression.Parameter(typeof(T), "value");
                var b = Expression.Negate(v);

                _negate = Expression.Lambda<Func<T, T>>(b, v).Compile();
            }

            return _negate(value);
        }

        /// <summary>
        /// Деление
        /// </summary>
        /// <param name="left">первый операнд</param>
        /// <param name="right">второй операнд</param>
        /// <returns></returns>
        public static T Divide(T left, T right)
        {
            if (_divide == null)
            {
                var l = Expression.Parameter(typeof(T), "left");
                var r = Expression.Parameter(typeof(T), "right");
                var b = Expression.Divide(l, r);

                _divide = Expression.Lambda<Func<T, T, T>>(b, l, r).Compile();
            }

            return _divide(left, right);
        }
    }
}
