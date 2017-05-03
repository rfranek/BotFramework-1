using System;
using Microsoft.Bot.Connector;

namespace Tomaszkiewicz.BotFramework.Extensions
{
    public static class ActivityExtensions
    {
        public static ConnectorClient CreateConnectorClient(this IActivity activity)
        {
            return new ConnectorClient((new Uri(activity.ServiceUrl)));
        }
    }
}