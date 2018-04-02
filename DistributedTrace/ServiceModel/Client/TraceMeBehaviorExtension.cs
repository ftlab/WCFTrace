using System;
using System.ServiceModel.Configuration;

namespace DistributedTrace.ServiceModel.Client
{
    public class TraceMeBehaviorExtension : BehaviorExtensionElement
    {
        public override Type BehaviorType { get { return typeof(TraceMeBehavior); } }

        protected override object CreateBehavior()
        {
            return new TraceMeBehavior();
        }
    }
}
