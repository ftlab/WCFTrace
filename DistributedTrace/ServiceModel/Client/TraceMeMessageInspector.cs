using DistributedTrace.Core;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace DistributedTrace.ServiceModel.Client
{
    /// <summary>
    /// Инспектор клиентского поведения формирования удаленной трассировки
    /// </summary>
    public class TraceMeMessageInspector : IClientMessageInspector
    {
        /// <summary>
        /// Перед отправкой сообщения включаем в заголовок информацию о текущей трассировке
        /// </summary>
        /// <param name="request">запрос</param>
        /// <param name="channel">канал</param>
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

            var index = request.Headers.FindHeader(TraceMeHeader.HeaderName, Namespace.Main);
            if (index > -1)
                request.Headers.RemoveAt(index);

            var header = MessageHeader.CreateHeader(
                TraceMeHeader.HeaderName, Namespace.Main
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
        /// <param name="reply">ответ</param>
        /// <param name="correlationState">корреляция</param>
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            var callScope = correlationState as TraceContextScope;
            if (callScope == null) return;
            try
            {
                var index = reply.Headers.FindHeader(TraceHeader.HeaderName, Namespace.Main);
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
