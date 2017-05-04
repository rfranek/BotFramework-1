using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Tomaszkiewicz.BotFramework.Extensions;

namespace Tomaszkiewicz.BotFramework.Dialogs
{
    [Serializable]
    public class YesNoDialog : IDialog<bool>
    {
        private readonly string _question;
        private readonly string _yesAnswer;
        private readonly string _noAnswer;
        private readonly string _notUnderstood;

        private static readonly string[] YesAnswers = {
            "tak",
            "ok",
            "yes"
        };

        private static readonly string[] NoAnswers =
        {
            "nie",
            "no"
        };

        public YesNoDialog(string question, string yesAnswer, string noAnswer, string notUnderstood)
        {
            _question = question;
            _yesAnswer = yesAnswer;
            _noAnswer = noAnswer;
            _notUnderstood = notUnderstood;
        }

        public async Task StartAsync(IDialogContext context)
        {
            await AskAndWait(context);
        }

        private async Task AskAndWait(IDialogContext context)
        {
            await context.PostAsync(context.MakeQuickReplies(new[] { _yesAnswer, _noAnswer }, _question));

            context.Wait(OnAnswer);
        }

        private async Task OnAnswer(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            if (!string.IsNullOrWhiteSpace(message.Text))
            {
                if (message.Text == _yesAnswer || YesAnswers.Contains(message.Text.ToLower()))
                {
                    context.Done(true);
                }
                else if (message.Text == _noAnswer || NoAnswers.Contains(message.Text.ToLower()))
                {
                    context.Done(false);
                }
                else
                {
                    await context.PostAsync(_notUnderstood);
                    await AskAndWait(context);
                }
            }
        }
    }
}