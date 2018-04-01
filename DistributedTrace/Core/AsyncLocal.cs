using System.Runtime.Remoting.Messaging;

namespace DistributedTrace.Core
{
    public class AsyncLocal<T>
    {
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
