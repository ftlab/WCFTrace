using System;

namespace DistributedTrace.Core
{
    /// <summary>
    /// Окружение контекста трассировки
    /// </summary>
    public class TraceContextScope : IDisposable
    {
        static AsyncLocal<TraceContextScope> _current = new AsyncLocal<TraceContextScope>();

        private bool _disposed;

        private TraceContextScope _expected;

        private TraceContextScope _saved;

        private readonly TraceId _id;

        private readonly TraceContextMode _mode;

        public TraceContextScope(TraceId id, TraceContextMode mode)
        {
            if (id == null) throw new ArgumentNullException("id");
            _id = id;
            _mode = mode;
        }

        public TraceContextScope(string id, string name, TraceContextMode mode) : this(TraceId.Create(id, name), mode) { }

        public TraceContextScope(string name, TraceContextMode mode) : this(TraceId.Create(name), mode) { }

        public TraceId Id { get { return _id; } }

        public TraceContextMode Mode { get { return _mode; } }

        public bool RequiredWrite
        {
            get
            {
                if (Mode == TraceContextMode.None)
                    return false;
                else if (Mode == TraceContextMode.New)
                    return true;
                else if (Mode == TraceContextMode.NewAndAdd)
                    return true;
                else if (Mode == TraceContextMode.Add)
                    return false;
                else if (Mode == TraceContextMode.AddOrNew)
                    return _saved == null;
                else throw new ArgumentOutOfRangeException(Mode.ToString());
            }
        }

        public TraceContext Context { get; internal set; }

        public static TraceContextScope Current
        {
            get
            {
                return _current.Value;
            }
            private set
            {
                _current.Value = value;
            }
        }

        public void Dispose()
        {
            if (_disposed) return;

            Context.Event.End = DateTime.Now;

            if (RequiredWrite)
                TraceWriter.Default.Write(Id, Context.Event);

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
