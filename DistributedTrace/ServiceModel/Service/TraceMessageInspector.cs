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
            var header = request.Headers.GetHeader<TraceMeHeader>(
                TraceMeHeader.HeaderName, TraceMeHeader.Namespace);

            if (header == null) return null;

            return new TraceContextScope(header.Id, TraceContextMode.Add);
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

                var header = MessageHeader.CreateHeader(
                    TraceHeader.HeaderName, TraceHeader.Namespace
                    , new TraceHeader()
                    {
                        Event = @event
                    });

                reply.Headers.Add(header);
            }
            finally
            {
                scope.Dispose();
            }
        }
    }
}
