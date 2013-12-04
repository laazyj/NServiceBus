﻿namespace NServiceBus.Pipeline.Contexts
{
    using Unicast.Behaviors;
    using Unicast.Messages;

    class HandlerInvocationContext : BehaviorContext
    {
        public HandlerInvocationContext(BehaviorContext parentContext, MessageHandler messageHandler)
            : base(parentContext)
        {
            Set(messageHandler);
        }

        public MessageHandler MessageHandler
        {
            get { return Get<MessageHandler>(); }
        }

        public LogicalMessage LogicalMessage
        {
            get { return Get<LogicalMessage>(); }
        }
    }
}