using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Bot.Connector;
using Tomaszkiewicz.BotFramework.Tools;

namespace Tomaszkiewicz.BotFramework.Extensions
{
    public static class ActivityExtensions
    {
        public static ConnectorClient CreateConnectorClient(this IActivity activity)
        {
            return new ConnectorClient((new Uri(activity.ServiceUrl)));
        }

        public static IMessageActivity MakeQuickReplies(this IActivity activity, IEnumerable<string> replies, string text)
        {
            return activity.MakeQuickReplies(replies.ToDictionary(x => x, y => y), text);
        }

        public static IMessageActivity MakeQuickReplies(this IActivity activity, Dictionary<string, string> replies, string text)
        {
            return QuickReplies.MakeQuickReplies(activity, replies, text);
        }
    }
}