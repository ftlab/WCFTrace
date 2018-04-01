namespace DistributedTrace.Core
{
    public class TraceContext
    {
        public static TraceContext Current { get; internal set; }

        public TraceLine Trace { get; internal set; }
    }
}
