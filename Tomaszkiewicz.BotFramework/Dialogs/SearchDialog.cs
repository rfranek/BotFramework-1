using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Tomaszkiewicz.BotFramework.Extensions;

namespace Tomaszkiewicz.BotFramework.Dialogs
{
    [Serializable]
    public class SearchDialog : IDialog<string>
    {
        private readonly Func<string, Task<IEnumerable<string>>> _searchFunc;
        private readonly string _firstPromptText;
        private readonly string _noResultsText;
        private readonly string _specifyPromptText;
        private readonly string _toomanyAttempsExceptionMessage;
        private readonly int _maxAttemps;
        private int _attemp;

        public SearchDialog(Func<string, Task<IEnumerable<string>>> searchFunc, string firstPromptText, string noResultsText, string specifyPromptText, string toomanyAttempsExceptionMessage, int maxAttemps = 3)
        {
            _searchFunc = searchFunc;
            _firstPromptText = firstPromptText;
            _noResultsText = noResultsText;
            _specifyPromptText = specifyPromptText;
            _toomanyAttempsExceptionMessage = toomanyAttempsExceptionMessage;
            _maxAttemps = maxAttemps;
        }

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync(_firstPromptText);

            context.Wait(Resume);
        }

        private async Task Resume(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var searchQuery = (await result).Text;
            var searchResults = (await _searchFunc(searchQuery)).ToArray();

            _attemp++;

            if (_attemp > _maxAttemps)
            {
                context.Fail(new TooManyAttemptsException(_toomanyAttempsExceptionMessage));

                return;
            }
            
            if (!searchResults.Any())
            {
                await context.PostAsync(_noResultsText);

                context.Wait(Resume);

                return;
            }

            if (searchResults.Length > 1)
            {
                await context.PostAsync(context.MakeQuickReplies(searchResults, _specifyPromptText));

                context.Wait(Resume);

                return;
            }

            context.Done(searchResults[0]);
        }
    }
}