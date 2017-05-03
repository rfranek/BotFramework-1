using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Newtonsoft.Json.Linq;

namespace Tomaszkiewicz.BotFramework.Extensions
{
    public static class DialogContextExtensions
    {
        public static async Task SendTypingMessage(this IDialogContext context)
        {
            var typingMesssage = context.MakeMessage();

            typingMesssage.Type = ActivityTypes.Typing;

            await context.PostAsync(typingMesssage);
        }

        public static IMessageActivity MakeCarousel(this IDialogContext context)
        {
            var reply = context.MakeMessage();

            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            reply.Attachments = new List<Attachment>();

            return reply;
        }

        public static IMessageActivity MakeQuickReplies(this IDialogContext context, IEnumerable<string> replies, string text)
        {
            return context.MakeQuickReplies(replies.ToDictionary(x => x, y => y), text);
        }

        public static IMessageActivity MakeQuickReplies(this IDialogContext context, Dictionary<string, string> replies, string text)
        {
            if (context.Activity.ChannelId == "facebook")
                return MakeQuickRepliesFacebook(context, replies, text);

            var reply = context.MakeMessage();

            reply.Attachments = new List<Attachment>();

            var heroCard = new HeroCard(text: text, buttons: new List<CardAction>());

            foreach (var replyItem in replies)
                heroCard.Buttons.Add(new CardAction("imBack", replyItem.Value, null, replyItem.Key));

            reply.Attachments.Add(heroCard.ToAttachment());

            return reply;
        }

        private static IMessageActivity MakeQuickRepliesFacebook(IDialogContext context, Dictionary<string, string> replies, string text)
        {
            var reply = context.MakeMessage();

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