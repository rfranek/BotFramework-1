using System.Collections.Generic;
using System.Linq;
using Microsoft.Bot.Builder.Dialogs;

namespace Tomaszkiewicz.BotFramework.Tools
{
    public class RepliesSet
    {
        private readonly IRepliesProvider _provider;

        public RepliesSet(IRepliesProvider provider)
        {
            _provider = provider;
        }

        public string GetReply(IDialogContext context, string key, params object[] args)
        {
            return string.Format(GetReply(context, key), args);
        }

        public string GetReply(IDialogContext context, string key)
        {
            var replies = _provider.GetReplies(key);
            
            if(!replies.Any())
                throw new KeyNotFoundException($"Cannot find any replies for key: {key}.");

            var storeKey = $"repliesSet_{key}";
            int lastReply;

            if (context.PrivateConversationData.TryGetValue(storeKey, out lastReply))
                lastReply++;

            if (lastReply >= replies.Length)
                lastReply = 0;
            
            context.PrivateConversationData.SetValue(storeKey, lastReply);

            return replies[lastReply];
        }
    }
}