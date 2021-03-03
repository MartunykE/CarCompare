using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Abstractions
{
    public class SubscriptionInfo
    {
        public Type HandlerType { get; }

        private SubscriptionInfo(Type handlerType)
        {
            HandlerType = handlerType;
        }

        public static SubscriptionInfo GetSubscriptionInfo(Type handlerType)
        {
            return new SubscriptionInfo(handlerType);
        }

    }
}
