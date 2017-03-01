﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

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

        public static IMessageActivity MakeQuickReplies(this IDialogContext context, IEnumerable<string> replies, string text = null)
        {
            return context.MakeQuickReplies(replies.ToDictionary(x => x, y => y), text);
        }

        public static IMessageActivity MakeQuickReplies(this IDialogContext context, Dictionary<string, string> replies, string text = null)
        {
            var reply = context.MakeMessage();

            reply.Attachments = new List<Attachment>();

            var heroCard = new HeroCard()
            {
                Text = text,
                Buttons = new List<CardAction>()
            };

            foreach (var replyItem in replies)
            {
                var button = new CardAction()
                {
                    Type = "imBack",
                    Value = replyItem.Key,
                    Title = replyItem.Value
                };

                heroCard.Buttons.Add(button);
            }

            reply.Attachments.Add(heroCard.ToAttachment());

            return reply;
        }
    }
}