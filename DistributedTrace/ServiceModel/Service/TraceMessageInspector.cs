using DistributedTrace.Core;
using System;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace DistributedTrace.ServiceModel.Service
{
    /// <summary>
    /// Инспектор сервисного поведения формирования удаленной трассировки
    /// </summary>
    public class TraceMessageInspector : IDispatchMessageInspector
    {
        public static string HostName = Dns.GetHostName();

        /// <summary>
        /// После принятия запроса если присутствует флаг TraceMe, создаем окружение трассировки
        /// </summary>
        /// <param name="request"></param>
        /// <param name="channel"></param>
        /// <param name="instanceContext"></param>
        /// <returns></returns>
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            var index = request.Headers.FindHeader(TraceMeHeader.HeaderName, Namespace.Main);
            if (index < 0) return null;

            var header = request.Headers.GetHeader<TraceMeHeader>(index);
            if (header == null) return null;

            var @event = TraceEvent.Create(
                id: header.Id
                , name: "disp> " + instanceContext.Host.Description.Name);

            @event["host"] = HostName;

            return new TraceContextScope(header.Id, @event, TraceContextMode.Add);
        }

        /// <summary>
        /// Перед отправкой ответа добавляем в заголовок результат трассировки
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="correlationState"></param>
        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            var scope = correlationState as TraceContextScope;

            if (scope == null) return;

            try
            {
                var ctx = scope.Context;

                var @event = ctx.Root;
                @event.SetEnd(scope.Id);

                var index = reply.Headers.FindHeader(TraceHeader.HeaderName, Namespace.Main);
                if (index > -1)
                    reply.Headers.RemoveAt(index);

                //@event.Cleanup();

                var traceHeader = new TraceHeader()
                {
                    TraceId = scope.Id,
                    Root = @event,
                };

                var header = MessageHeader.CreateHeader(
                    TraceHeader.HeaderName, Namespace.Main
                    , traceHeader);

                reply.Headers.Add(header);
            }
            finally
            {
                scope.Dispose();
            }
        }
    }
}
