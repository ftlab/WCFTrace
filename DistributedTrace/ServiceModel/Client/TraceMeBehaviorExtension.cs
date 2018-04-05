using System;
using System.ServiceModel.Configuration;

namespace DistributedTrace.ServiceModel.Client
{
    /// <summary>
    /// Расширение подключения клиентского поведения формирования удаленной трассировки
    /// </summary>
    public class TraceMeBehaviorExtension : BehaviorExtensionElement
    {
        /// <summary>
        /// Тип поведения
        /// </summary>
        public override Type BehaviorType { get { return typeof(TraceMeBehavior); } }

        /// <summary>
        /// Создание экземпляра поведения
        /// </summary>
        /// <returns></returns>
        protected override object CreateBehavior()
        {
            return new TraceMeBehavior();
        }
    }
}
