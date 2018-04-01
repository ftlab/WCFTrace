using System;
using System.ServiceModel.Configuration;

namespace DistributedTrace.ServiceModel.Service
{
    public class TraceBehaviorExtension : BehaviorExtensionElement
    {
        public override Type BehaviorType { get { return typeof(TraceBehavior); } }

        protected override object CreateBehavior()
        {
            return new TraceBehavior();
        }
    }
}
