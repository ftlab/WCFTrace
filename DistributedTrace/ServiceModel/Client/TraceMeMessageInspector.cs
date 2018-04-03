using DistributedTrace.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace DistributedTrace.ServiceModel.Client
{
    /// <summary>
    /// Инспектор включения в заголовок сообщения признака о необходимости формирования трассировки
    /// </summary>
    public class TraceMeMessageInspector : IClientMessageInspector
    {
        /// <summary>
        /// Перед отправкой сообщения включаем в заголовок информацию о текущей трассировке
        /// </summary>
        /// <param name="request"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            var scope = TraceContextScope.Current;
            if (scope == null) return null;

            var id = scope.Id;
            var @event = new TraceEvent()
            {
                Start = DateTime.Now,
                Type = "CALL",
                Message = request.Headers.Action,
            };

            var callScope = new TraceContextScope(id, @event, TraceContextMode.Add);

            var index = request.Headers.FindHeader(TraceMeHeader.HeaderName, TraceMeHeader.Namespace);
            if (index > -1)
                request.Headers.RemoveAt(index);

            var header = MessageHeader.CreateHeader(
                TraceMeHeader.HeaderName, TraceMeHeader.Namespace
                , new TraceMeHeader()
                {
                    Id = id,
                });

            request.Headers.Add(header);
            return callScope;
        }

        /// <summary>
        /// После получения ответа извлекаем трассировку
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="correlationState"></param>
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            var callScope = correlationState as TraceContextScope;
            if (callScope == null) return;
            try
            {
                var index = reply.Headers.FindHeader(TraceHeader.HeaderName, TraceHeader.Namespace);
                if (index < 0) return;

                var header = reply.Headers.GetHeader<TraceHeader>(index);
                if (header != null && header.Event != null)
                    callScope.Context.AppendEvent(header.Event);
            }
            finally
            {
                callScope.Dispose();
            }
        }
    }
}
