using System.Threading.Tasks;
using Microsoft.Bot.Connector;

namespace Tomaszkiewicz.BotFramework
{
    public abstract class Bot
    {
        public async Task DispatchActivity(Activity activity)
        {
            switch (activity.GetActivityType())
            {
                case ActivityTypes.Message:
                    await OnMessage(activity);
                    break;

                case ActivityTypes.ConversationUpdate:
                    await OnConversationUpdate(activity);
                    break;

                case ActivityTypes.Event:
                    await OnEvent(activity);
                    break;

                case ActivityTypes.ContactRelationUpdate:
                    await OnContactRelationUpdate(activity);
                    break;

                case ActivityTypes.DeleteUserData:
                    await OnDeleteUserData(activity);
                    break;

                case ActivityTypes.Typing:
                    // do nothing
                    break;

                case ActivityTypes.Ping:
                    // do nothing
                    break;

                default:
                    await OnUnknownActivity(activity);
                    break;
            }
        }

        public virtual Task OnMessage(Activity message)
        {
            return Task.CompletedTask;
        }

        public virtual Task OnConversationUpdate(IConversationUpdateActivity activity)
        {
            return Task.CompletedTask;
        }

        public virtual Task OnEvent(IEventActivity activity)
        {
            return Task.CompletedTask;
        }

        public virtual Task OnContactRelationUpdate(IContactRelationUpdateActivity activity)
        {
            return Task.CompletedTask;
        }

        public virtual Task OnDeleteUserData(Activity activity)
        {
            return Task.CompletedTask;
        }

        public Task OnUnknownActivity(Activity activity)
        {
            return Task.CompletedTask;
        }
    }
}