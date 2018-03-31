using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace WCFTrace
{
    public class TraceContextScope : IDisposable
    {
        static AsyncLocal<TraceContextScope> _currentScope = new AsyncLocal<TraceContextScope>();

        private bool _disposed;

        private TraceContextScope _expected;

        private TraceContextScope _saved;

        public TraceContextScope(TraceContextMode mode)
        {
            Init();
        }

        private void Init()
        {
            //var octx = OperationContext.
        }

        public void Dispose()
        {
            if (_disposed) return;

            PopContext();
            _disposed = true;
        }

        private void PopContext()
        {

        }

        private bool NeedToCreateContext(TraceContextMode mode)
        {
            bool retVal = false;
            switch (mode)
            {
                case TraceContextMode.New:
                    retVal = true;
                    break;
                case TraceContextMode.Add:
                    _expected = _saved;
                    break;
                case TraceContextMode.AddOrNew:
                    _expected = _saved;
                    if (_expected == null)
                        retVal = true;
                    break;
                default: throw new ArgumentOutOfRangeException("mode");
            }

            return retVal;
        }
    }
}
