using System.Linq;
using Microsoft.Bot.Connector;

namespace Tomaszkiewicz.BotFramework.Extensions
{
    public static class ConversationUpdateActivityExtensions
    {
        public static bool AreMembersAdded(this IConversationUpdateActivity activity)
        {
            if (!activity.MembersAdded.Any())
                return false;

            var newMembers = activity.MembersAdded?.Where(t => t.Id != activity.Recipient.Id);

            return newMembers != null && newMembers.Any();
        }
    }
}