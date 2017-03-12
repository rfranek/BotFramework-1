using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Tomaszkiewicz.BotFramework.WebApi.Extensions;

namespace Tomaszkiewicz.BotFramework.WebApi.Dialogs
{
    [Serializable]
    public abstract class AutoDeserializeDialog<T> : IDialog<T>
    {
        [OnDeserialized]
        private void OnDeserialized(StreamingContext ctx)
        {
            this.RestoreAllDependencies();
        }

        public abstract Task StartAsync(IDialogContext context);
    }
}