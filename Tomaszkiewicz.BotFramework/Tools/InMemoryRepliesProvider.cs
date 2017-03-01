using System.Collections.Generic;

namespace Tomaszkiewicz.BotFramework.Tools
{
    public class InMemoryRepliesProvider : IRepliesProvider
    {
        readonly Dictionary<string, List<string>> _replies = new Dictionary<string, List<string>>();

        public void AddReply(string key, string reply)
        {
            EnsureKey(key);

            _replies[key].Add(reply);
        }

        public void AddReplies(string key, IEnumerable<string> replies)
        {
            EnsureKey(key);

            _replies[key].AddRange(replies);
        }

        public string[] GetReplies(string key)
        {
            EnsureKey(key);

            return _replies[key].ToArray();
        }

        private void EnsureKey(string key)
        {
            if (!_replies.ContainsKey(key))
                _replies[key] = new List<string>();
        }
    }
}