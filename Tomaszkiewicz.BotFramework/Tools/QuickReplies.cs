using System.Collections.Generic;
using System.Linq;
using Microsoft.Bot.Builder.ConnectorEx;
using Microsoft.Bot.Connector;
using Newtonsoft.Json.Linq;

namespace Tomaszkiewicz.BotFramework.Tools
{
    public static class QuickReplies
    {
        internal static IMessageActivity MakeQuickReplies(IActivity activity, Dictionary<string, string> replies, string text)
        {
            if (activity.ChannelId == "facebook")
                return MakeQuickRepliesFacebook(activity, replies, text);

            var reply = activity.ToConversationReference().GetPostToUserMessage();

            reply.Attachments = new List<Attachment>();

            var heroCard = new HeroCard(text: text, buttons: new List<CardAction>());

            foreach (var replyItem in replies)
                heroCard.Buttons.Add(new CardAction("imBack", replyItem.Value, null, replyItem.Key));

            reply.Attachments.Add(heroCard.ToAttachment());

            return reply;
        }

        private static IMessageActivity MakeQuickRepliesFacebook(IActivity activity, Dictionary<string, string> replies, string text)
        {
            var reply = activity.ToConversationReference().GetPostToUserMessage();

            reply.Text = text;

            var items = replies.Select(x => new
            {
                content_type = "text",
                title = x.Value,
                payload = x.Key
            });

            reply.ChannelData = JObject.FromObject(new
            {
                quick_replies = items
            });

            return reply;
        }
    }
}