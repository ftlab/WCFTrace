using DistributedTrace.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Xml;

namespace DistributedTrace.ServiceModel.Client
{
    /// <summary>
    /// Инспектор включения в заголовок сообщения признака о необходимости формирования трассировки
    /// </summary>
    public class TraceMeMessageInspector : IClientMessageInspector
    {
        public TraceMeMessageInspector()
        {
        }

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
            var @event = TraceEvent.Create(id: id
                , message: request.Headers.Action
                , type: "call");

            var callScope = new TraceContextScope(id, @event, TraceContextMode.Add);

            var index = request.Headers.FindHeader(TraceMeHeader.HeaderName, Namespace.Value);
            if (index > -1)
                request.Headers.RemoveAt(index);

            var header = MessageHeader.CreateHeader(
                TraceMeHeader.HeaderName, Namespace.Value
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
                var index = reply.Headers.FindHeader(TraceHeader.HeaderName, Namespace.Value);
                if (index < 0) return;

                var header = reply.Headers.GetHeader<TraceHeader>(index);
                if (header != null && header.Root != null)
                    callScope.Context.AppendEvent(header.Root);
            }
            finally
            {
                callScope.Dispose();
            }
        }
    }
}
