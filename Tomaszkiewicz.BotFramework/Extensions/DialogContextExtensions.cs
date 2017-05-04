using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Tomaszkiewicz.BotFramework.Tools;

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
            return QuickReplies.MakeQuickReplies(context.Activity, replies, text);
        }
    }
}