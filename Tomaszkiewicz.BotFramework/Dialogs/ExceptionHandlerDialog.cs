using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;

namespace Tomaszkiewicz.BotFramework.Dialogs
{
    [Serializable]
    public class ExceptionHandlerDialog<T> : IDialog<T>
    {
        private readonly Func<IDialog<T>> _dialogFactory;
        private readonly bool _displayException;
        private readonly int _stackTraceLength;

        public ExceptionHandlerDialog(Func<IDialog<T>> dialogFactory, bool displayException = true, int stackTraceLength = 500)
        {
            _dialogFactory = dialogFactory;
            _displayException = displayException;
            _stackTraceLength = stackTraceLength;
        }

        public async Task StartAsync(IDialogContext context)
        {
            var dialog = _dialogFactory();

            try
            {
                context.Call(dialog, ResumeAsync);
            }
            catch (Exception e)
            {
                if (_displayException)
                    await DisplayException(context, e);
            }
        }

        private async Task ResumeAsync(IDialogContext context, IAwaitable<T> result)
        {
            try
            {
                context.Done(await result);
            }
            catch (Exception e)
            {
                if (_displayException)
                    await DisplayException(context, e);
            }
        }

        private async Task DisplayException(IDialogContext context, Exception e)
        {
            var stackTrace = e.StackTrace;

            if (stackTrace.Length > _stackTraceLength)
                stackTrace = stackTrace.Substring(0, _stackTraceLength) + "…";

            stackTrace = stackTrace.Replace(Environment.NewLine, "  \n");

            var message = e.Message.Replace(Environment.NewLine, "  \n");

            var exceptionStr = $"**{message}**  \n\n{stackTrace}";

            await context.PostAsync(exceptionStr);
        }
    }
}