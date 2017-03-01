using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;

namespace Tomaszkiewicz.BotFramework.Dialogs
{
    [Serializable]
    public class PromptTimeDialog : IDialog<DateTime>
    {
        private readonly string _prompt;
        private readonly string _retry;
        private readonly int _attemps;

        public PromptTimeDialog(string prompt, string retry = null, int attemps = 3)
        {
            _prompt = prompt;
            _attemps = attemps;
            _retry = retry;
        }

        public Task StartAsync(IDialogContext context)
        {
            PromptForTime(context);

            return Task.CompletedTask;
        }

        private void PromptForTime(IDialogContext context)
        {
            PromptDialog.Text(context, ValidateTime, _prompt, _retry, _attemps);
        }

        private async Task ValidateTime(IDialogContext context, IAwaitable<string> result)
        {
            var str = await result;

            DateTime time;

            if (DateTime.TryParse(str, out time))
            {
                context.Done(time);
                return;
            }

            PromptForTime(context);
        }
    }
}