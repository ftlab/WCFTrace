using System.Runtime.Remoting.Messaging;

namespace DistributedTrace.Core
{
    /// <summary>
    /// Механизм хранения данных в некотором окружении
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AsyncLocal<T>
    {
        /// <summary>
        /// Значение
        /// </summary>
        public T Value
        {
            get
            {
                var obj = CallContext.GetData(GetType().FullName);
                return (obj == null) ? default(T) : (T)obj;
            }
            set
            {
                CallContext.SetData(GetType().FullName, value);
            }
        }
    }
}
