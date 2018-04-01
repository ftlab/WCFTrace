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
            var ctx = TraceContext.Current;
            if (ctx != null)
            {
                var header = MessageHeader.CreateHeader(
                    TraceMeHeader.HeaderName, TraceMeHeader.Namespace
                    , new TraceMeHeader()
                    {
                        Id = ctx.Trace.Id,
                        Name = ctx.Trace.Name,
                    });

                request.Headers.Add(header);
            }
            return null;
        }

        /// <summary>
        /// После получения ответа извлекаем трассировку
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="correlationState"></param>
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            var ctx = TraceContext.Current;
            if (ctx != null)
            {
                var header = reply.Headers.GetHeader<TraceHeader>(
                    TraceHeader.HeaderName, TraceHeader.Namespace);
                if (header != null && header.Trace != null)
                    ctx.Trace.AddTrace(header.Trace);
            }
        }
    }
}
