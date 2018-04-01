using DistributedTrace.Core;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace DistributedTrace.ServiceModel.Service
{
    public class TraceMessageInspector : IDispatchMessageInspector
    {
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            var header = request.Headers.GetHeader<TraceMeHeader>(
                TraceMeHeader.HeaderName, TraceMeHeader.Namespace);

            if (header == null) return null;

            return new TraceContextScope(TraceContextMode.Add);
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            var scope = correlationState as TraceContextScope;

            if (scope == null) return;

            try
            {
                var ctx = scope.Context;
                if (ctx == null) return;

                var header = MessageHeader.CreateHeader(
                    TraceHeader.HeaderName, TraceHeader.Namespace
                    , new TraceHeader() { Trace = ctx.Trace });

                reply.Headers.Add(header);
            }
            finally
            {
                scope.Dispose();
            }
        }
    }
}
