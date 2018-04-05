using System;
using System.ServiceModel.Configuration;

namespace DistributedTrace.ServiceModel.Service
{
    /// <summary>
    /// Расширение подключения сервисного поведения формирования трассировки
    /// </summary>
    public class TraceBehaviorExtension : BehaviorExtensionElement
    {
        /// <summary>
        /// Тип поведения
        /// </summary>
        public override Type BehaviorType { get { return typeof(TraceBehavior); } }

        /// <summary>
        /// Создание экземпляра поведения
        /// </summary>
        /// <returns></returns>
        protected override object CreateBehavior()
        {
            return new TraceBehavior();
        }
    }
}
