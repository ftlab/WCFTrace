using DistributedTrace.Core;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace DistributedTrace.ServiceModel.Service
{
    /// <summary>
    /// Инспектор сообщения обработки трассировки
    /// </summary>
    public class TraceMessageInspector : IDispatchMessageInspector
    {
        /// <summary>
        /// После принятия запроса если присутствует флаг TraceMe, создаем окружение трассировки
        /// </summary>
        /// <param name="request"></param>
        /// <param name="channel"></param>
        /// <param name="instanceContext"></param>
        /// <returns></returns>
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            var index = request.Headers.FindHeader(TraceMeHeader.HeaderName, TraceMeHeader.Namespace);
            if (index < 0) return null;

            var header = request.Headers.GetHeader<TraceMeHeader>(index);
            if (header == null) return null;

            var @event = new TraceEvent()
            {
                Start = DateTime.Now,
                Message = request.Headers.Action,
                Source = instanceContext.Host.Description.Name,
                Type = "DISPATCH"
            };

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

                var @event = ctx.Event;
                @event.End = DateTime.Now;

                var index = reply.Headers.FindHeader(TraceHeader.HeaderName, TraceHeader.Namespace);
                if (index > -1)
                    reply.Headers.RemoveAt(index);

                var traceHeader = new TraceHeader()
                {
                    Event = @event,
                };

                var header = MessageHeader.CreateHeader(
                    TraceHeader.HeaderName, TraceHeader.Namespace
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
