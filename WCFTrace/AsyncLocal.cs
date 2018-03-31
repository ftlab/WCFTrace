using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;

namespace WCFTrace
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
